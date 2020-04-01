using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMobility : MonoBehaviour {

    public float damage = 0f;
    public float speed = 0f;
    public float direction = 0f;
    public int durability = 0;
    public float disappearTime = 0f;

    public float damageModifier = 1f;
    public float speedModifier = 1f;
    public float cooldownModifier = 1f;

    public bool tracking;
    public bool speedingUp;
    float speedingUpValue = 1f;
    public float speedingUpJumpValue;
    public GameObject nearestEnemy;
    public float minDist = Mathf.Infinity;
    public bool explosive = false;
    public bool explosion = false;
    public int effect;

    GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        speedingUpValue = 1f;
        if (tracking == true)
        {
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

        transform.rotation =  Quaternion.Euler(0, 0, direction);
    }

    void Update () {
        disappearTime -= Time.deltaTime;

        if (disappearTime <= 0 && explosive != true)
        {
            Destroy(this.gameObject);
            
        }
        if (durability <= 0 && explosive != true && explosion != true)
        {
            Destroy(this.gameObject);
        }
        if(speedingUp == true)
        {
            speedingUpValue = Mathf.Clamp(speedingUpValue + speedingUpJumpValue, 1f, 3f);
        }
        if (tracking == false)
        {
            transform.position += transform.TransformDirection(Vector3.right * speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * speedingUpValue);
        }
        else if(minDist < Mathf.Infinity && nearestEnemy != null)
        {
            if (transform.position.x < nearestEnemy.transform.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.5f * speedingUpValue);
                transform.position += transform.TransformDirection(Vector3.right * speed * Time.deltaTime * 0.5f * speedingUpValue);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.25f * speedingUpValue);
                transform.position += transform.TransformDirection(Vector3.right * speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * 0.75f * speedingUpValue);
            }
        }
        else
        {
            minDist = Mathf.Infinity;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            transform.position += transform.TransformDirection(Vector3.right * speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * speedingUpValue);
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
    void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "enemy" || _tag == "boss")
        {
            durability--;
            if (durability <= 0 && explosive != true && explosion != true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
