using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShoot : MonoBehaviour {

    string animName = "shot";
    public KeyCode shoot;
    public Animator anim;

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
    public float cooldownModifier;
    float _cooldown;

    public float abilityDamageModifier = 1f;
    public float abilityMovementSpeedModifier = 1f;
    public float abilityCooldownModifier = 1f;

    public float specialAbilityDamageModifier = 1f;
    public float specialAbilityMovementSpeedModifier = 1f;
    public float specialAbilityCooldownModifier = 1f;

    public bool[] stopShooting = new bool[] { false,false,false};

    void Start () {
        gun.SetActive(true);
        RefreshAbilityModifiers();
        RefreshSpecialAbilityModifiers();
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
        if ((Input.GetKey(shoot) && _cooldown <= 0f))
        {
            Shoot();
        }
    }


    public void Shoot()
    {
        if (stopShooting[0] == false && stopShooting[1] == false && stopShooting[2] == false )
        {
            for (int i = 0; i < xTimes; i++)
            {
                if (Characters.characters[Informations.statistics[3]].specialBullet == false || Informations.statistics[2] > 0)
                {
                    bullet = Resources.Load<GameObject>("Prefabs/Bullets_Hero/Bullet" + Informations.statistics[2]);
                    bullet.GetComponent<BulletMobility>().direction = direction;
                }
                else if (Characters.characters[Informations.statistics[3]].specialBullet == true && Informations.statistics[2] == 0)
                {
                    bullet = Resources.Load<GameObject>("Prefabs/Bullets_Special/Bullet" + Informations.statistics[3]);
                }
                bullet.transform.position = bulletPos.transform.position;
                bullet.GetComponent<BulletMobility>().damage = damage * bullet.GetComponent<BulletMobility>().damageModifier;
                bullet.GetComponent<BulletMobility>().speed = speed * bullet.GetComponent<BulletMobility>().speedModifier;
                //bullet.GetComponent<BulletMobility>().durability = durability;
                //bullet.GetComponent<BulletMobility>().disappearTime = disappearTime;

                anim.Play(animName);

                //FindObjectOfType<AudioManager>().Play("LaserSound" + Informations.statistics[3]);

                FindObjectOfType<AudioManager>().Play("LaserSound0");

                Instantiate(bullet);
            }
            _cooldown = cooldown * bullet.GetComponent<BulletMobility>().cooldownModifier;
        }
    }
    public void GetHeroStats()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Hero/Bullet" + Informations.statistics[2]);
        bulletTypeNum = Informations.statistics[3];
        damage = Characters.characters[Informations.statistics[3]].damage * Characters.charactersUpgrades.damage * bullet.GetComponent<BulletMobility>().damageModifier;
        speed = Characters.characters[Informations.statistics[3]].bulletSpeed * bullet.GetComponent<BulletMobility>().speedModifier;
        cooldown = Characters.characters[Informations.statistics[3]].cooldown * Characters.charactersUpgrades.cooldown * bullet.GetComponent<BulletMobility>().cooldownModifier;
        //disappearTime = Characters.characters[Informations.statistics[3]].range;
        hero.GetComponent<HeroControl>().acceleration = Characters.characters[Informations.statistics[3]].movementSpeed;
        hero.GetComponent<HeroHealthScript>().maxHealth = Characters.characters[Informations.statistics[3]].health * Characters.charactersUpgrades.health;
        hero.GetComponent<HeroHealthScript>().health = hero.GetComponent<HeroHealthScript>().maxHealth;
        bulletTypeNum = Informations.statistics[3];
        playerNum = Informations.statistics[3];

        hero.GetComponent<HeroHealthScript>().RefreshBars();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + Informations.statistics[3]);
    }
    public void RefreshStats()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Hero/Bullet" + Informations.statistics[2]);
        bulletTypeNum = Informations.statistics[3];
        damage = Characters.characters[Informations.statistics[3]].damage * Characters.charactersUpgrades.damage * bullet.GetComponent<BulletMobility>().damageModifier * abilityDamageModifier * specialAbilityDamageModifier;
        speed = Characters.characters[Informations.statistics[3]].bulletSpeed * bullet.GetComponent<BulletMobility>().speedModifier;
        cooldown = Characters.characters[Informations.statistics[3]].cooldown * Characters.charactersUpgrades.cooldown * bullet.GetComponent<BulletMobility>().cooldownModifier * abilityCooldownModifier * specialAbilityCooldownModifier;
        //disappearTime = Characters.characters[Informations.statistics[3]].range;
        hero.GetComponent<HeroControl>().acceleration = Characters.characters[Informations.statistics[3]].movementSpeed  * abilityMovementSpeedModifier * specialAbilityMovementSpeedModifier;
        hero.GetComponent<HeroHealthScript>().maxHealth = Characters.characters[Informations.statistics[3]].health * Characters.charactersUpgrades.health;
        bulletTypeNum = Informations.statistics[3];
        playerNum = Informations.statistics[3];

        hero.GetComponent<HeroHealthScript>().RefreshBars();
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + Informations.statistics[3]);

    }
    public void RefreshAbilityModifiers()
    {
        abilityCooldownModifier = 1f;
        abilityDamageModifier = 1f;
        abilityMovementSpeedModifier = 1f;
    }
    public void RefreshSpecialAbilityModifiers()
    {
        specialAbilityCooldownModifier = 1f;
        specialAbilityDamageModifier = 1f;
        specialAbilityMovementSpeedModifier = 1f;
    }

}
