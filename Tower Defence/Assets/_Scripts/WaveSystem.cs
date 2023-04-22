using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyHolder;
        public Enemy[] enemys;
        public Enemy[] bigEnemys;
        public int numEnemies;
        public float spawnDelay;
        public float bigEnemyRatio = 0.2f;
    }

    public Wave[] waves;

    public int currentWaveIndex = 0;
    public int roundNumber;
   
    public TextMeshProUGUI roundText;

    public Animator anim;
    public Animator roundAnim;

    private bool spawningWave = false;
    private bool canSpawn = false;
    public TextMeshProUGUI endRoundsText;
    private float noEnemyTime = 0f;
    private float timeThreshold = 1.5f;
    Enemy enemyPrefab = null;

    public AudioClip finishRoundSound;


    bool gaveReward = false;
    bool canclick = true;
    bool canContinueToNextWave = true;
    int currentWave = 0;

    IEnumerator SpawnWave(Wave wave)
    {
        if (canContinueToNextWave == false)
        {
            yield break;
        }

        UpdateRoundText();
        canclick = false;
        spawningWave = true;

        int spawnCount = 0;
        int numBigEnemies = Mathf.RoundToInt(wave.numEnemies * wave.bigEnemyRatio);

        yield return new WaitForSeconds(1f);

        while (spawnCount < wave.numEnemies)
        {
            if (spawnCount % 2 == 0 && numBigEnemies > 0 && wave.bigEnemys != null && wave.bigEnemys.Length > 0)
            {
                enemyPrefab = wave.bigEnemys[Random.Range(0, wave.bigEnemys.Length)];
                numBigEnemies--;
            }
            else
            {
                enemyPrefab = wave.enemys[Random.Range(0, wave.enemys.Length)];
            }

            Instantiate(wave.enemyHolder, transform.position, Quaternion.identity).GetComponent<EnemyController>().enemyScriptableObj = enemyPrefab;
            spawnCount++;

            yield return new WaitForSeconds(wave.spawnDelay);
        }

        spawningWave = false;
        canSpawn = false;
        canContinueToNextWave = false;
        currentWaveIndex++;
    }


    void Update()
    {
        if (!spawningWave && currentWaveIndex < waves.Length && canSpawn)
        {
            StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        }

        if(currentWaveIndex == waves.Length && !CheckIfEnemys()) { StartCoroutine(WinGame()); }



        if(currentWave < currentWaveIndex && !CheckIfEnemys()) { EndOfWave(); currentWave = currentWaveIndex; }

        //check if wave ended




        //figure out when the round ends
        //the check if enemy needs to last a few seconds just in case its inbetween spawning enemies



      
        UpdateRoundsText();
    }
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3); 
        Actions.OnWin();
    }

    bool CheckIfEnemys()
    {
        //if there are enemys return true
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            noEnemyTime = 0f; // reset timer if enemies are present
            return true;
        }
        else
        {
            noEnemyTime += Time.deltaTime; // add to timer if no enemies

            if (noEnemyTime >= timeThreshold) // check if threshold is reached
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    public void EndOfWave()
    {
        TutorialManager.Instance.OnWaitTillWaveEnd();

        canclick = true;

        canContinueToNextWave = true;
            Actions.OnWaveEnded();

        AudioManager.Instance.PlaySound(finishRoundSound);
        roundAnim.SetTrigger("NewRound");
        UpdateRoundText();


    }

    void UpdateRoundText()
    {
        roundText.text = currentWaveIndex.ToString();
    }

   public void StartSpawning() => canSpawn = true;

    private void UpdateRoundsText()
    {
        endRoundsText.text =  " You Survived " + currentWaveIndex + " rounds";
    }

    public void AnimCheck()
    {
        bool played = true;

        if(canclick == false && played == true)
        {
            anim.SetTrigger("Error");
            played = false;

        }
        else if (canclick == true && played == true)
        {
            anim.SetTrigger("Buy");

            played = false;

        }

    }





}
