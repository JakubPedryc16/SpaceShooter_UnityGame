using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    //public EnemySkill enemySkill;

    public float health = 100f;
    public float colorBack = 0.1f;
    bool colorReseted;
    public int wallDmg = 1;
    public float heroDmg = 10f;


    public float materialsChance = 0;
    public float itemChance = 0;
    public int crystalsAmount = 1;
    public float pointsForHeroAbility;
    // float moneyGetChance = 0;
    //public int moneyValue = 1;
    public int enemyKind;
    GameMaster gameMaster;
    GameObject hero;

    public delegate void EnemyEvent(GameObject enemyObject);
    public event EnemyEvent OnDeath;

    // Use this for initialization
    void Start () {
        LoadStatistics();
        gameMaster = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        hero = GameObject.FindGameObjectWithTag("player");
    }
	
	// Update is called once per frame
	void Update () {

        colorBack -= Time.deltaTime;
        if(colorBack <= 0f && colorReseted == false)
        {
           GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
           colorReseted = true;
        }
        if(transform.position.x <= -8.7f)
        {
            gameMaster.HurtWall(wallDmg);
            Death();
        }
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "bullet" || _tag == "explosion")
        {
            health -= col.gameObject.GetComponent<BulletMobility>().damage;
            GetComponent<SpriteRenderer>().color = new Color32(255, 72, 72, 255);
            colorBack = 0.1f;
            colorReseted = false;
            if (health <= 0)
            {
                CheckForLoot();
                if (GameObject.FindGameObjectWithTag("boss") == null)
                {
                    gameMaster.EarnMoney(crystalsAmount);
                }
                hero.GetComponent<HeroSpecialAbility>().GetPoints(pointsForHeroAbility);
                Death();
            }
            else { FindObjectOfType<AudioManager>().Play("HitSound"); }
        }

        else if(_tag == "specialBullet"|| _tag == "heroBlades")
        {
            health -= col.gameObject.GetComponent<BulletMobility>().damage;
            GetComponent<SpriteRenderer>().color = new Color32(255, 72, 72, 255);
            colorBack = 0.1f;
            colorReseted = false;
            if (health <= 0)
            {
                CheckForLoot();
                if (GameObject.FindGameObjectWithTag("boss") == null)
                {
                    gameMaster.EarnMoney(crystalsAmount);
                }
                Death();
            }
            else { FindObjectOfType<AudioManager>().Play("HitSound"); }
        }
        else if(_tag == "player" && col.GetComponent<HeroHealthScript>().immunity <= 0f)
        {
            health -= 50f;
            if (health <= 0)
            {
                CheckForLoot();
                Death();
            }
        }


    }
    public void CheckForLoot()
    {
        FindObjectOfType<AudioManager>().Play("DeathSound");
        if (enemyKind <= 500)
        {
            if (Informations.enemiesUnlocked[enemyKind] == false)
            {
                Informations.enemiesUnlocked[enemyKind] = true;
            }
        }
        float num0 = Random.Range(0f, 100f);
        float num1 = Random.Range(0f, 100f);
        float num2 = Random.Range(0f, 100f);

        if (itemChance >= num0)
        {
            int itemType = Random.Range(1, 4);
            GameObject item = Resources.Load<GameObject>("Prefabs/Pickups/Items/Item" + itemType);
            item.transform.position = new Vector3(transform.position.x + Random.Range(0f, 0.5f), transform.position.y + Random.Range(0f, 0.5f));
            Instantiate(item);
        }
        if (materialsChance >= num1 && GameObject.FindGameObjectWithTag("boss") == null)
        { 
            int materialType = 0;
            GameObject material = Resources.Load<GameObject>("Prefabs/Pickups/Materials/Material" + materialType);
            material.transform.position = new Vector3(transform.position.x + Random.Range(0f, 0.5f), transform.position.y + Random.Range(0f,0.5f));
            Instantiate(material);

        }

        float num = Random.Range(0f, 100f);
        float actualAmount = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (enemyKind < 800)
            {
                if (gameMaster.potionDropChances[enemyKind, i] + actualAmount >= num)
                {
                    GameObject item = Resources.Load<GameObject>("Prefabs/Pickups/Items/potion" + i);
                    item.transform.position = new Vector3(transform.position.x + Random.Range(0f, 0.5f), transform.position.y + Random.Range(0f, 0.5f));
                    Instantiate(item);
                    break;
                }
                actualAmount += gameMaster.potionDropChances[enemyKind, i];
            }
        }

    }
    public void LoadStatistics()
    {
        health *= Informations.difficultyStats[Informations.statistics[5]].enemyHealthMultiplier;
        if (GetComponent<EnemyShooting>() != null)
        {
            GetComponent<EnemyShooting>().damage *= Informations.difficultyStats[Informations.statistics[5]].enemyDamageMultiplier;
            GetComponent<EnemyShooting>().speed *= Informations.difficultyStats[Informations.statistics[5]].enemyBulletSpeedMultiplier;
            GetComponent<EnemyShooting>().cooldown *= Informations.difficultyStats[Informations.statistics[5]].enemyAbilityCooldownsMultiplier;
        }
    }
    public void Death()
    {
        OnDeath?.Invoke(this.gameObject);
        Destroy(this.gameObject);
        
    }
}

