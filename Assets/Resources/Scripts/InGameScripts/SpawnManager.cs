using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {


    public GameObject YouWonObject;
    public GameObject YouLostObject;
    public float timeAfterWave;

    public int waveNum = 0;
    public int enemyToSpawn;
    public int enemiesLeft;
    public float _cooldown;

    public bool effectorSpawned = false;

    [System.Serializable]
    public class EnemyKinds
    {
        public int type = 0;
        public float spawnChance = 0f;
    }

    [System.Serializable]
    public class Wave
    {
        public EnemyKinds[] enemies;
        public int amount = 0;
        public float cooldown = 0f;
        public bool bossWave = false;
        public int bossType;
    }

    public Wave[] waves;

    void Start()
    {
        _cooldown = timeAfterWave * Informations.difficultyStats[Informations.statistics[5]].waveCooldownsMultiplier;
        enemiesLeft = waves[waveNum].amount;
    }

    // Update is called once per frame
    void Update()
    {

        if (_cooldown >= 0f)
        {
            _cooldown -= Time.deltaTime;
        }

        if (YouWonObject == null && YouLostObject == null)
        {
            if (waves[waveNum].bossWave == false)
            {
                if (_cooldown <= 0f && enemiesLeft > 0 && waves[waveNum].bossWave == false)
                {
                    float x = 0;
                    float chance = Random.Range(1, 101);
                    for (int i = 0; i < waves[waveNum].enemies.Length; i++)
                    {
                        x += waves[waveNum].enemies[i].spawnChance;
                        if (chance <= x)
                        {
                            enemyToSpawn = waves[waveNum].enemies[i].type;
                            break;
                        }
                    }

                    //float position = Random.Range(-3.9f, 3.9f);
                    float position = Random.Range(-3.5f, 3.5f);
                    GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy" + enemyToSpawn);
                    enemy.transform.position = new Vector3(8.6f, position);
                    Instantiate(enemy);
                    enemiesLeft--;
                    _cooldown = waves[waveNum].cooldown;
                }
            }

            else if (enemiesLeft > 0 && _cooldown <= 0f)
            {

                float position = Random.Range(-3.9f, 3.9f);
                GameObject boss = Resources.Load<GameObject>("Prefabs/Bosses/Boss" + waves[waveNum].bossType);
                boss.transform.position = new Vector3(8.7f, position);
                Instantiate(boss);
                enemiesLeft--;
            }
            if (enemiesLeft <= 0)
            {
                GameObject[] enemiesLeftOnBoard;
                GameObject[] bossesOnTheBoard;
                enemiesLeftOnBoard = GameObject.FindGameObjectsWithTag("enemy");
                bossesOnTheBoard = GameObject.FindGameObjectsWithTag("boss");

                if (enemiesLeftOnBoard.Length == 0 && bossesOnTheBoard.Length == 0)
                {
                    //bossSpawned = false;
                    if (waveNum + 1 >= waves.Length && GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().dontUPauseIt == false)
                    {
                        GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().YouWin();
                    }
                    else if(waveNum + 1 < waves.Length)
                    {
                        waveNum++;
                        enemiesLeft = waves[waveNum].amount;
                        _cooldown = timeAfterWave * Informations.difficultyStats[Informations.statistics[5]].waveCooldownsMultiplier;
                    }
                }
            }
        }
    }

}
