using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RatMove : MonoBehaviour
{

    [SerializeField] float speed = 5f; // скорость игрока
    Vector3[] point = new Vector3[10]; // массив координат точек для движения
    GameObject[] movePoints = new GameObject[10]; // массив видимых обьектов-точек
    int pointNumber = 1; // порядковый номер точки
    Vector3 startingPosition; // стартовая позиция игрока
    Vector3 distance; // расстояния между точками
    const float minDistance = 0.03f * 0.03f;
    bool go = false; // переключатель состояние траектория-движение
    bool scoreWindowActive = false; //используется при проверке на активное окно очков - конец уровня
    [SerializeField] GameObject movePoint; // ссылка на обьект-точку
    [SerializeField] GameObject lineGeneratorPrefab; // ссылка на обьект с lineRenderer
    public GameObject scoreWindow;

    // кнопка подтверждения
    public void ConfirmButton()
    {
        if ((go == false) && (scoreWindowActive == false))
        {
            pointNumber = 1; // устанавливаем на движение к первой точке
            go = true; // переключаем на режим движения
        }
    }

    // кнопка отмены
    public void CancelButton()
    {
        if ((go == false) && (scoreWindowActive == false))
        {
            ClearAllMovePoints(); // чистим все точки
            ClearAllLines(); // чистим все линии
            pointNumber = 1; // устанавливаем на построение траектории с 1й (не стартовой) точки
        }
    }

    void Start()
    {

        startingPosition = transform.position; // задаем стартовую позицию
        point[0] = startingPosition; // первый узел траектории всегда стартовая позиции
        point[0].z = transform.position.z; // координата по z у стартовой точки = как у игрока, чтобы не выходить за пределы видимости камеры
    }

    void Update()
    {

        // режим постановки траектории
        if ((go == false) && (scoreWindowActive == false))
        {
            // считываем нажатие touch
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                //проверка, что мышь не на кнопке интрефейса
                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // считываем нажатие мыши
     //           if (Input.GetMouseButtonDown(0))
     //           {
                    // проверяем, что доступное количество точек еще не поставлено
                    if (pointNumber < point.Length)
                    {
                        // записываем позицию мыши (при клике) в массив точек
                        point[pointNumber] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        // вызываем функцию генерации точки
                        GenerateMovePoint();
                        // вызываем функцию генерации линии
                        GenerateLine();
                        // переход к вводу следующей точки
                        pointNumber++;
                    }
                  
                }
            }

        }

        // режим движения по траектории
        if (go == true)
        {
                if ((pointNumber < point.Length) && !(point[pointNumber] == startingPosition))
                {
                    // координата по z точки = как у игрока, чтобы не выходить за пределы видимости камеры
                    point[pointNumber].z = transform.position.z;
                    // движение игрока к следующей точке
                    transform.position = Vector3.MoveTowards(transform.position, point[pointNumber], speed * Time.deltaTime);
                    // проверка очень близкого приближения к следующей точке
                    distance = point[pointNumber] - transform.position;
                    if (distance.sqrMagnitude < minDistance)
                    {
                        // переход к следующей точке
                        pointNumber++;
                    }
                } 
                else
                {
                    ShowMissionScore();
                }
        }
        
    }

    // функция получения массива координат точек для последующей отрисовки
    private void GenerateLine()
    {
        // Получение массива Обьектов с тагом movePoint
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("movePoint");
        // инициализация массива векторов обьектов 
        Vector3[] allPointPositions = new Vector3[allPoints.Length];
        //первый вектор - стартовая позиция игрока
        allPointPositions[0] = startingPosition;
        // цикл сопоставляющий массив обьектов и векторов
        for (int a = 1; a < allPoints.Length; a++)
        {
            allPointPositions[a] = allPoints[a].transform.position;
        }

        // вызов функции отрисовки линии
        SpawnLineGenerator(allPointPositions);

    }

    // функция отрисовки линии
    private void SpawnLineGenerator(Vector3[] linePoints)
    {
        // Создание обьекта с компонентом lineRenderer
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        // получение компонента lineRenderer
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        // задание количества узлов в lineRenderer = количеству элементов массива
        lRend.positionCount = linePoints.Length;
        // задание позиций в lineRenderer из массива linePoints
        lRend.SetPositions(linePoints);

    }

    // все узлы уничтожаются, а их координаты становятся стартовыми
    private void ClearAllMovePoints()
    {
        // цикл по всем точкам, кроме стартовой
        for (int i = 1; i < (point.Length); i++)
        {
            // точка прохода = точка старта
            point[i] = startingPosition;
            // видимая точка уничтожается
            Destroy(movePoints[i]);
        }
    }

    //очистка всех точек
    private void ClearAllLines()
    {
        // Получение массива Обьектов с тагом line
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("line");

        // Уничтожение каждого обьекта в массиве
        foreach (GameObject l in allLines)
        {
            Destroy(l);
        }
    }
    //создание точки узла
    private void GenerateMovePoint()
    {
        movePoints[pointNumber] = Instantiate(movePoint, new Vector3(point[pointNumber].x, point[pointNumber].y, -1), Quaternion.identity);
    }

    public void ShowMissionScore()
    {
        scoreWindowActive = true;
        go = false;
        Debug.Log("Pause");
        Instantiate(scoreWindow, new Vector3(0, 0, -20), Quaternion.identity);
    }


}