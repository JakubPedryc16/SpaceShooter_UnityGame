using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{

    public void SaveGame()
    {
		SaveLoad.SavePlayer(Informations.saveNum, this);

    }

    public void Load(int num)
    {
        //int[] loadedStats = SaveLoad.LoadPlayer1();
        PlayerData dataToLoad = SaveLoad.LoadPlayer(num);
		int[] loadedStats = dataToLoad.stats;
        int[] loadedQuest = dataToLoad.quests;
        int[] loadedUpgrades = dataToLoad.upgrades;
        int[] currentAbility = dataToLoad.actualAbility;
        bool[] charactersAvailable = dataToLoad.charactersUnlocked;
        bool[] questLockedBullets = dataToLoad.questLockedBullets;
        bool[] abilitiesUnlocked = dataToLoad.abilitiesUnlocked;
        bool[] enemiesUnlocked = dataToLoad.enemiesUnlocked;

        for (int i = 0; i < Informations.statistics.Length; i++)
        {
            Informations.statistics[i] = loadedStats[i];
        }
        for (int i = 0; i < Informations.quests.Length; i++)
        {
            Informations.quests[i] = loadedQuest[i];
        }
        for (int i = 0; i < Informations.upgrades.Length; i++)
        {
            Informations.upgrades[i] = loadedUpgrades[i];
        }
        for (int i = 0; i < Informations.actualAbility.Length; i++)
        {
            Informations.actualAbility[i] = currentAbility[i];
        }
        for (int i = 0; i < Informations.charactersUnlocked.Length; i++)
        {
            Informations.charactersUnlocked[i] = charactersAvailable[i];
        }
        for (int i = 0; i < Informations.questLockedBullets.Length; i++)
        {
            Informations.questLockedBullets[i] = questLockedBullets[i];
        }
        for (int i = 0; i < Informations.abilitiesUnlocked.Length; i++)
        {
            Informations.abilitiesUnlocked[i] = abilitiesUnlocked[i];
        }
        for (int i = 0; i < Informations.enemiesUnlocked.Length; i++)
        {
            Informations.enemiesUnlocked[i] = enemiesUnlocked[i];
        }
        Informations.saveNum = num;
    }
    public void Reset()
    {
        PlayerData dataToLoad = SaveLoad.ResetPlayer(Informations.saveNum);
        int[] loadedStats = dataToLoad.stats;
        int[] loadedQuest = dataToLoad.quests;
        int[] loadedUpgrades = dataToLoad.upgrades;
        int[] currentAbility = dataToLoad.actualAbility;
        bool[] charactersAvailable = dataToLoad.charactersUnlocked;
        bool[] bulletsUnlocked = dataToLoad.questLockedBullets;
        bool[] abilitiesUnlocked = dataToLoad.abilitiesUnlocked;
        bool[] enemiesUnlocked = dataToLoad.enemiesUnlocked;

        for (int i = 0; i < Informations.statistics.Length; i++)
        {
            Informations.statistics[i] = loadedStats[i];
        }
        for (int i = 0; i < Informations.quests.Length; i++)
        {
            Informations.quests[i] = loadedQuest[i];
        }
        for (int i = 0; i < Informations.upgrades.Length; i++)
        {
            Informations.upgrades[i] = loadedUpgrades[i];
        }
        for (int i = 0; i < Informations.actualAbility.Length; i++)
        {
            Informations.actualAbility[i] = currentAbility[i];
        }
        for (int i = 0; i < Informations.charactersUnlocked.Length; i++)
        {
            Informations.charactersUnlocked[i] = charactersAvailable[i];
        }
        for (int i = 0; i < Informations.questLockedBullets.Length; i++)
        {
            Informations.questLockedBullets[i] = bulletsUnlocked[i];
        }
        for (int i = 0; i < Informations.abilitiesUnlocked.Length; i++)
        {
            Informations.abilitiesUnlocked[i] = abilitiesUnlocked[i];
        }
        for (int i = 0; i < Informations.enemiesUnlocked.Length; i++)
        {
            Informations.enemiesUnlocked[i] = enemiesUnlocked[i];
        }
        SaveGame();
    }

}
