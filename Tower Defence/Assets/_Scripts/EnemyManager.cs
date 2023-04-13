using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;  // the prefab of the enemy to instantiate
    public float spawnInterval = 3f;  // the time interval between enemy spawns

    private float timer = 0f;

    public Enemy[] enemies;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            var randomNum = Random.Range(0, enemies.Length);
          var currentEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
            currentEnemy.GetComponent<EnemyController>().enemyScriptableObj = enemies[randomNum];

            timer = 0f;
        }
    }

}
