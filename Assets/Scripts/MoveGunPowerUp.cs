using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveGunPowerUp : MonoBehaviour
{
    public GameObject gunPowerUpPrefab;
    private float spawnDelay = 5f;
    private float spawnAreaWidth = 24f;
    private float spawnAreaLength = 8f;
    private float powerUpDuration = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", spawnDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("MoveGunPowerUp here!");
    }

    void SpawnPowerUp()
    {
        // Debug.Log("[+] Gun PowerUp Prefab called!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f);

        float randomZ = UnityEngine.Random.Range(-spawnAreaLength / 2f, spawnAreaLength / 2f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        // Debug.Log("[+] About to Instantiate Gun PowerUp!");

        GameObject newGunPowerUp = Instantiate(gunPowerUpPrefab, randomPosition, Quaternion.identity);
        // Debug.Log("[+] Gun PowerUp Instantiated!");

        Destroy(newGunPowerUp, powerUpDuration);


    }
}
