using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PDAElement : MonoBehaviour {

    public PDAScript pda;
    public int index = 0;

    public void ShowInfo()
    {
        pda.ShowInfo(index);
        if (pda.PDAoverlap == 0)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/Background0");
            pda.buyButton.SetActive(false);
            pda.actualPrice.gameObject.SetActive(false);
            pda.crystals.gameObject.SetActive(false);
        }
        else if (pda.PDAoverlap == 1)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.buyButton.SetActive(true);
            pda.actualPrice.gameObject.SetActive(true);
            if (Informations.questLockedCharacters[index] == true)
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Can't be bought";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Players/Player" + index);
            }
            else if(Informations.charactersUnlocked[index] == false)
            {
                pda.crystals.gameObject.SetActive(true);
                pda.actualPrice.text = "" + Informations.heroPrices[index];
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Unlocked";
                pda.buyText.GetComponent<Text>().text = "";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Players/Player" + index);
            }
        }
        else if (pda.PDAoverlap == 2)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.buyButton.SetActive(false);
            pda.actualPrice.gameObject.SetActive(true);
            pda.crystals.gameObject.SetActive(false);
            if (Informations.enemiesUnlocked[index] == false)
            {
                pda.actualPrice.text = "Find and kill to unlock";
            }
            else
            {
                pda.actualPrice.text = "Unlocked";
                pda.buyText.GetComponent<Text>().text = "";
            }
            if (Informations.enemiesUnlocked[index] == true)
            {
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Enemies/Enemy" + index);
            }
            else
            {
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
        }
        else if (pda.PDAoverlap == 3)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + index);
            pda.buyButton.SetActive(true);
            pda.actualPrice.gameObject.SetActive(true);
            if (Informations.questLockedBullets[index] == true)
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Can't be bought";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Unlocked";
                pda.buyText.GetComponent<Text>().text = "";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + index);
            }
        }
        else if (pda.PDAoverlap == 4)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + index);
            pda.buyButton.SetActive(true);
            pda.actualPrice.gameObject.SetActive(true);
            if(Informations.questLockedSpells[index] == true)
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Can't be bought";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else if (Informations.spellsUnlocked[index] == false)
            {
                pda.crystals.gameObject.SetActive(true);
                pda.actualPrice.text = "" + Informations.spellPrices[index];
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Unlocked";
                pda.buyText.GetComponent<Text>().text = "";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + index);
            }
        }
        else if (pda.PDAoverlap == 5)
        {
            pda.actualImage.gameObject.SetActive(true);
            pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + index);
            pda.buyButton.SetActive(true);
            pda.actualPrice.gameObject.SetActive(true);
            if(Informations.questLockedAbilities[index] == true)
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Can't be bought";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else if (Informations.abilitiesUnlocked[index] == false)
            {
                pda.crystals.gameObject.SetActive(true);
                pda.actualPrice.text = "" + Informations.abilitesPrices[index];
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
            }
            else
            {
                pda.crystals.gameObject.SetActive(false);
                pda.actualPrice.text = "Unlocked";
                pda.buyText.GetComponent<Text>().text = "";
                pda.actualImage.sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + index);
            }
        }
    } 
    public void SetImage(Sprite _sprite)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = _sprite;
    }
    public void SetObjectNum()
    {
        pda.objectNum = index;
        pda.actualButton = this.gameObject;
    }

}
