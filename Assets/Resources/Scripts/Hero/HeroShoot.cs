using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroShoot : MonoBehaviour {

    public string animName = "shot";
    public KeyCode shoot;
    public KeyCode reload;
    public Animator anim;

    public Image reloadShadow;
    public Text bulletLeftText;
    public Image reloadProgressShadow;
    public Image reloadWeapon;

    public GameObject gun;
    public GameObject bullet;
    public GameObject bulletPos;
    public GameMaster gm;
    public GameObject hero;


    public float damage;
    public float speed;
    public int durability = 1;
    public float disappearTime = 2f;
    public float direction = -180f;
    public float directionLimits;

    public float specialAbilityCooldown;
    public int xTimes = 1;

    public int bulletTypeNum;
    public int playerNum;
    public float cooldown;
    float _cooldown;

    public int amountOfBullets;
    public float timeGapBetweenBullets;
    public float reloadTime;
    public float _reloadTime = 0f;
    public int clipSize;
    int leftAmmo;

    public float damageModifier = 1f;
    public float movementSpeedModifier = 1f;
    public float cooldownModifier = 1f;


    //public bool[] stopShooting = new bool[] { false,false,false};
    public int abilityActiveCount = 0;

    void Start () {
        gun.SetActive(true);
        RefreshModifiers();
        if (Characters.characters[Informations.statistics[3]].specialBullet == true)
        {
            animName = "specialShot" + Informations.statistics[3];
        }
        else
        {
            animName = "shot";
        }
        GetHeroStats();
    }

	void Update () {


        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
        if(_reloadTime > 0f)
        {
            _reloadTime -= Time.deltaTime;
        }
        if ((Input.GetKey(shoot) && _cooldown <= 0f))
        {
            WeaponShot(amountOfBullets,timeGapBetweenBullets,reloadTime, clipSize);
        }

        if (Input.GetKey(reload))
        {
            _reloadTime = reloadTime;
            leftAmmo = clipSize;
        }
        reloadProgressShadow.fillAmount = (reloadTime - _reloadTime) / reloadTime;
        bulletLeftText.text = "" + leftAmmo;
        if (_reloadTime > 0f)
        {
            reloadProgressShadow.color = new Color32(200, 200, 255, 110);
            reloadShadow.color = new Color32(0, 0, 0, 220);
            reloadWeapon.color = new Color32(255, 255, 255, 150);
        }
        else
        {
            reloadProgressShadow.color = new Color32(255, 255, 255, 0);
            reloadShadow.color = new Color32(0, 0, 0, 0);
            reloadWeapon.color = new Color32(255, 255, 255, 255);
        }
    
    }


    public void Shoot()
    {

        if (Characters.characters[Informations.statistics[3]].specialBullet == false || Informations.statistics[2] > 0)
        {
            bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Hero/Bullet" + Informations.statistics[2]);
        }
        else if (Characters.characters[Informations.statistics[3]].specialBullet == true && Informations.statistics[2] == 0)
        {
            bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Special/Bullet" + Informations.statistics[3]);
        }
        bullet.transform.position = new Vector2(bulletPos.transform.position.x + Random.Range(-0.1f,0.1f), bulletPos.transform.position.y + Random.Range(-0.1f, 0.1f));
        bullet.GetComponent<BulletMobility>().damage = damage;
        bullet.GetComponent<BulletMobility>().speed = speed;

        anim.Play(animName);

        //laserSound.Play();
        FindObjectOfType<AudioManager>().Play("LaserSound0");

        Instantiate(bullet);


    }
    public void WeaponShot(int amount, float timeBetweenShots,float reloadingTime, int clipSize)
    {
        if (_reloadTime <= 0f && leftAmmo > 0 && abilityActiveCount == 0)
        {
            if (amount == 1)
            {
                Shoot();
            }
            else
            {
                for (int i = 0; i < amount; i++)
                {
                    float value = i * timeBetweenShots;
                    Invoke("Shoot", value);
                }
            }
            //GetComponent<HeroControl>().fatigue -= 0.4f * cooldown * Characters.weapons[Informations.statistics[3]].fatigue;
            _cooldown += cooldown * cooldownModifier + timeGapBetweenBullets;
            leftAmmo--;
        }
        else if(_reloadTime <= 0f && abilityActiveCount == 0)
        {
            _reloadTime = reloadTime;
            leftAmmo = clipSize;
        }
    }

    public void GetHeroStats()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Hero/Bullet" + Informations.statistics[2]);
        bulletTypeNum = Informations.statistics[3];
        damage = Characters.characters[Informations.statistics[3]].damage * Characters.charactersUpgrades.damage; // * bullet.GetComponent<BulletMobility>().damageModifier;
        speed = Characters.characters[Informations.statistics[3]].bulletSpeed; // * bullet.GetComponent<BulletMobility>().speedModifier;
        cooldown = Characters.characters[Informations.statistics[3]].cooldown * Characters.charactersUpgrades.cooldown ;
        //disappearTime = Characters.characters[Informations.statistics[3]].range;
        hero.GetComponent<HeroControl>().acceleration = Characters.characters[Informations.statistics[3]].movementSpeed;
        hero.GetComponent<HeroHealthScript>().maxHealth = Characters.characters[Informations.statistics[3]].health * Characters.charactersUpgrades.health;
        hero.GetComponent<HeroHealthScript>().health = hero.GetComponent<HeroHealthScript>().maxHealth;
        bulletTypeNum = Informations.statistics[3];
        playerNum = Informations.statistics[3];

        hero.GetComponent<HeroHealthScript>().RefreshBars();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + Informations.statistics[3]);

        reloadTime = Characters.weapons[Informations.statistics[3]].reloadTime * Characters.charactersUpgrades.cooldown;
        clipSize = Characters.weapons[Informations.statistics[3]].clipSize;
        timeGapBetweenBullets = Characters.weapons[Informations.statistics[3]].gapTime * Characters.charactersUpgrades.cooldown;
        amountOfBullets = Characters.weapons[Informations.statistics[3]].bulletsAtOnce;
        leftAmmo = clipSize;
    }
    public void RefreshStats()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Hero/Bullet" + Informations.statistics[2]);
        bulletTypeNum = Informations.statistics[3];
        damage = Characters.characters[Informations.statistics[3]].damage * Characters.charactersUpgrades.damage * damageModifier; // bullet.GetComponent<BulletMobility>().damageModifier 
        speed = Characters.characters[Informations.statistics[3]].bulletSpeed; // bullet.GetComponent<BulletMobility>().speedModifier;
        cooldown = Characters.characters[Informations.statistics[3]].cooldown * Characters.charactersUpgrades.cooldown * cooldownModifier;
        //disappearTime = Characters.characters[Informations.statistics[3]].range;
        hero.GetComponent<HeroControl>().acceleration = Characters.characters[Informations.statistics[3]].movementSpeed * movementSpeedModifier;
        hero.GetComponent<HeroHealthScript>().maxHealth = Characters.characters[Informations.statistics[3]].health * Characters.charactersUpgrades.health;
        bulletTypeNum = Informations.statistics[3];
        playerNum = Informations.statistics[3];

        hero.GetComponent<HeroHealthScript>().RefreshBars();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + Informations.statistics[3]);

        reloadTime = Characters.weapons[Informations.statistics[3]].reloadTime * Characters.charactersUpgrades.cooldown;
        clipSize = Characters.weapons[Informations.statistics[3]].clipSize;
        timeGapBetweenBullets = Characters.weapons[Informations.statistics[3]].gapTime * Characters.charactersUpgrades.cooldown;
        amountOfBullets = Characters.weapons[Informations.statistics[3]].bulletsAtOnce;
    }
    public void RefreshModifiers()
    {
        cooldownModifier = 1f;
        damageModifier = 1f;
        movementSpeedModifier = 1f;
    }

}
