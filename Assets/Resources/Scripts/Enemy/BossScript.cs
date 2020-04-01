using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour  {
    public float startHealth = 1000f;
    public float colorBack = 0.1f;
    public float health;
    public float bodyDamage;
    public int num;
    GameMaster gameMaster;
    
    // Use this for initialization
    void Start()
    {
        LoadStatistics();
        health = startHealth;
        gameMaster = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

        colorBack -= Time.deltaTime;
        if (colorBack <= 0f)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
        if (health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("DeathSound");
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
            for(int i = 0;i < enemies.Length; i++)
            {
                Destroy(enemies[i].gameObject);
            }
            if(Informations.statistics[4] <= num)
            {
                gameMaster.EarnMoney((int)gameMaster.bossRewards[num,0]);
                Informations.questLockedBullets[(int)gameMaster.bossRewards[num, 1] - 1] = false; // delete -1 from the next 4 lines
                Informations.questLockedCharacters[(int)gameMaster.bossRewards[num, 2] -1] = false;
                Informations.questLockedSpells[(int)gameMaster.bossRewards[num, 3] -1] = false;
                Informations.questLockedAbilities[(int)gameMaster.bossRewards[num, 4] -1] = false;
            }
            Destroy(this.gameObject);
        }
    }
    public void LoadStatistics()
    {
        bodyDamage *= Informations.difficultyStats[Informations.statistics[5]].bossDamageMultiplier;
        GetComponent<BossTricks>().attacksCooldown *= Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
        GetComponent<BossTricks>().bossRestingTime *= Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
        startHealth *= Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "bullet" || _tag == "explosion")
        {
            FindObjectOfType<AudioManager>().Play("HitSound");
            GameObject.FindGameObjectWithTag("player").GetComponent<HeroSpecialAbility>().GetPoints(Mathf.Clamp(col.gameObject.GetComponent<BulletMobility>().damage/20,0.1f,2f));
            health -= col.gameObject.GetComponent<BulletMobility>().damage;
            GetComponent<SpriteRenderer>().color = new Color32(255, 72, 72, 255);
            colorBack = 0.1f;
            if (gameMaster.bossHealth.GetComponent<Image>().fillAmount >= gameMaster.bossLostHealth.GetComponent<Image>().fillAmount)
            {
                gameMaster.GetComponent<GameMaster>().timeToDisappear = 0.75f;
            }
        }
        else if(_tag == "specialBullet")
        {
            FindObjectOfType<AudioManager>().Play("HitSound");
            health -= col.gameObject.GetComponent<BulletMobility>().damage * 0.7f;
            GetComponent<SpriteRenderer>().color = new Color32(255, 72, 72, 255);
            colorBack = 0.1f;
            if (gameMaster.bossHealth.GetComponent<Image>().fillAmount >= gameMaster.bossLostHealth.GetComponent<Image>().fillAmount)
            {
                gameMaster.GetComponent<GameMaster>().timeToDisappear = 0.75f;
            }
        }
        else if(_tag == "heroBlades")
        {
            FindObjectOfType<AudioManager>().Play("HitSound");
            health -= col.gameObject.GetComponent<BulletMobility>().damage * 0.5f;
            GetComponent<SpriteRenderer>().color = new Color32(255, 72, 72, 255);
            colorBack = 0.1f;
            if (gameMaster.bossHealth.GetComponent<Image>().fillAmount >= gameMaster.bossLostHealth.GetComponent<Image>().fillAmount)
            {
                gameMaster.GetComponent<GameMaster>().timeToDisappear = 0.75f;
            }
        }

    }

}
