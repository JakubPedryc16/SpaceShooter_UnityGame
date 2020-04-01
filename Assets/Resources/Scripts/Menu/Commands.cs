using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Commands : MonoBehaviour{

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
    public void Play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void PDA()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Informations.straightToPDA = 1;
    }
    public void Customization()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
    public void ChangePlayerPlus()
    {
        Characters.characterStatsNum = Mathf.Clamp(++Characters.characterStatsNum,0,3);
    }
    public void ChangePlayerMinus()
    {
        Characters.characterStatsNum = Mathf.Clamp(--Characters.characterStatsNum,0,3);
    }
    public void EarnMoney(int amount)
    {
        Informations.statistics[1] += amount;
    }
    public void ChapterPlus()
    {
        Informations.statistics[4]++;
    }
    public void difficultyEasy()
    {
        Informations.statistics[5] = 0;
    }
    public void difficultyMedium()
    {
        Informations.statistics[5] = 1;
    }
    public void difficultyHard()
    {
        Informations.statistics[5] = 2;
    }
    public void difficultyInsane()
    {
        Informations.statistics[5] = 3;
    }

}
