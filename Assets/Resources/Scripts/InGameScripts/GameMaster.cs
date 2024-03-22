using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    public float tempoMeter = 1f;
    float timeLeftToChangeMeter;
    public float timeToChangeMeter = 15f;
    public float timeMeterLimits = 1.75f;

    public float actualTimeModulations;
    public bool stopTime = false;

    float time;
    float TMtime;

    public GameObject YouLostObject;
    public bool dontUPauseIt = false;
    public GameObject youWinObject;
    public Text firstTimeBossKillText;
    public int wallHealth = 5;
    public int moneyStatus;
    //public int lastMoney;
    public GameObject moneyText;
    GameObject player;
    public GameObject wallHealthImage;
    public GameObject bossHealth;
    public GameObject bossLostHealth;
    public GameObject bossHealthShadow;
    public float timeToDisappear;

    public Text tempoMeterText;
    public Text scoreTextWin;
    public Text scoreTextLose;

    public int TMbonusAfterBoss;
    GameObject choosenChapter;
    void Start()
    {

        RefreshTime();
        timeLeftToChangeMeter = timeToChangeMeter;
        timeToChangeMeter *= Informations.difficultyStats[Informations.statistics[5]].timeMeterTempo;
        timeMeterLimits *= Informations.difficultyStats[Informations.statistics[5]].timeMeterLimits;
        Instantiate(Resources.Load<GameObject>("Prefabs/Chapters/Chapter" + Informations.statistics[0]));
        GetUpgrades();
        bossHealth.SetActive(false);
        bossHealthShadow.SetActive(false);
        player = GameObject.FindGameObjectWithTag("player");
    }
    void Update()
    {
        time += Time.deltaTime * tempoMeter;
        TMtime += Time.deltaTime;
        for(int i = 0; i < tempoMeterGoals.Length ;i++)
        {
            if(tempoMeter < tempoMeterGoals[i])
            {
                tempoMeterText.text = "TM: " + tempoMeterGoalsStrings[i];
                tempoMeterText.color = tempoMeterGoalsColors[i];
                break;
            }
        }
        if (timeLeftToChangeMeter >= 0 && stopTime == false)
        {
            timeLeftToChangeMeter -= (Time.deltaTime / tempoMeter) * actualTimeModulations;
        }
        else if(tempoMeter < timeMeterLimits && stopTime == false)
        {
            timeLeftToChangeMeter = timeToChangeMeter;
            tempoMeter = Mathf.Clamp(tempoMeter + 0.05f,1f,timeMeterLimits);
            RefreshTime();
        }
        if (timeToDisappear >= 0)
        {
            timeToDisappear -= Time.deltaTime;
        }
        else if (bossLostHealth.GetComponent<Image>().fillAmount > bossHealth.GetComponent<Image>().fillAmount)
        {
            bossLostHealth.GetComponent<Image>().fillAmount -= 0.001f;
        }
        if (GameObject.FindGameObjectsWithTag("boss").Length > 0)
        {
            bossHealth.SetActive(true);
            bossHealthShadow.SetActive(true);
            bossLostHealth.SetActive(true);
            bossHealth.GetComponent<Image>().fillAmount = GameObject.FindGameObjectWithTag("boss").GetComponent<BossScript>().health / GameObject.FindGameObjectWithTag("boss").GetComponent<BossScript>().startHealth;
        }
        else
        {
            bossHealth.SetActive(false);
            bossHealthShadow.SetActive(false);
            bossLostHealth.SetActive(false);

        }
        moneyText.GetComponent<Text>().text ="" + (moneyStatus) ;       
    }
    public void HurtWall(int dmg)
    {
        wallHealth -= dmg;
        if (wallHealth <= 0)
        {
            player.GetComponent<HeroHealthScript>().HurtHero(20f + (5f * Informations.statistics[0]));
            wallHealth = 4;
        }
        wallHealthImage.GetComponent<Image>().fillAmount = wallHealth / 4f;
    }
    public void EarnMoney(int value)
    {
        moneyStatus += value;
        Informations.statistics[1] += value;
    }

    public void RefreshTime()
    {
        Time.timeScale = tempoMeter * actualTimeModulations;
    }
    public void GetUpgrades()
    {
        Characters.charactersUpgrades.cooldown = Informations.upgradesAmount.cooldownMultiplier[Informations.upgrades[1]];
        Characters.charactersUpgrades.damage = Informations.upgradesAmount.damageMultiplier[Informations.upgrades[2]];
        Characters.charactersUpgrades.health = Informations.upgradesAmount.healthMultiplier[Informations.upgrades[0]];
        //Characters.charactersUpgrades.mana = Informations.upgradesAmount.manaMultiplier[Informations.upgrades[1]];
    }
    public void YouWin()
    {
        dontUPauseIt = true;
        stopTime = true;
        Time.timeScale = 0f;
        youWinObject.SetActive(true);
        if (Informations.statistics[4] <= Informations.statistics[0])
        {
            Informations.statistics[4] = Informations.statistics[0] + 1;
            firstTimeBossKillText.text = firstTimeBossKillString[Informations.statistics[0]];



            scoreTextWin.text = "Basic Money: " + (moneyStatus - bossRewards[Informations.statistics[0],0]) + "\nTimeMeter Bonus: " + (int)(((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]) + "\nWin Bonus (TimeMeter bonus x1.2): " + (int)(((((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]) * 1.2f) - (((moneyStatus - bossRewards[Informations.statistics[0], 0])* (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0],0])) + "\nYou Get: " + (int)(((moneyStatus - bossRewards[Informations.statistics[0], 0] )* (time / TMtime)) + (int)(((((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]) * 1.2f) - (((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]))) + " and boss reward: " + bossRewards[Informations.statistics[0], 0];
            EarnMoney((int)(((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]) + (int)(((((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0]) * 1.2f) - (((moneyStatus - bossRewards[Informations.statistics[0], 0]) * (time / TMtime)) - moneyStatus + bossRewards[Informations.statistics[0], 0])));
        }
        else if(GameObject.FindGameObjectWithTag("firstChapterWinObject") != null)
        {
            scoreTextWin.text ="Basic Money: " + moneyStatus + "\nTimeMeter Bonus: " + (int)((moneyStatus * (time / TMtime)) - moneyStatus) + "\nWin Bonus (TimeMeter bonus x1.2): " + (int)((((moneyStatus * (time / TMtime)) - moneyStatus) * 1.2f) - ((moneyStatus * (time / TMtime)) - moneyStatus)) + "\nYou Get: " + (int)(moneyStatus + (moneyStatus * (time / TMtime)) - moneyStatus) + ((((moneyStatus * (time / TMtime)) - moneyStatus) * 1.2f) - ((moneyStatus * (time / TMtime)) - moneyStatus));
            EarnMoney((int)(((moneyStatus * (time / TMtime)) - moneyStatus) + ((((moneyStatus * (time / TMtime)) - moneyStatus) * 1.2f) - ((moneyStatus * (time / TMtime)) - moneyStatus))));
        }

    }
    public void YouLost()
    {
        dontUPauseIt = true;
        stopTime = true;
        Time.timeScale = 0f;
        scoreTextLose.text = "Basic Money: " + moneyStatus + "\nTimeMeter Bonus: " + (int)((moneyStatus * (time / TMtime)) - moneyStatus) + "\nYou Get: " + (int)(moneyStatus + (moneyStatus * (time / TMtime)) - moneyStatus);
        EarnMoney((int)((moneyStatus * (time / TMtime)) - moneyStatus));
        YouLostObject.SetActive(true);
    }

    public float[,] bossRewards = new float[4, 5]
    {
        {500f,2f,4f,3f,3f}, //money, characters, bullet, spell, ability
        {500f,2f,4f,3f,3f},
        {0f,0f,0f,0f,0f},
        {0f,0f,0f,0f,0f}
    };

    float[] tempoMeterGoals = new float[7]  {1.05f,1.2f,1.35f,1.5f,1.7f,2f,3f};
    string[] tempoMeterGoalsStrings = new string[7] { "Weak", "Cool", "Awesome", "Chaotic", "Insane!", "Macabre !", "Godless !!!" };
    Color32[] tempoMeterGoalsColors = new Color32[7] { new Color32(255, 255, 255, 255), new Color32(0, 255, 255, 255), new Color32(200, 255, 0, 255), new Color32(255, 200, 0, 255), new Color32(255, 55, 0, 255), new Color32(200, 0, 255, 255), new Color32(255, 0, 175, 255) };

    string[] firstTimeBossKillString = new string[4]
    {
        "New Episode Unlocked\nYou Get:- New character\n-New Bullet\n-New Spell\n-New Ability\nCheck The PDA and Customization\nCongratulations !!!",
        "New Episode Unlocked\nYou Get:- New character\n-New Bullet\n-New Spell\n-New Ability\nCheck The PDA and Customization\nCongratulations !!!",
        "New Episode Unlocked\n",
        "New Episode Unlocked\n"
    };
}
