using UnityEngine;
using System.Collections;
using System.Collections.Generic; //для List
using System.Linq; //для ToList
using UnityEngine.EventSystems;



public class PlayerMove : MonoBehaviour
{

    [SerializeField] float speed = 1.5f;
    Vector3[] point = new Vector3[6];
    int i = 1;
    Vector3 startingPosition;
    Vector3 distance;
    bool go = false;
    GameObject[] movePoints = new GameObject[6];
    [SerializeField] GameObject movePoint;
    [SerializeField] GameObject lineGeneratorPrefab;
 


    public void ConfirmButton()
    {
        go = true;
        i = 1;
    }

    public void CancelButton()
    {
        for (i = 0; i < (point.Length); i++)
        {
            point[i] = startingPosition;
            Destroy(movePoints[i]);
        }
        i = 1;
        ClearAllPoints();


    }

   void Start()
   {
        startingPosition = transform.position;
        point[0] = startingPosition;
        point[0].z = transform.position.z;

   }

    void Update()
    {
        

        if (go == false) // режим постановки траектории
        {
            if (!EventSystem.current.IsPointerOverGameObject()) //проверка, что мышь не на кнопке интрефейса
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (i < (point.Length))
                    {

                        point[i] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        // создание точки узла
                        // movePoints[i] = movePoint;
                        movePoints[i] = Instantiate(movePoint, new Vector3(point[i].x, point[i].y, 0), Quaternion.identity);
                        GenerateLine();
                        i++;
                    }
                    
                }
            }

        }

        if (go == true)// режим движения по траектории
        {
            if (i < point.Length)
            {
                 if (point[i] == startingPosition)
                 { i = point.Length; }
                if (i < point.Length)
                {
                    point[i].z = transform.position.z;
                    transform.position = Vector3.MoveTowards(transform.position, point[i], speed * Time.deltaTime);
                    distance = point[i] - transform.position;
                    if (distance.sqrMagnitude < 0.03f * 0.03f)
                    {
                        i++;
                    }
                }

            }
        }
    }
     
    private void GenerateLine()
    {
        // Get GameObject array of all objects with tag 'PointMarker'.
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("movePoint");
        // Initialise new Vector3 array, length = allPoints.Length.
        Vector3[] allPointPositions = new Vector3[allPoints.Length];

        allPointPositions[0] = startingPosition;
            // Loop to assign Vector3 to allPointPositions from allPoint object's transform.position.
            for (int a = 1; a < allPoints.Length; a++)
            {
                allPointPositions[a] = allPoints[a].transform.position;
            }
            
            // Run function to create line, give it allPointPositions array as parameter.
            SpawnLineGenerator(allPointPositions);
       
    }

    private void SpawnLineGenerator(Vector3[] linePoints)
    {
        // Create new LineHolder object.
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        // Get reference to newLineGen's LineRenderer.
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        // Set amount of LineRenderer positions = amount of line point positions.
        lRend.positionCount = linePoints.Length;
        // Set positions of LineRenderer using linePoints array.
        lRend.SetPositions(linePoints);

    }

    private void ClearAllPoints()
    {
        // Get GameObject array of all objects with tag 'PointMarker'.
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("line");

        // For each object in array, destroy it.
        foreach (GameObject p in allPoints)
        {
            Destroy(p);
        }
    }
}
        
    
