using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuGameMaster : MonoBehaviour {

    public GameObject menu;
    public GameObject menuButtons;
    public GameObject backgroundSecondMenu;
    public GameObject backgroundFirstMenu;

    public GameObject SaveAndLanguages;

    public int TextNum = 0;

    public GameObject settings;
    public GameObject PDA;

    public GameObject difficultyButton;
    public GameObject[] difficulties;
    public Text actualDifficultyText;
    bool difficultiesActive;

    // Use this for initialization
    void Start()
    {
        SetDifficultiesOff();
        backgroundSecondMenu.SetActive(true);
        menu.SetActive(true);
        if(Informations.firstMenuLoaded == false)
        {
            SaveAndLanguages.SetActive(true);
            Informations.firstMenuLoaded = true;
        }
        else
        {
            SaveAndLanguages.SetActive(false);
        }
    }
	
    public void SaveAndLanguagesOff()
    {
        SaveAndLanguages.SetActive(false);
    }
    public void DifficultyActivation()
    {
        if (difficultiesActive == false)
        {
            actualDifficultyText.text = "Actual:\n" + difficultyNames[Informations.statistics[5]];
            difficultiesActive = true;
            actualDifficultyText.gameObject.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                difficulties[i].SetActive(true);
            }
        }
        else
        {

            difficultiesActive = false;
            actualDifficultyText.gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                difficulties[i].SetActive(false);
            }
        }
    }
    public void SetDifficultiesOff()
    {
        difficultiesActive = false;
        actualDifficultyText.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            difficulties[i].SetActive(false);
        }
    }
    public void SetDifficulty(int num)
    {
        Informations.statistics[5] = num;
        actualDifficultyText.text = "Actual:\n" + difficultyNames[Informations.statistics[5]];
    }
    string[] difficultyNames = new string[3]
    {
        "Novice","Advanced","Insane!!!"
    };
}
