using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public float startingcooldown;
    public float cooldown;
    public float _cooldown;
    public float speed;
    public float precision;
    public float damage;

    public float cooldownchangesMin;
    public float cooldownchangesMax;
    float cooldownchanges;
    public float cooldownTimeChanger = 1f;
    public GameObject spawnPosition;

    public string bulletKind;

    public float[] directions;
    public bool chaoticBulletsDirections;
    public float range;
    public int amount;

    GameObject hero;
	
	void Start () {
        hero = GameObject.FindGameObjectWithTag("player");
        cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
        _cooldown = startingcooldown + cooldownchanges;
    }
	
	void Update () {


        if (_cooldown >= 0f)
        {
            _cooldown -= Time.deltaTime * cooldownTimeChanger;
        }
        if (_cooldown < 0f)
        {
            if (amount <= 0)
            {
                ShootBullet();

            }
            else if (amount > 1 && chaoticBulletsDirections == false)
            {
                ShootBullets(amount,directions);

            }
            else if (amount > 1 && chaoticBulletsDirections == true)
            {
                ShootBulletsChaotic(amount,range);
            }
            else
            {
                Debug.Log("Wrong input data in EnemyShooting");
            }

            cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
            _cooldown = cooldown + cooldownchanges;

        }
	}
    public void ShootBullet()
    {

        GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet" + bulletKind);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        bullet.GetComponent<EnemyBulletMobility>().damage = damage;
        bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(Mathf.Clamp(-precision, -180f, 0),Mathf.Clamp(precision, 0, 180));
        bullet.transform.position = spawnPosition.transform.position;
        FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
        Instantiate(bullet);

    }
    public void ShootBullets(int amount,float[] directions)
    {
        for(int i = 0;i < amount; i++)
        {
            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet" + bulletKind);
            bullet.GetComponent<EnemyBulletMobility>().speed = speed;
            bullet.GetComponent<EnemyBulletMobility>().damage = damage;
            bullet.GetComponent<EnemyBulletMobility>().precision = directions[i];
            bullet.transform.position = spawnPosition.transform.position;
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            Instantiate(bullet);
        }
    }
    public void ShootBulletsChaotic(int amount,float range)
    {
        for (int i = 0; i < amount; i++)
        {

            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet" + bulletKind);
            bullet.GetComponent<EnemyBulletMobility>().speed = speed;
            bullet.GetComponent<EnemyBulletMobility>().damage = damage;
            bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(Mathf.Clamp(-range, -180f, 0), Mathf.Clamp(range, 0, 180));
            bullet.transform.position = spawnPosition.transform.position;
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            Instantiate(bullet);
        }
    }

}
