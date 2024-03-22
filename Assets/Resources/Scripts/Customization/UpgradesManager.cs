using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour {

    [System.Serializable]
    public class UpgradesDoubler
    {
        //public float[] bulletSizeAdder;
        public float[] cooldownAdder;
        public float[] rangeAdder;
        public float[] damageAdder;
        public int[] healthAdder;
        public float[] movementAdder;
        public float[] bulletSpeedAdder;
    }
    public UpgradesDoubler _UpgradesDoubler;

    public GameObject gm;
    public Text moneyStatus;

    public GameObject[] bars;
    public GameObject[] upgradeNotes;
    public Text[] nameAndPrice;

	// Use this for initialization
	void Start () {
        Refresh();
	}
	
	// Update is called once per frame
	void Update () {
        moneyStatus.text = "" + Informations.statistics[1];
    }
    public void GetUpgrades()
    {
        Characters.charactersUpgrades.health = Informations.upgradesAmount.healthMultiplier[Informations.upgrades[0]];
        //Characters.charactersUpgrades.movementSpeed = Informations.upgradesAmount.movementMultiplier[Informations.upgrades[0]];
        //Characters.charactersUpgrades.range = Informations.upgradesAmount.rangeMultiplier[Informations.upgrades[0]];
        Characters.charactersUpgrades.cooldown = Informations.upgradesAmount.cooldownMultiplier[Informations.upgrades[1]];
        //Characters.charactersUpgrades.mana = Informations.upgradesAmount.manaMultiplier[Informations.upgrades[1]];
        Characters.charactersUpgrades.damage = Informations.upgradesAmount.damageMultiplier[Informations.upgrades[2]];
        //Characters.charactersUpgrades.bulletSpeed = Informations.upgradesAmount.bulletSpeedMultiplier[Informations.upgrades[2]];


  


    }
    public void PlusUpgrade(int barNum)
    {
        if (Informations.upgrades[barNum] < 11)
        {
            if (Informations.statistics[1] >= Informations.prices[Informations.upgrades[barNum]] && bars[barNum].GetComponent<Image>().fillAmount != 0.8)
            {
                Informations.statistics[1] -= Informations.prices[Informations.upgrades[barNum]];
                upgradeNotes[barNum].GetComponent<Text>().color = new Color32(255, 255, 255, 255);
                bars[barNum].GetComponent<Image>().fillAmount = upgradeCircleParameters[barNum];
                Informations.upgrades[barNum] = Mathf.Clamp(Informations.upgrades[barNum] + 1, 0, 11);
                GetUpgrades();
                Refresh();
            }
        }
    }
    public void Refresh()
    {
        bars[0].GetComponent<Image>().fillAmount = upgradeCircleParameters[Informations.upgrades[0]];
        bars[1].GetComponent<Image>().fillAmount = upgradeCircleParameters[Informations.upgrades[1]];
        bars[2].GetComponent<Image>().fillAmount = upgradeCircleParameters[Informations.upgrades[2]];


        if (bars[0].GetComponent<Image>().fillAmount != 0.8f)
        {
            nameAndPrice[0].text = "" + Informations.prices[Informations.upgrades[0]] + "$";
        }
        else
        {
            nameAndPrice[0].text = "MAX";
        }
        if (bars[1].GetComponent<Image>().fillAmount != 0.8f)
        {
            nameAndPrice[1].text = "" + Informations.prices[Informations.upgrades[1]] + "$";
        }
        else
        {
            nameAndPrice[1].text = "MAX";
        }
        if (bars[2].GetComponent<Image>().fillAmount != 0.8f)
        {
            nameAndPrice[2].text = "" + Informations.prices[Informations.upgrades[2]] + "$";
        }
        else
        {
            nameAndPrice[2].text = "MAX";
        }
    }
    static float[] upgradeCircleParameters = new float[12]
{
        0f,0.06f,0.12f,0.18f,0.24f,0.34f,0.4f,0.46f,0.52f,0.58f,0.64f,0.8f
};
}
