using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {

    public float startingcooldown;
    public float cooldown;
    float _cooldown;
    public float speed;
    public float precision;
    public float damage;

    public float cooldownchangesMin;
    public float cooldownchangesMax;
    float cooldownchanges;
    public float cooldownTimeChanger = 1f;
    public float speedChanger = 1f;
    public GameObject spawnPosition;

    public string bulletKind;
    public bool sniperMode;
    public float sniperModeCooldownTimer;
    public float sniperModeRange;
    bool colorBack;

    public GameObject animStartLook;
    public float cooldownAnim;
    float _cooldownAnim;
    bool animLookRefreshed;
    public bool animActive;
    public bool multipleBullets;
    public float[] directions;
    public bool chaoticBulletsDirections;
    public float range;
    public int amount;

    GameObject hero;
	// Use this for initialization
	void Start () {
        cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
        hero = GameObject.FindGameObjectWithTag("player");
        cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
        _cooldown = startingcooldown + cooldownchanges;
    }
	
	// Update is called once per frame
	void Update () {

        if (_cooldownAnim > 0)
        {
            GetComponent<EnemyMobility>().actualSpeed = GetComponent<EnemyMobility>().wantedSpeed * 3f;
            _cooldownAnim -= Time.deltaTime;
        }
        else if (animLookRefreshed == false && animActive == true)
        {
            GetComponent<EnemyMobility>().actualSpeed = GetComponent<EnemyMobility>().basicSpeed;
            animStartLook.SetActive(true);
        }
        if (sniperMode == true)
        {
            if(hero.transform.position.y >= transform.position.y - sniperModeRange && hero.transform.position.y <= transform.position.y + sniperModeRange)
            {
                colorBack = false;
                GetComponent<SpriteRenderer>().color = new Color32(255, 150, 150, 255);
                _cooldown -= Time.deltaTime * (sniperModeCooldownTimer - 1);
            }
            else if(colorBack == false)
            {
                colorBack = true;
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            }
        }
        if (_cooldown >= 0f)
        {
            _cooldown -= Time.deltaTime * cooldownTimeChanger;
        }
        if (_cooldown <= 0f)
        {
            if (multipleBullets == false && chaoticBulletsDirections == false)
            {
                ShootBullet();
            }
            else if ( chaoticBulletsDirections == false)
            {
                ShootBullets(amount,directions);
            }
            else
            {
                ShootBulletsChaotic(amount,range);
            }

            cooldownchanges = Random.Range(cooldownchangesMin, cooldownchangesMax);
            _cooldown = cooldown + cooldownchanges;

        }
	}
    public void ShootBullet()
    {
        if (animActive == true)
        {
            _cooldownAnim = cooldownAnim;
            animStartLook.SetActive(false);
            animLookRefreshed = false;
        }
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets_Enemy/Bullet" + bulletKind);
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
            if (animActive == true)
            {
                _cooldownAnim = cooldownAnim;
                animStartLook.SetActive(false);
                animLookRefreshed = false;
            }
            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets_Enemy/Bullet" + bulletKind);
            bullet.GetComponent<EnemyBulletMobility>().speed = speed;
            bullet.GetComponent<EnemyBulletMobility>().damage = damage;
            bullet.GetComponent<EnemyBulletMobility>().precision = directions[i];
            bullet.transform.position = spawnPosition.transform.position;
            Instantiate(bullet);
        }
    }
    public void ShootBulletsChaotic(int amount,float range)
    {
        for (int i = 0; i < amount; i++)
        {
            if (animActive == true)
            {
                _cooldownAnim = cooldownAnim;
                animStartLook.SetActive(false);
                animLookRefreshed = false;
            }
            GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets_Enemy/Bullet" + bulletKind);
            bullet.GetComponent<EnemyBulletMobility>().speed = speed;
            bullet.GetComponent<EnemyBulletMobility>().damage = damage;
            bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(Mathf.Clamp(-range, -180f, 0), Mathf.Clamp(range, 0, 180));
            bullet.transform.position = spawnPosition.transform.position;
            Instantiate(bullet);
        }
    }

}
