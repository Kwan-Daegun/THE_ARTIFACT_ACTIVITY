using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject wolfPrefab, wolfEaterPrefab;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int eaterChance = 3; // chance out of 10 to spawn eater wolf

    [SerializeField]
    private float spawnTime = 12f; // initial spawn time delay per wolf

    [SerializeField]
    private float spawnReductionPerWolf = 1f; // reduction in spawn delay per each wolf

    [SerializeField]
    private float minSpawnDelay = 3.5f; // min spawn delay per wolf;

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

        if (Random.Range(0, 11) > eaterChance)
        {
            Instantiate(wolfPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);

        }
        else
        {
            Instantiate(wolfEaterPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);
        }

    }
        

} // class





























