using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
	public static void SavePlayer(int playerNum, Save save){
		BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/save" + playerNum, FileMode.Create);

        PlayerData data = new PlayerData(save);

        bf.Serialize(stream, data);
        stream.Close();			
	}
    public static PlayerData LoadPlayer(int playerNum)
    {

        if (File.Exists(Application.persistentDataPath + "/save"+ playerNum))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/save"+ playerNum, FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;

        }
        else
        {
            Debug.Log("File not found");
            return new PlayerData();
        }
    }
    public static PlayerData ResetPlayer(int playerNum)
    {
            return new PlayerData();
    }
}
[Serializable]
public class PlayerData
{
    public int[] stats;
    public int[] quests;
    public int[] upgrades;
    public int[] actualAbility;
    public bool[] charactersUnlocked;
    public bool[] questLockedCharacters;
    public bool[] questLockedBullets;
    public bool[] questLockedSpells;
    public bool[] spellsUnlocked;
    public bool[] abilitiesUnlocked;
    public bool[] questLockedAbilities;
    public bool[] enemiesUnlocked;

    public PlayerData(Save save)
    {
        stats = new int[6];

        stats[0] = Informations.statistics[0];
        stats[1] = Informations.statistics[1];
        stats[2] = Informations.statistics[2];
        stats[3] = Informations.statistics[3];
        stats[4] = Informations.statistics[4];
        stats[5] = Informations.statistics[5];

        quests = new int[1];

        quests[0] = Informations.quests[0];

        upgrades = new int[3];

        upgrades[0] = Informations.upgrades[0];
        upgrades[1] = Informations.upgrades[1];
        upgrades[2] = Informations.upgrades[2];

        actualAbility = new int[3];

        actualAbility[0] = Informations.actualAbility[0];
        actualAbility[1] = Informations.actualAbility[1];
        actualAbility[2] = Informations.actualAbility[2];

        charactersUnlocked = new bool[4];

        charactersUnlocked[0] = Informations.charactersUnlocked[0];
        charactersUnlocked[1] = Informations.charactersUnlocked[1];
        charactersUnlocked[2] = Informations.charactersUnlocked[2];
        charactersUnlocked[3] = Informations.charactersUnlocked[3];

        questLockedCharacters = new bool[4];

        questLockedCharacters[0] = Informations.questLockedCharacters[0];
        questLockedCharacters[1] = Informations.questLockedCharacters[1];
        questLockedCharacters[2] = Informations.questLockedCharacters[2];
        questLockedCharacters[3] = Informations.questLockedCharacters[3];

        questLockedBullets = new bool[2];

        questLockedBullets[0] = Informations.questLockedBullets[0];
        questLockedBullets[1] = Informations.questLockedBullets[1];

        abilitiesUnlocked = new bool[3];

        abilitiesUnlocked[0] = Informations.abilitiesUnlocked[0];
        abilitiesUnlocked[1] = Informations.abilitiesUnlocked[1];
        abilitiesUnlocked[2] = Informations.abilitiesUnlocked[2];

        questLockedAbilities = new bool[3];

        questLockedAbilities[0] = Informations.questLockedAbilities[0];
        questLockedAbilities[1] = Informations.questLockedAbilities[1];
        questLockedAbilities[2] = Informations.questLockedAbilities[2];

        spellsUnlocked = new bool[3];

        spellsUnlocked[0] = Informations.spellsUnlocked[0]; 
        spellsUnlocked[1] = Informations.spellsUnlocked[1];
        spellsUnlocked[2] = Informations.spellsUnlocked[2];

        questLockedSpells = new bool[3];

        questLockedSpells[0] = Informations.questLockedSpells[0];
        questLockedSpells[1] = Informations.questLockedSpells[1];
        questLockedSpells[2] = Informations.questLockedSpells[2];

        enemiesUnlocked = new bool[11];

        enemiesUnlocked[0] = Informations.enemiesUnlocked[0];
        enemiesUnlocked[1] = Informations.enemiesUnlocked[1];
        enemiesUnlocked[2] = Informations.enemiesUnlocked[2];
        enemiesUnlocked[3] = Informations.enemiesUnlocked[3];
        enemiesUnlocked[4] = Informations.enemiesUnlocked[4];
        enemiesUnlocked[5] = Informations.enemiesUnlocked[5];
        enemiesUnlocked[6] = Informations.enemiesUnlocked[6];
        enemiesUnlocked[7] = Informations.enemiesUnlocked[7];
        enemiesUnlocked[8] = Informations.enemiesUnlocked[8];
        enemiesUnlocked[9] = Informations.enemiesUnlocked[9];
        enemiesUnlocked[10] = Informations.enemiesUnlocked[10];
    }
    public PlayerData()
    {
        stats = new int[6];
        stats[5] = 1;
        quests = new int[1];
        upgrades = new int[3];
        actualAbility = new int[3];
        charactersUnlocked = new bool[]
        {
            true,true,true,true
        };
        questLockedCharacters = new bool[]
        {
            false,false,false,true
        };
        questLockedBullets = new bool[]
        {
            false,true
        };
        abilitiesUnlocked = new bool[]
        {
            true,true,true
        };
        questLockedAbilities = new bool[]
        {
            false,false,true
        };
        spellsUnlocked = new bool[]
        {
            true,true,true
        };
        questLockedSpells = new bool[]
        {
            false,false,true
        };
        enemiesUnlocked = new bool[11];
    }
}
