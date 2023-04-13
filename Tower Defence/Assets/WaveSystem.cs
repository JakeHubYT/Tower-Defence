using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyHolder;
        public Enemy[] enemys;
        public int numEnemies;
        public float spawnDelay;
    }

    public Wave[] waves;

    private int currentWaveIndex = 0;
    private bool spawningWave = false;
    private bool canSpawn = false;

    IEnumerator SpawnWave(Wave wave)
    {
        spawningWave = true;
        for (int i = 0; i < wave.numEnemies; i++)
        {
            GameObject currentEnemy = wave.enemyHolder;
            Instantiate(currentEnemy, transform.position, Quaternion.identity);

            currentEnemy.GetComponent<EnemyController>().enemyScriptableObj = wave.enemys[Random.Range(0, wave.enemys.Length)];

            yield return new WaitForSeconds(wave.spawnDelay);
        }
        spawningWave = false;
        canSpawn = false;
        currentWaveIndex++;
    }
    
    void Update()
    {
        if (!spawningWave && currentWaveIndex < waves.Length && canSpawn)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }

    }


    public void StartSpawning() => canSpawn = true;
  
}
