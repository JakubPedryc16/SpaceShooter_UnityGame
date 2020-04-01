using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeroHealthScript : MonoBehaviour {

    public GameMaster gm;
    public Text healthText;
    public Image healthBar;
    public Image lostHealthBar;
    public float timeToDisappearHP;

    public float maxHealth;
    public float health;

    public float basicImmunity = 1.5f;
    public float immunity;
    bool colorBack = true;

    float timeMeterBackTimeLeft;
    bool timeMeterBackActive = false;

	void Start () {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        health = maxHealth;
        RefreshBars();
    }
	
	void Update () {
        if(timeMeterBackTimeLeft >= 0 && gm.stopTime == false)
        {
            timeMeterBackTimeLeft -= Time.deltaTime;
        }
        else if (timeMeterBackActive == true && gm.stopTime == false)
        {
            timeMeterBackTimeLeft = 0.4f;
            gm.tempoMeter = Mathf.Clamp(gm.tempoMeter - 0.05f,1,gm.timeMeterLimits);
            gm.RefreshTime();
            if(gm.tempoMeter <= 1.01f)
            {
                gm.tempoMeter = 1f;
                timeMeterBackActive = false;
            }
        }
        if(timeToDisappearHP > 0f)
        {
            timeToDisappearHP -= Time.deltaTime;
        }
        else if(lostHealthBar.fillAmount > healthBar.fillAmount)
        {
            lostHealthBar.fillAmount -= 0.005f;
        }
        if (immunity > 0f)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
            immunity -= Time.deltaTime;
            colorBack = false;
        }
        else if (colorBack == false)
        {
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            colorBack = true;
        }
    }
    public void RefreshBars()
    {
        healthBar.fillAmount = health / maxHealth;
        healthText.text = "" + (int)health;
    }

    public void HurtHero(float amount)
    {
        timeMeterBackActive = true;
        if (timeToDisappearHP <= 0f && lostHealthBar.fillAmount <= healthBar.fillAmount)
        {
            timeToDisappearHP = 0.75f;
        }
        health -= amount;
        RefreshBars();
        if (health <= 0f)
        {
            health = 0f;
            RefreshBars();
            GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().YouLost();
        }
        else if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void HealHero(float amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        RefreshBars();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "enemyBullet" && immunity <= 0f)
        {
            HurtHero(col.GetComponent<EnemyBulletMobility>().damage);
            immunity = basicImmunity;
        }
        else if(_tag == "enemy" && immunity <= 0f)
        {
            if (col.GetComponent<EnemyHealth>().heroDmg != 0)
            {
                HurtHero(col.GetComponent<EnemyHealth>().heroDmg);
                immunity = basicImmunity;
            }
        }
        else if(_tag == "boss" && immunity <= 0f)
        {
            HurtHero(col.GetComponent<BossScript>().bodyDamage * Informations.difficultyStats[Informations.statistics[5]].bossDamageMultiplier);
            immunity = basicImmunity;
        }
    }
}
