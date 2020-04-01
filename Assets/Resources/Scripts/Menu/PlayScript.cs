using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour {

    public GameObject chaptersText;
    public GameObject chapters;
    public bool chaptersactive = false;
    string[] chapterNames = new string[4]
    {
        "The Gate of The Ancient Protector","The Ancient Cultist's Entry","3","?"
    };
    public GameObject menuButtons;
    // Use this for initialization
    void Start () {
        if(Informations.statistics[4] < 1)
        {
            Informations.statistics[4] = 1;
        }
        chapters.SetActive(false);
        chaptersText.GetComponent<Text>().text = chapterNames[Informations.statistics[0]];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChaptersActivation()
    {
        if(chaptersactive == false)
        {
            chaptersactive = true;
            chapters.SetActive(true);
            menuButtons.SetActive(false);
        }
        else if (chaptersactive == true)
        {
            chaptersText.GetComponent<Text>().text = chapterNames[Informations.statistics[0]];
            chaptersactive = false;
            chapters.SetActive(false);
            menuButtons.SetActive(true);
        }
    }
    public void NextChapter()
    {
        Informations.statistics[0] = Mathf.Clamp(Informations.statistics[0] + 1,0, Informations.statistics[4]);
        chaptersText.GetComponent<Text>().text = chapterNames[Informations.statistics[0]];
    }
    public void PreviousChapter()
    {
        Informations.statistics[0] = Mathf.Clamp(Informations.statistics[0] - 1, 0, Informations.statistics[4]);
        chaptersText.GetComponent<Text>().text = chapterNames[Informations.statistics[0]];
    }
    
}
