using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTracking : MonoBehaviour
{
    GameMaster gm;
    public GameObject nearestEnemy;
    public float minDist = Mathf.Infinity;
    BulletMobility bulletMobility;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        bulletMobility = GetComponent<BulletMobility>();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("boss");
        for (int i = 0; i < enemies.Length; i++)
        {
            float dist = Vector2.Distance(transform.position, enemies[i].transform.position);
            if (dist < minDist)
            {
                nearestEnemy = enemies[i];
                minDist = dist;
            }
        }
        if (boss != null)
        {
            if (Vector2.Distance(transform.position, boss.transform.position) < minDist)
            {
                nearestEnemy = boss;
                minDist = Vector2.Distance(transform.position, boss.transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (minDist < Mathf.Infinity && nearestEnemy != null)
        {
            if (transform.position.x < nearestEnemy.transform.position.x)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    nearestEnemy.transform.position,
                    bulletMobility.speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.5f * bulletMobility.speedingUpValue
                );
                transform.position += transform.TransformDirection(Vector3.right * bulletMobility.speed * Time.deltaTime * 0.5f * bulletMobility.speedingUpValue);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, bulletMobility.speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.25f * bulletMobility.speedingUpValue);
                transform.position += transform.TransformDirection(Vector3.right * bulletMobility.speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.75f * bulletMobility.speedingUpValue);
            }
        }
        else
        {
            minDist = Mathf.Infinity;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            transform.position += transform.TransformDirection(Vector3.right * bulletMobility.speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * bulletMobility.speedingUpValue);
            for (int i = 0; i < enemies.Length; i++)
            {
                float dist = Vector3.Distance(transform.position, enemies[i].transform.position);
                if (dist < minDist)
                {
                    nearestEnemy = enemies[i];
                    minDist = dist;
                }
            }
        }
    }
}
