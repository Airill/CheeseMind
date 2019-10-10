using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{
    public GameObject cheesePrefab;
    Vector3 cheeseStartVector;
    float spawnTime = 0.3f;

    private void Start()
    {
        spawnCheese();
    }

    void spawnCheese() 
    {
        cheeseStartVector = new Vector3(Random.Range(-8, 8), 6, 0);
        Instantiate(cheesePrefab, cheeseStartVector, Quaternion.Euler(0, 0, Random.Range(0,360)));
        Invoke("spawnCheese", spawnTime);
    }
}
