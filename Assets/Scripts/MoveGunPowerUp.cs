using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveGunPowerUp : MonoBehaviour
{
    public GameObject gunPowerUpPrefab;
    private float spawnDelay = 10f;
    private float spawnAreaWidth = 24f;
    private float spawnAreaLength = 8f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 3f, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("MoveGunPowerUp here!");
    }

    void SpawnPowerUp()
    {
        Debug.Log("[+] PowerUp Prefab called!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f);

        float randomZ = UnityEngine.Random.Range(-spawnAreaLength / 2f, spawnAreaLength / 2f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        Debug.Log("About to Instantiate PowerUp!");

        Instantiate(gunPowerUpPrefab, randomPosition, Quaternion.identity);
        Debug.Log("PowerUp Instantiated!");


    }
}
