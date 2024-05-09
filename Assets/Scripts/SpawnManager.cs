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


    public GameObject warpPrefab;
    private float spawnDelayWarp = 10f;
    private float spawnAreaWidthWarp = 24f;

    public GameObject[] powerUpPrefabs;
    private float spawnDelayPowerUp = 30f;
    private float spawnAreaWidthPowerUp = 24f;
    private float spawnAreaUpperPowerUp = 1.5f;
    private float spawnAreaLowerPowerUp = -10f;
    private float powerUpDurationPowerUp = 10f;

    public GameObject escapeRocket; // Reference to the prefab you want to instantiate
    public float delayInSecondsRocket = 3f; // Delay before instantiation
    private static bool objectInstantiatedRocket = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRobot", startDelay, spawnInterval);
        InvokeRepeating("Spawn_A_Warp", spawnDelayWarp, spawnDelayWarp);
        InvokeRepeating("SpawnPowerups", spawnDelayPowerUp, spawnDelayPowerUp);
        StartCoroutine(InstantiateSpaceshipAfterDelay());
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

    // spawn either a gun or a health power up! 
    void SpawnPowerups()
    {
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidthPowerUp / 2f, spawnAreaWidthPowerUp / 2f);
        float randomZ = UnityEngine.Random.Range(spawnAreaLowerPowerUp, spawnAreaUpperPowerUp);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        int upIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject healthPower = Instantiate(powerUpPrefabs[upIndex], randomPosition, Quaternion.identity);
        
        Destroy(healthPower, powerUpDurationPowerUp);

    }


    // This code spawns the escape rocket after a certain number of seconds have passed 
    IEnumerator InstantiateSpaceshipAfterDelay()
    {
        yield return new WaitForSeconds(delayInSecondsRocket);

        if (!objectInstantiatedRocket)
        {
            // Instantiate the object after the delay
            Vector3 pos = new Vector3(-2.5f, 5f, 0f);
            Instantiate(escapeRocket, pos, Quaternion.identity);


            objectInstantiatedRocket = true;
        }


    }

    void Spawn_A_Warp()
    {
        Debug.Log("[+] Spawning a warp!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidthWarp / 2f, spawnAreaWidthWarp / 2f);

        float randomZ = UnityEngine.Random.Range(-6f, 1.5f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        Debug.Log("About to Instantiate Warp!");

        var new_warp = Instantiate(warpPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Warp Prefab Instantiated!");

        Destroy(new_warp, 15f); // Destroy the warp after 15 seconds
    }
}
