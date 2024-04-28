using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnWarp : MonoBehaviour
{
    public GameObject warpPrefab;
    private float spawnDelay = 120f;
    private float spawnAreaWidth = 24f;
    private float spawnAreaLength = 8f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn_A_Warp", spawnDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("MoveGunPowerUp here!");
    }

    void Spawn_A_Warp()
    {
        Debug.Log("[+] Spawning a warp!");
        // Generate random position within spawn area
        float randomX = UnityEngine.Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f);

        float randomZ = UnityEngine.Random.Range(-spawnAreaLength / 2f, spawnAreaLength / 2f);

        Vector3 randomPosition = new Vector3(randomX, 1.1f, randomZ);
        Debug.Log("About to Instantiate Warp!");

        Instantiate(warpPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Warp Prefab Instantiated!");


    }
}
