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
    public AudioSource ghettoblaster;
   

    public GameObject warpPrefab;
    private float spawnDelayWarp = 10f;
    private float spawnAreaWidthWarp = 24f;

    public GameObject[] powerUpPrefabs;
    private float spawnDelayPowerUp = 5f;
    private float spawnAreaWidthPowerUp = 24f;
    private float spawnAreaUpperPowerUp = 15f;
    private float spawnAreaLowerPowerUp = 9f;
    private float powerUpDurationPowerUp = 10f;

    public GameObject escapeRocket; // Reference to the prefab you want to instantiate
    private float delayInSecondsRocket = (60 * 1f); // Delay 1 minutes before instantiation
    private static bool objectInstantiatedRocket = false;
    public AudioClip escape_rocket_entrance;
    private GameManager spawn_game_manager;

    // Start is called before the first frame update
    void Start()
    {
        spawn_game_manager = GameObject.Find("GameManager").GetComponent<GameManager>();
 
        Debug.Log("Game is active!");
        ghettoblaster.volume = 0.5f;
        ghettoblaster.Play();
    }

    public void setRocketInst(bool b)
    {
        objectInstantiatedRocket = b;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void StartGame()
    {
        Debug.Log("SpawnManager.StartGame() called!");
        if (spawn_game_manager.isGameActive)
        {
            InvokeRepeating("SpawnRobot", startDelay, spawnInterval);
            InvokeRepeating("Spawn_A_Warp", spawnDelayWarp, spawnDelayWarp);
            InvokeRepeating("SpawnPowerups", spawnDelayPowerUp, spawnDelayPowerUp);
            
        }
    }

    public IEnumerator SpawnGameObjects(float spawnInterval)
    {
        while (spawn_game_manager.isGameActive)
        {
            Debug.Log("Spawning game objects every: " + spawnInterval);
            yield return new WaitForSeconds(spawnInterval);
            SpawnRobot();
            SpawnPowerups();
            Spawn_A_Warp();
        }
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
    public IEnumerator InstantiateSpaceshipAfterDelay()
    {
        Debug.Log("[+] Going to instantiate the escape rocket in: " + delayInSecondsRocket + " seconds");
        
        yield return new WaitForSeconds(delayInSecondsRocket);

        if (!objectInstantiatedRocket)
        {
            ghettoblaster.PlayOneShot(escape_rocket_entrance, 1f);
            // Instantiate the object after the delay
            Vector3 pos = new Vector3(-2.5f, 2f, 5f);
            Instantiate(escapeRocket, pos, Quaternion.identity);


            objectInstantiatedRocket = true;
        }


    }

    void Spawn_A_Warp()
    {
        // Debug.Log("[+] Spawning a warp!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidthWarp / 2f, spawnAreaWidthWarp / 2f);

        float randomZ = UnityEngine.Random.Range(-6f, 1.5f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        // Debug.Log("About to Instantiate Warp!");

        var new_warp = Instantiate(warpPrefab, randomPosition, Quaternion.identity);
        // Debug.Log("Warp Prefab Instantiated!");

        Destroy(new_warp, 15f); // Destroy the warp after 15 seconds
    }
}
