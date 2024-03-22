using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillResurrection : MonoBehaviour
{
    public ParticleSystem effect;
    void Awake()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().OnDeath += SpawnZombie;
            //ParticleSystem particleSystem = enemy.AddComponent<ParticleSystem>();
        }
    }

    void SpawnZombie(GameObject enemyObject)
    {
        GameObject zombiePrefab = Resources.Load<GameObject>("Prefabs/Enemies/Enemy801");
        Instantiate(zombiePrefab, enemyObject.transform.position, new Quaternion());
    }

    void OnDestroy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyHealth>().OnDeath -= SpawnZombie;
            Destroy(enemy.GetComponent<ParticleSystem>());
        }
    }
}
