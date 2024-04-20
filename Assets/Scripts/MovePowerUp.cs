using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovePowerUp : MonoBehaviour
{
    public GameObject powerUpPrefab;
    private float spawnDelay = 60f;
    private float spawnAreaWidth = 24f;
    private float spawnAreaLength = 8f;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("[+] MovePowerUp (HealthPowerUp) Start method called!");


        InvokeRepeating("SpawnPowerUp", spawnDelay, spawnDelay);

        Debug.Log("[+] InvokeRepeating method called for the HealthPowerUp!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPowerUp()
    {
        Debug.Log("[+] Health Power Up Prefab called!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f);

        float randomZ = UnityEngine.Random.Range(-spawnAreaLength / 2f, spawnAreaLength / 2f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        Debug.Log("About to Instantiate Health Power Up!");

        Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Health Power Up Instantiated!");


    }
}
