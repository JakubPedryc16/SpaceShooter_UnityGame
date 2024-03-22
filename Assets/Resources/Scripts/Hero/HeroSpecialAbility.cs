using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroSpecialAbility : MonoBehaviour {
    public Image pointsBar;
    public Text pointsText;
    public GameObject bullet;
    public GameObject bulletPosition;
    public GameObject SpecialAbilityObject;
    public HeroManaAndAbilities manaAndAbilities;
    public GameMaster gm;
    HeroShoot heroShoot;
    bool specialItemActive;
    bool itemChanged = true;
    public KeyCode specialAbilityButton;
    public KeyCode endButton;
    public float timeLeft = 0f;
    public float timeParameter = 0f;
    public bool done = false;
    public float pointsNeeded = 100f;
    public float maxPoints;
    public float points;
    float lastPoints;
    public float timeChanger = 1;
    float abilityNum0;
    float abilityNum1;
    bool freeUsage = false;

    float[] cooldown = new float[3];
    int switchNum;
	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        heroShoot = GetComponent<HeroShoot>();
        manaAndAbilities = GetComponent<HeroManaAndAbilities>();
        if (Informations.statistics[3] == 2 )
        {
            SpecialAbilityObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Players/SpecialAbilityObject_" + Informations.statistics[3]);
            specialItemActive = true;
        }
        //SpecialAbilityObject.SetActive(false);
        pointsNeeded = Informations.specialAbilityPointsNeeded[Informations.statistics[3]];
        maxPoints = Informations.specialAbilityMaxPoints[Informations.statistics[3]];
        RefreshBar();
    }
	
	// Update is called once per frame
	void Update () {
        if(specialItemActive == true && itemChanged == false)
        {
            if(timeLeft <= 0)
            {
                gameObject.GetComponent<HeroShoot>().gun.SetActive(false);
                SpecialAbilityObject.SetActive(true);
                itemChanged = true;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (cooldown[i] > 0f)
            {
                cooldown[i] -= Time.deltaTime * timeChanger;
            }
        }
        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime * timeChanger;
        }
        if (timeParameter >= 0f)
        {
            timeParameter -= Time.deltaTime * timeChanger;
        }
        if (Input.GetKeyDown(specialAbilityButton) && points >= pointsNeeded && Characters.characters[Informations.statistics[3]].specialAbility == true || done == true && timeLeft <= 0f)
        {

            switch(Informations.statistics[3])
            {
                case 0:
                    TimeSlow();
                    break;
                case 1:
                    MindAttack();
                    break;
                case 2:
                    SpearAttack();
                    break;
                case 3:
                    MachineGun();
                    break;
            }
            RefreshBar();
        }
        else if (Input.GetKeyDown(endButton))
        {
            switchNum = -1;
            switch (Informations.statistics[3])
            {
                case 0:
                    TimeSlow();
                    break;
                case 1:
                    MindAttack();
                    break;
                case 2:
                    SpearAttack();
                    break;
                case 3:
                    MachineGun();
                    break;
            }
            RefreshBar();
        }

    }
    public void GetPoints(float amount)
    {
        points = Mathf.Clamp(points + amount,0f,maxPoints);
        RefreshBar();
    }
    public void RefreshBar()
    {
        if(points >= pointsNeeded)
        {
            pointsBar.color = new Color32(255, 0, 180, 255);
        }
        else if(pointsBar.color != new Color32(255,255,255,255))
        {
            pointsBar.color = new Color32(255, 255, 255, 255);
        }
        pointsBar.fillAmount = points / maxPoints;
        pointsText.text = "" + System.Math.Round(points,1) + " / " + maxPoints;
    }
    public void RefreshNums()
    {
        switchNum = 0;
        timeChanger = 1;
        endButton = KeyCode.None;
        specialAbilityButton = KeyCode.Space;
        //GetComponent<HeroShoot>().abilityActiveCount--;
        freeUsage = false;
        done = false;
        timeParameter = 0f;
        abilityNum0 = 0f;
        abilityNum1 = 0f;
        cooldown[0] = 0f;
        cooldown[1] = 0f;
        cooldown[2] = 0f;
        SpecialAbilityObject.SetActive(false);
        gameObject.GetComponent<HeroShoot>().gun.SetActive(true);
        itemChanged = true;

    }
    public void TimeSlow()
    {
        switch (switchNum)
        {
            case 0:
                GetComponent<HeroShoot>().abilityActiveCount ++;
                done = true;
                //Time.timeScale = 0.4f * gm.timeMeter * gm.actualTimeModulations;
                timeChanger = 1f / 0.4f;
                gm.actualTimeModulations *= 0.4f;
                Time.timeScale = gm.tempoMeter * gm.actualTimeModulations;
                GetComponent<HeroShoot>().movementSpeedModifier = timeChanger;
                GetComponent<HeroShoot>().RefreshStats();
                lastPoints = points;
                points -= 10f * Mathf.Round((points - 4f) / 10f);
                BulletStats(0 + "", 12f, 10f, 4f, 1, 0f, 0f);
                switchNum = 1;
                cooldown[1] = 0f;
                cooldown[0] = 0f;
                break;
            case 1:
                if (cooldown[0] <= 0 && cooldown[1] <= 0f)
                {
                    bullet.transform.position = bulletPosition.transform.position + new Vector3(Random.Range(0f, 0.08f), Random.Range(-0.08f, 0.08f), 0f);
                    bullet.GetComponent<BulletMobility>().direction = Random.Range(-3f, 3f);
                    Instantiate(bullet);
                    FindObjectOfType<AudioManager>().Play("LaserSound0");
                    cooldown[0] = 0.045f;
                    abilityNum0++;
                    if (abilityNum0 >= 4)
                    {
                        cooldown[1] = 1.2f;
                        abilityNum0 = 0f;
                        abilityNum1++;
                    }
                }
                if(abilityNum1 >= Mathf.Round((lastPoints - 4f) / 10f))
                {
                    switchNum = 2;
                    abilityNum1 = 0f;
                }
                break;
            case 2:             
                timeChanger = 1f;
                gm.actualTimeModulations *= 1 / 0.4f;
                Time.timeScale = gm.tempoMeter * gm.actualTimeModulations;
                GetComponent<HeroShoot>().RefreshModifiers();
                GetComponent<HeroShoot>().RefreshStats();
                RefreshNums();
                GetComponent<HeroShoot>().abilityActiveCount--;
                break;

        }
    }
    public void MindAttack()
    {
        points -= pointsNeeded;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject boss = GameObject.FindGameObjectWithTag("boss");
        for( int i = 0;i < enemies.Length; i++)
        {
            enemies[i].GetComponent<EnemyStates>().Freeze(0.25f,8f);
            //manaAndAbilities.RestoreMana(2);
            for(int a = 0; a < 2;a++)
            {
                manaAndAbilities.cooldown[a] -= 2f;
            }
            
        }
        if(boss != null)
        {
            boss.GetComponent<BossStates>().Freeze(0.4f,4f);
            manaAndAbilities.cooldown[0] -= 5f;
            manaAndAbilities.cooldown[1] -= 5f;
            //manaAndAbilities.RestoreMana(5);
        }
        manaAndAbilities.KniveCircleShot0();
        manaAndAbilities.KniveCircleShot1();
        RefreshNums();
    }
    public void SpearAttack()
    {
        switch (switchNum)
        {
            case -1:
                RefreshNums();
                GetComponent<HeroShoot>().abilityActiveCount--;
                break;
            case 0:
                GetComponent<HeroShoot>().abilityActiveCount++;
                //points -= pointsNeeded;
                endButton = KeyCode.Space;
                SpecialAbilityObject.SetActive(true);
                gameObject.GetComponent<HeroShoot>().gun.SetActive(false);
                specialAbilityButton = GetComponent<HeroShoot>().shoot;
                switchNum = 1;
                break;
            case 1:
                itemChanged = false;
                SpecialAbilityObject.SetActive(false);
                //gameObject.GetComponent<HeroShoot>().gun.SetActive(true);
                timeLeft = 0.4f;
                if (points >= pointsNeeded)
                {
                    points -= pointsNeeded;
                    if (timeParameter <= 0f || (timeParameter <= 3f && abilityNum0 == 1f))
                    {
                        switchNum = 2;
                        SpearAttack();
                    }
                    else if (timeParameter <= 3f)
                    {
                        switchNum = 3;
                        SpearAttack();
                    }
                    else
                    {
                        switchNum = 4;
                        SpearAttack();
                    }
                }
                else
                {
                    switchNum = -1;
                    SpearAttack();
                }
                break;
            case 2:
                abilityNum0 = 0f;
                timeParameter = 1.5f;
                BulletStats(2 + "_" + 0, 0.06f, Characters.characters[Characters.characterStatsNum].damage  * 2f, 0.12f, 2, 0f, 0f);
                Instantiate(bullet);
                if(points < pointsNeeded)
                {
                    switchNum = -1;
                    done = true;
                }
                else
                {
                    switchNum = 1;
                }
                break;
            case 3:
                abilityNum0 = 1f;
                timeParameter = 4f;
                BulletStats(2 + "_" + 1, 0.06f, Characters.characters[Characters.characterStatsNum].damage * 3f, 0.15f, 3, 0f, 0.1f);
                Instantiate(bullet);
                if (points < pointsNeeded)
                {
                    switchNum = -1;
                    done = true;
                }
                else
                {
                    switchNum = 1;
                }
                break;
            case 4:
                abilityNum0 = 0f;
                timeParameter = 0f;
                BulletStats(2 + "_" + 2, 0.06f, Characters.characters[Characters.characterStatsNum].damage * 4f, 0.20f, 4, 0f, 0.2f);
                Instantiate(bullet);
                if (points < pointsNeeded)
                {
                    switchNum = -1;
                    done = true;
                }
                else
                {
                    switchNum = 1;
                }
                break;
        }
    }
    public void MachineGun()
    {
        switch(switchNum)
        {
            case -1:
                RefreshNums();
                GetComponent<HeroShoot>().abilityActiveCount--;
                break;
            case 0:
                abilityNum0 = 1f;
                GetComponent<HeroShoot>().abilityActiveCount++;
                endButton = KeyCode.Space;
                specialAbilityButton = GetComponent<HeroShoot>().shoot;
                switchNum = 1;
                done = true;
                break;
            case 1:
                if (Input.GetKey(specialAbilityButton))
                {
                    if (cooldown[1] <= 0f)
                    {
                        BulletStats("" + 3, 8f, 5f, 3f, 1, Random.Range(-12f,12f), Random.Range(-0.25f,0.25f));
                        Instantiate(bullet);
                        GetComponent<HeroShoot>().anim.Play(GetComponent<HeroShoot>().animName);
                        FindObjectOfType<AudioManager>().Play("LaserSound0");
                        cooldown[1] += 0.28f * abilityNum0;
                    }
                    if (cooldown[0] <= 0f)
                    {
                        abilityNum0 = Mathf.Clamp(abilityNum0 - 0.04f, 0.4f, 1f);
                        cooldown[0] += 0.25f;
                        points -= pointsNeeded;
                    }
                }
                else if (cooldown[0] <= 0f)
                {
                    cooldown[0] += 0.28f;
                    abilityNum0 = Mathf.Clamp(abilityNum0 + 0.1f, 0.4f, 1f);
                }
                if(Input.GetKeyDown(endButton) || points <= pointsNeeded)
                {
                    switchNum = -1;
                }
                break;
        }
    }
    public void BulletStats(string num, float _speed, float _dmg, float _disappearTime, int _durability, float direction, float positionxChange)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_SpecialAbility/Bullet" + num);
        bullet.GetComponent<BulletMobility>().damage = _dmg * Informations.upgradesAmount.magicDamage[Informations.upgrades[2]];
        bullet.GetComponent<BulletMobility>().speed = _speed;
        //bullet.GetComponent<BulletMobility>().disappearTime = _disappearTime;
        bullet.GetComponent<BulletMobility>().durability = _durability;
        bullet.transform.position = new Vector3(bulletPosition.transform.position.x + positionxChange, bulletPosition.transform.position.y,0f);
        bullet.GetComponent<BulletMobility>().direction = direction;        
    }


}
