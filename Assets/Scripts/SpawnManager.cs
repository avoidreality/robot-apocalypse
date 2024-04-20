using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] robots;
    private float spawnRangeX = 13;
    private float spawnPosZ = 20;
    private float startDelay = 1;
    private float spawnInterval = 5.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRobot", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRobot()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ); // Get a random x position for the Vector3 object for the robot
        int roboIndex = Random.Range(0, robots.Length); // get a random number between 0 and 2
        // instantiate a random animal with the random number and the random x position Vector3 
        Instantiate(robots[roboIndex], spawnPos, robots[roboIndex].transform.rotation);

       
    }
}
