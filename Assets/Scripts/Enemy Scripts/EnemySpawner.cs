using System.Collections;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject wolfPrefab, wolfEaterPrefab, enemy1Prefab;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int totalWaves = 10;
    public float timeBetweenWaves = 5f;

    [Header("UI")]
    public TMP_Text waveText;

    public static int enemiesAlive = 0;
    private int currentWave = 0;

    private void Start()
    {
        enemiesAlive = 0;
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        while (currentWave < totalWaves)
        {
            currentWave++;
            UpdateWaveText();

            yield return StartCoroutine(SpawnWave(currentWave));

            yield return new WaitUntil(() => enemiesAlive <= 0);

            if (currentWave < totalWaves)
            {
                if (waveText != null)
                    waveText.text = "Next wave in " + timeBetweenWaves + "s...";
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        if (waveText != null)
            waveText.text = "Final Wave Complete!";

        if (GameOverUIController.instance != null)
            GameOverUIController.instance.Win();
    }

    IEnumerator SpawnWave(int wave)
    {
        int enemyCount = 3 + (wave * 2);
        float spawnDelay = Mathf.Max(0.3f, 1.5f - (wave * 0.1f));

        for (int i = 0; i < enemyCount; i++)
        {
            enemiesAlive++;
            SpawnEnemy(wave);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(int wave)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        int roll = Random.Range(0, 10);

        if (wave >= 7 && roll < 4)
            Instantiate(enemy1Prefab, spawnPoint.position, Quaternion.identity);
        else if (wave >= 4 && roll < 4)
            Instantiate(wolfEaterPrefab, spawnPoint.position, Quaternion.identity);
        else if (roll < 3 + (wave / 3))
            Instantiate(wolfEaterPrefab, spawnPoint.position, Quaternion.identity);
        else
            Instantiate(wolfPrefab, spawnPoint.position, Quaternion.identity);
    }

    void UpdateWaveText()
    {
        if (waveText != null)
            waveText.text = "Wave " + currentWave + " / " + totalWaves;
    }
}