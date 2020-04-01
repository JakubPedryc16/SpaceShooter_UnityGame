using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterCustomization : MonoBehaviour {

    public Text abilityInfo;
    public Image actualBullet;
    public Image[] actualAbility;
    Color abilityColor;
    Color SpellColor;

    int abilityUnlockedAmount;
    string[] bulletInfo = new string[2]
        {
            "Normal bullets","Bullets are bigger but your fire rate is lower"
        };
    string[] specialBulletInfo = new string[3]
    {
            "The Legendary Bart's Laser, lower cooldown, higher speed", "none", " A SPEAR ?! More damage ?!"
    };
    string[] spellInfos = new string[3]
    {
        "Mystic Knives\nImmidiately shoots three knives\nMana: "+ Informations.spellManaCost[0] + "\nCooldown: "+ Informations.spellCooldown[0],"Rage\nshoots many bullets with low accuraty\nMana: "+ Informations.spellManaCost[1] + "\nCooldown: "+ Informations.spellCooldown[1],"Freeze Bomb\nshoots a missile that explode and freeze enemies\nMana: "+ Informations.spellManaCost[2] + "\nCooldown: "+ Informations.spellCooldown[2]
    };
    string[] abilityInfos = new string[3]
    {
        "Haste\nYou move and shoot faster for few seconds\nMana: "+ Informations.abilityManaCost[0] + "\nCooldown: "+ Informations.abilityCooldown[0],"Magic Missiles\nshoots additional tracking bullets\nMana: "+ Informations.abilityManaCost[1] + "\nCooldown: "+ Informations.abilityCooldown[1],"Circle Attacks\nshoots bullets around you for a few seconds\nMana "+ Informations.abilityManaCost[2] + "\nCooldown: "+ Informations.abilityCooldown[2]
    };
    public Text actualBulletInfo;
	// Use this for initialization
	void Start () {
        abilityColor = new Color32(174,57,172,255);
        SpellColor = new Color32(37,118,119,255);
        /*
        actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + Informations.statistics[2]);
        actualBulletInfo.text = bulletInfo[Informations.statistics[2]];*/
        RefreshBullet();
        abilityInfo.text = spellInfos[Informations.actualAbility[0]];
        abilityInfo.color = SpellColor;
        actualAbility[0].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + Informations.actualAbility[0]);
        actualAbility[1].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + Informations.actualAbility[1]);
    }
	
	// Update is called once per frame
	void Update () {
    }
    public void ChangeBulletUp()
    {
        for (int i = Informations.statistics[2] + 1; i  < Informations.questLockedBullets.Length; i++)
        {
            if (Informations.questLockedBullets[i] == false)
            {
                Informations.statistics[2] = i;
                actualBulletInfo.text = bulletInfo[Informations.statistics[2]];
                actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + Informations.statistics[2]);
                break;
            }
        }

    }
    public void ChangeBulletMinus()
    {
        for (int i = Informations.statistics[2] - 1; i >= 0; i--)
        {
            if (Characters.characters[Informations.statistics[3]].specialBullet == true && Informations.statistics[2] - 1 == 0)
            {
                Informations.statistics[2] = i;
                actualBulletInfo.text = specialBulletInfo[Informations.statistics[3]];
                actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets_Special/Bullet" + Informations.statistics[3]);
                break;
            }
            else
            {
                if (Informations.questLockedBullets[i] == false)
                {
                    Informations.statistics[2] = i;
                    actualBulletInfo.text = bulletInfo[Informations.statistics[2]];
                    actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + Informations.statistics[2]);
                    break;
                }
                
            }
        }
    }
    public void RefreshBullet()
    {
            if (Characters.characters[Informations.statistics[3]].specialBullet == true && Informations.statistics[2] == 0)
            {
                actualBulletInfo.text = specialBulletInfo[Informations.statistics[3]];
                actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets_Special/Bullet" + Informations.statistics[3]);
            }
            else
            {
                actualBulletInfo.text = bulletInfo[Informations.statistics[2]];
                actualBullet.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + Informations.statistics[2]);
            }

    }
    public void ChangeSpellPlus()
    {
        for (int i = Informations.actualAbility[0] + 1; i < Informations.spellsUnlocked.Length; i++)
        {
            if (Informations.spellsUnlocked[i] == true && Informations.questLockedSpells[i] == false)
            {
                Informations.actualAbility[0] = i;
                //Informations.actualAbility[num] = Mathf.Clamp(Informations.actualAbility[num] + 1, 0, 2);
                actualAbility[0].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + Informations.actualAbility[0]);
                abilityInfo.text = spellInfos[Informations.actualAbility[0]];
                abilityInfo.color = SpellColor;
                break;
            }
        }

    }
    public void ChangeSpellMinus()
    {
        for (int i = Informations.actualAbility[0] - 1; i >= 0; i--)
        {
            if (Informations.spellsUnlocked[i] == true && Informations.questLockedSpells[i] == false)
            {
                Informations.actualAbility[0] = i;
                //Informations.actualAbility[num] = Mathf.Clamp(Informations.actualAbility[num] - 1, 0, 2);
                actualAbility[0].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + Informations.actualAbility[0]);
                abilityInfo.text = spellInfos[Informations.actualAbility[0]];
                abilityInfo.color = SpellColor;
                break;
            }

        }
    }
    public void ChangeAbilityPlus()
    {
        for (int i = Informations.actualAbility[1] + 1; i < Informations.abilitiesUnlocked.Length; i++)
        {
            if (Informations.abilitiesUnlocked[i] == true && Informations.questLockedAbilities[i] == false)
            {
                Informations.actualAbility[1] = i;
                //Informations.actualAbility[num] = Mathf.Clamp(Informations.actualAbility[num] + 1, 0, 2);
                actualAbility[1].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + Informations.actualAbility[1]);
                abilityInfo.text = abilityInfos[Informations.actualAbility[1]];
                abilityInfo.color = abilityColor;
                break;
            }
        }
        
    }
    public void ChangeAbilityMinus()
    {
        for (int i = Informations.actualAbility[1] - 1; i >= 0; i--)
        {
            if (Informations.abilitiesUnlocked[i] == true && Informations.questLockedAbilities[i] == false)
            {
                Informations.actualAbility[1] = i;
                //Informations.actualAbility[num] = Mathf.Clamp(Informations.actualAbility[num] - 1, 0, 2);
                actualAbility[1].sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + Informations.actualAbility[1]);
                abilityInfo.text = abilityInfos[Informations.actualAbility[1]];
                abilityInfo.color = abilityColor;
                break;
            }

        }
    }
}
