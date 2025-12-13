using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject wolfPrefab, wolfEaterPrefab, enemy1Prefab;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int eaterChance = 3; // chance out of 10 to spawn eater wolf
    [SerializeField]
    private int enemy1Chance = 2; // chance out of 10 to spawn enemy1

    [SerializeField]
    private float spawnTime = 12f;
    [SerializeField]
    private float spawnReductionPerWolf = 1f;
    [SerializeField]
    private float minSpawnDelay = 3.5f;

    private float currentSpawnTime;
    private float timer;

    private void Start()
    {
        currentSpawnTime = spawnTime;
        timer = Time.time;
    }

    private void Update()
    {
        if (Time.time > timer)
        {
            Spawn();

            currentSpawnTime -= spawnReductionPerWolf;
            if (currentSpawnTime <= minSpawnDelay)
                currentSpawnTime = minSpawnDelay;

            timer = Time.time + currentSpawnTime;
        }
    }

    void Spawn()
    {
        int roll = Random.Range(0, 11);

        if (roll <= enemy1Chance)
        {
            Instantiate(enemy1Prefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
        else if (roll <= enemy1Chance + eaterChance)
        {
            Instantiate(wolfEaterPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
        else
        {
            Instantiate(wolfPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
