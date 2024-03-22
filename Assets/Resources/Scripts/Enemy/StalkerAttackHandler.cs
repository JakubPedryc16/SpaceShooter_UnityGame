using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerAttackHandler : MonoBehaviour
{

    GameObject bullet;
    BulletLoader bulletLoader;

    public float startingcooldown;
    public float cooldown;
    public float _cooldown;
    public float speed;
    public float precision;
    public float damage;

    public float cooldownchangesMin;
    public float cooldownchangesMax;
    float cooldownchanges;
    public GameObject spawnPosition;

    public string bulletKind;


    public float[] directions;
    //public bool chaoticBulletsDirections;
    public float range;
    public int amount;


    public string[] attackNames =
    {
        "SingleShot",
        "MultipleShot",
        "ChaoticShot",
        "ShotSeries",
        "SingleShotSeries",
        "CircleShot",
    };

    GameObject hero;
    // Use this for initialization
    void Start()
    {
        bulletLoader = new BulletLoader(damage: damage, speed: speed);
        hero = GameObject.FindGameObjectWithTag("player");
        cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
        _cooldown = startingcooldown + cooldownchanges;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldown >= 0f)
        {
            _cooldown -= Time.deltaTime;
        }
        if (_cooldown < 0f)
        {
            BulletsStats();
            
            Invoke(attackNames[Random.Range(0, attackNames.Length)], 0f);

            cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
            _cooldown = cooldown + cooldownchanges;

        }
    }

    public void SingleShot()
    {
        ShotAttack(0);
    }
    public void MultipleShot()
    {
    ShotAttack(1);
    }
    public void ChaoticShot()
    {
        ShotAttack(2);
    }
    public void ShotSeries()
    {
        Invoke("MultipleShot", 0f);
        Invoke("MultipleShot", 1f);
        Invoke("ChaoticShot", 2f);
    }
    public void SingleShotSeries()
    {
        for (int i = 0; i < amount * 2; i++)
        {
            Invoke("SingleShot", i);
        }
    }
    public void CircleShot()
    {
        ShotAttack(3);
    }
    public void ShotAttack(int typeOfAttack)
    {
        bullet = bulletLoader.Load(bulletKind);
        switch (typeOfAttack)
        {
            case 0: //single
                bullet.GetComponent<EnemyBulletMobility>().speed = 2*speed;
                bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(Mathf.Clamp(-precision, -180f, 0), Mathf.Clamp(precision, 0, 180));
                bullet.transform.position = spawnPosition.transform.position;
                FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
                Instantiate(bullet);
                break;
            case 1: //multiple
                for (int i = 0; i < amount; i++)
                {
                    bullet.GetComponent<EnemyBulletMobility>().precision = directions[i];
                    bullet.transform.position = spawnPosition.transform.position;
                    Instantiate(bullet);
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
                }
                break;
            case 2: //multiple + random directions
                for (int i = 0; i < amount; i++)
                {
                    bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(Mathf.Clamp(-range, -180f, 0), Mathf.Clamp(range, 0, 180));
                    bullet.transform.position = spawnPosition.transform.position;
                    Instantiate(bullet);
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
                }
                break;
            case 3:
                for (float i = -180; i < 180; i += 360 / (3 * amount))
                {
                    bullet.GetComponent<EnemyBulletMobility>().precision = i;
                    bullet.transform.position = spawnPosition.transform.position;
                    Instantiate(bullet);
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
                }
                break;
        }
    }
    public void BulletsStats()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet" + bulletKind);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        bullet.GetComponent<EnemyBulletMobility>().damage = damage;

    }
}


