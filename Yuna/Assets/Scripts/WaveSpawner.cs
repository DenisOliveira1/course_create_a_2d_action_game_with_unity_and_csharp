using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave{
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    public Wave[] waves;
    public Transform[] SpawnPoints;
    public float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    private bool finishedSpawing;

    public GameObject boss;
    public Transform bossSpawnPoint;
    public GameObject healthBar;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    private void Update()
    {
        if(finishedSpawing && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            finishedSpawing = false;
            if(currentWaveIndex + 1 < waves.Length)
                {
                    currentWaveIndex++;
                    StartCoroutine(StartNextWave(currentWaveIndex));
                }
            else
                {
                    Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                    healthBar.gameObject.SetActive(true);
                }
        }
    }

    IEnumerator StartNextWave(int index){
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index){
        currentWave = waves[index];

        for (var i = 0; i < currentWave.count; i++)
        {
            if (!IsPlayerAlive()) yield break;

            Enemy randomEnemy = currentWave.enemies[Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            finishedSpawing = IsFinishedSpawing(i);

            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
    }

    private bool IsFinishedSpawing(int i)
    {
        return i == currentWave.count - 1;
    }

    private bool IsPlayerAlive()
    {
        return player != null;
    }
}
