using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManaAndAbilities : MonoBehaviour
{

    public Image[] abilityCooldownImages;
    public Image[] abilityCooldownImagesShadow;
    public Text[] abilityCooldownsTimeNum;
    public Image[] abilityImages;
    public TrailRenderer trail;
    HeroShoot heroShoot;

    public Text manaText;

    public Image manaBar;
    public Image lostManaBar;
    public float timeToDisappearLostMP;

    public GameObject bullet;
    public GameObject bulletPosition;

    public float maxMana;
    public float mana;
    public float manaRegeneration = 1.5f;

    float[] abilityTimeLeft = new float[] { 0f, 0f };
    bool[] abilityDone = new bool[] { false, false };
    float[] abilityFloat = new float[2];

    int[] switchNum = new int[3];
    public float[] cooldown = new float[3]
    {
        0f,0f,0f
    };
    public KeyCode[] abilityButtons;
    void Start()
    {
        heroShoot = GetComponent<HeroShoot>();
        lostManaBar.color = new Color32(230, 0, 150, 200);
        abilityImages[0].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + Informations.actualAbility[0]);
        abilityImages[1].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + Informations.actualAbility[1]);
        manaRegeneration = Characters.characters[Informations.statistics[3]].manaRegeneration;
        maxMana = Characters.characters[Characters.characterStatsNum].mana * Characters.charactersUpgrades.mana;
        mana = maxMana;
        RefreshBars();
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            if (abilityFloat[i] > 0f)
            {
                abilityFloat[i] -= Time.deltaTime;
            }
        }
        for (int i = 0; i < 2; i++)
        {
            if (abilityTimeLeft[i] > 0f)
            {
                abilityTimeLeft[i] -= Time.deltaTime;
            }
            else if (abilityDone[i] == false)
            {
                if (i == 1)
                {
                    trail.gameObject.SetActive(false);
                    GetComponent<HeroShoot>().RefreshAbilityModifiers();
                    GetComponent<HeroShoot>().RefreshStats();
                }
                GetComponent<HeroShoot>().stopShooting[i] = false;
                abilityDone[i] = true;
            }
        }
        if (timeToDisappearLostMP > 0f)
        {
            timeToDisappearLostMP -= Time.deltaTime;
        }
        else if (lostManaBar.fillAmount > mana / maxMana)
        {
            lostManaBar.color = new Color32(230, 0, 150, 200);
            lostManaBar.fillAmount -= 0.005f;
        }
        else if (lostManaBar.fillAmount < mana / maxMana)
        {
            lostManaBar.fillAmount = Mathf.Clamp(Mathf.Round((mana / maxMana) * 100f), 0f, 100f) / 100f;
            manaBar.fillAmount = Mathf.Clamp(Mathf.Round((mana / maxMana) * 100f), 0f, 100f) / 100f;
        }
        if (mana < maxMana)
        {
            mana = Mathf.Clamp(mana + (manaRegeneration * Informations.upgradesAmount.manaMultiplier[Informations.upgrades[2]] * Time.deltaTime), 0f, maxMana);
            RefreshBars();
        }
        for (int i = 0; i < 2; i++)
        {
            if (cooldown[i] >= 0f)
            {
                cooldown[i] -= Time.deltaTime;
            }
            if (cooldown[i] <= 0)
            {
                abilityCooldownsTimeNum[i].color = new Color32(255, 255, 255, 0);
            }
            abilityCooldownsTimeNum[i].text = "" + System.Math.Round(cooldown[i], 1);
            if (i == 1)
            {
                abilityCooldownImages[i].fillAmount = (Informations.abilityCooldown[Informations.actualAbility[i]] - cooldown[i]) / Informations.abilityCooldown[Informations.actualAbility[i]];
            }
            else
            {
                abilityCooldownImages[i].fillAmount = (Informations.spellCooldown[Informations.actualAbility[i]] - cooldown[i]) / Informations.spellCooldown[Informations.actualAbility[i]];
            }

            if (cooldown[i] > 0f)
            {
                abilityCooldownImages[i].color = new Color32(200, 200, 255, 110);
                abilityCooldownImagesShadow[i].color = new Color32(0, 0, 0, 220);
                abilityImages[i].color = new Color32(255, 255, 255, 150);
            }
            else
            {
                abilityCooldownImages[i].color = new Color32(255, 255, 255, 0);
                abilityCooldownImagesShadow[i].color = new Color32(0, 0, 0, 0);
                abilityImages[i].color = new Color32(255, 255, 255, 255);
            }
        }
        if (Input.GetKeyDown(abilityButtons[0]) && mana >= Informations.spellManaCost[Informations.actualAbility[0]] && cooldown[0] <= 0f)
        {
            UseMana(Informations.spellManaCost[Informations.actualAbility[0]]);
            cooldown[0] = Informations.spellCooldown[Informations.actualAbility[0]];
            abilityCooldownsTimeNum[0].color = new Color32(255, 255, 255, 255);
            abilityDone[0] = false;
            if (timeToDisappearLostMP <= 0f && manaBar.fillAmount >= lostManaBar.fillAmount)
            {
                timeToDisappearLostMP = 0.75f;
            }
            switch (Informations.actualAbility[0])
            {
                case 0:
                    MysticKnife();
                    break;
                case 1:
                    RageAttack();
                    break;
                case 2:
                    ExplosiveShot();
                    break;
            }
        }
        else if (Input.GetKeyDown(abilityButtons[0]) && cooldown[0] <= 0f)
        {
            lostManaBar.fillAmount = Informations.spellManaCost[Informations.actualAbility[0]] / maxMana;
            timeToDisappearLostMP = 0.5f;
        }
        if (Input.GetKeyDown(abilityButtons[1]) && mana >= Informations.abilityManaCost[Informations.actualAbility[1]] && cooldown[1] <= 0f && GetComponent<HeroSpecialAbility>().timeLeft <= 0f)
        {
            UseMana(Informations.abilityManaCost[Informations.actualAbility[1]]);
            cooldown[1] = Informations.abilityCooldown[Informations.actualAbility[1]];
            abilityCooldownsTimeNum[1].color = new Color32(255, 255, 255, 255);
            abilityDone[1] = false;
            if (timeToDisappearLostMP <= 0f && manaBar.fillAmount >= lostManaBar.fillAmount)
            {
                timeToDisappearLostMP = 0.75f;
            }
            switch (Informations.actualAbility[1])
            {
                case 0:
                    SpeedUp();
                    break;
                case 1:
                    AutoShoot();
                    break;
                case 2:
                    KniveCircle();
                    break;
            }
        }
        else if (Input.GetKeyDown(abilityButtons[1]) && cooldown[1] <= 0f)
        {
            lostManaBar.fillAmount = Informations.abilityManaCost[Informations.actualAbility[1]] / maxMana;
            timeToDisappearLostMP = 0.5f;
        }
    }
    public void RestoreMana(int amount)
    {
        mana = Mathf.Clamp(mana + amount, 0f, maxMana);
        RefreshBars();
    }
    public void UseMana(float amount)
    {
        mana = Mathf.Clamp(mana - amount,0f,maxMana);
    }
    public void RefreshBars()
    {
        manaBar.fillAmount = Mathf.Round((mana / maxMana) * 100f) / 100f;
        manaText.text = "" + (int)mana;
    }
    // Spells
    public void MysticKnife()
    {
        for (float i = -45f; i <= 45f; i += 45f)
        {
            BulletStats("Bullets_1Spell/Bullet0", 8f, 20f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, i);
            Instantiate(bullet);
        }
        BulletStats("Bullets_1Spell/Bullet0", 8f, 20f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, 180);
        Instantiate(bullet);
        FindObjectOfType<AudioManager>().Play("LaserSound0");
    }
    public void RageAttack()
    {
        //abilityTimeLeft[0] = 2.5f;
        GetComponent<HeroShoot>().stopShooting[0] = true;
        for (float i = 0f; i < 2.5f; i += 0.25f)
        {
            Invoke("RageShots", i);
        }

    }
    public void RageShots()
    {
        FindObjectOfType<AudioManager>().Play("LaserSound0");
        for (int i = 0; i < 3; i++)
        {
            BulletStats("Bullets_1Spell/Bullet1", 10f, 2f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, Random.Range(-40f, 40f));
            Instantiate(bullet);
        }
    }
    public void ExplosiveShot()
    {
        FindObjectOfType<AudioManager>().Play("LaserSound0");
        BulletStats("Bullets_1Spell/Bullet2", 2f, 10f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 2f, 1, 0);
        Instantiate(bullet);
    }
    //Abilities
    public void SpeedUp()
    {
        trail.gameObject.SetActive(true);
        abilityTimeLeft[1] = 5f;
        GetComponent<HeroShoot>().abilityCooldownModifier = 0.4f;
        GetComponent<HeroShoot>().abilityMovementSpeedModifier = 1.2f;
        GetComponent<HeroShoot>().RefreshStats();
        GetComponent<HeroShoot>().Shoot();
    }
    public void AutoShoot()
    {
        if (abilityDone[1] == false)
        {
            abilityTimeLeft[1] = 5f;
            abilityDone[1] = true;
        }
        if (abilityTimeLeft[1] > 0.5f)
        {
            Invoke("AutoShoot", 0.5f);
        }
        BulletStats("Bullets_Ability/Bullet0", 6f, 5f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, Random.Range(0f, 0f));
        Instantiate(bullet);
        FindObjectOfType<AudioManager>().Play("LaserSound0");

    }
    public void KniveCircle()
    {
        FindObjectOfType<AudioManager>().Play("LaserSound0");
        for (int i = 0;i < 6; i += 2)
        {
            Invoke("KniveCircleShot0", i);
        }
        for (int i = 1; i < 5; i += 2)
        {
            Invoke("KniveCircleShot1", i);
        }

    }
    public void KniveCircleShot0()
    {
        for(int i = 0; i < 360; i += 60)
        {
            BulletStats("Bullets_Ability/Bullet2", 6f, 10f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, i);
            Instantiate(bullet);
        }
    }
    public void KniveCircleShot1()
    {
        for (int i = -30; i < 330; i += 60)
        {
            BulletStats("Bullets_Ability/Bullet2", 6f, 10f * Characters.charactersUpgrades.damage * Characters.characters[Informations.statistics[3]].magicDamageMultiplier, 4f, 1, i);
            Instantiate(bullet);
        }
    }
    public void BulletStats(string num, float _speed, float _dmg, float _disappearTime, int _durability, float direction)
    {
        RefreshBars();
        bullet = Resources.Load<GameObject>("Prefabs/" + num);
        bullet.GetComponent<BulletMobility>().damage = _dmg;
        bullet.GetComponent<BulletMobility>().speed = _speed;
        //bullet.GetComponent<BulletMobility>().disappearTime = _disappearTime;
        bullet.GetComponent<BulletMobility>().durability = _durability;
        bullet.transform.position = bulletPosition.transform.position;
        bullet.GetComponent<BulletMobility>().direction = direction;
    }
}
