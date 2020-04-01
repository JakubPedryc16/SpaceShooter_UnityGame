using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangingCharacters : MonoBehaviour {

    public GameObject playerName;
    public GameObject playerNameInfo;
    public GameObject playerImage;
    public GameObject UpgradeSystemManager;
    public Text heroInfo;
    public GameMasterCustomization gmc;
    string[] heroInfos = new string[4]
        {
            "\n","\n+30% mana & mana regeneration\n+20% magic damage\n-20% fire rate","\n +20% health and movement speed",""
        };
    string[] heroNames = new string[4]
    {
        "Bart", "Kris", "Kaal", "Hemeritus"
    };
    string[] heroNameInfo = new string[4]
{
        "The Original", "Destroyer Of Galactics", "The Crazy Wild Barbarian","The Ancient One"
};

    // Use this for initialization
    void Start () {
        ChangePlayerImage();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangePlayerImage()
    {
        playerImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + Informations.statistics[3]);
        playerName.GetComponent<Text>().text = heroNames[Informations.statistics[3]];
        playerNameInfo.GetComponent<Text>().text = heroNameInfo[Informations.statistics[3]];
        UpgradeSystemManager.GetComponent<UpgradesManager>().GetUpgrades();
        heroInfo.text = heroInfos[Informations.statistics[3]];



    }

    public void ChangePlayerPlus()
    {
        for (int i = Informations.statistics[3] + 1; i < Informations.charactersUnlocked.Length; i++)
        {
            if (Informations.charactersUnlocked[i] == true && Informations.questLockedCharacters[i] == false)
            {
                Characters.characterStatsNum = i;
                Informations.statistics[3] = i;
                gmc.RefreshBullet();
                break;
            }
        }
    }
    public void ChangePlayerMinus()
    {
        for (int i = Informations.statistics[3] - 1; i >= 0; i--)
        {
            if (Informations.charactersUnlocked[i] == true && Informations.questLockedCharacters[i] == false)
            {
                Characters.characterStatsNum = i;
                Informations.statistics[3] = i;
                gmc.RefreshBullet();
                break;
            }
        }
    }

}
