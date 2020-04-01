using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PDAScript : MonoBehaviour {
    public bool done = false;

    private int PDAheight = 5;
    private int PDAwidth = 5;
    private float startY = 0f;

    public Slider slide;
    public GameObject infoText;
    public GameObject PDApanel;
    public GameObject PDAelement;
    public Stack<GameObject> slots = new Stack<GameObject>();

    public Texture2D plague;
    public Texture2D enemy;
    public Texture2D effector;
    public Texture2D buff;
    public GUIContent button;

    public GameObject PDA;
    public bool PDAActive = false;

    public GameObject menuButtons;

    public int PDAoverlap = 0;

    public GameObject buyButton;
    public GameObject buyText;
    public int objectNum;
    public GameObject actualButton;
    public Image actualImage;
    public Text actualPrice;
    public Image crystals;
    // Use this for initialization
    void CreateSlots()
    {
        buyButton.SetActive(false);
        actualPrice.gameObject.SetActive(false);
        actualImage.gameObject.SetActive(false);
        infoText.GetComponent<Text>().text = "";
        for (int y = 0; y < PDAheight; y++)
        {
            for (int x = 0; x < PDAwidth; x++)
            {
                GameObject element = Instantiate(PDAelement, new Vector3(470f + x * 110f, 900f + y * -135f, 0f), new Quaternion(), PDApanel.transform);
                element.transform.position = new Vector3(470f + x * 135f, 900f + y * -135f, 0f-100f);
                PDAElement slot = element.GetComponent<PDAElement>();
                slot.pda = this;
                slot.index = (x + y * 5);
                switch (PDAoverlap)
                {
                    case 0:
                        element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Backgrounds/Background0");
                        break;
                    case 1:
                        if ((Informations.charactersUnlocked[slot.index] == true && Informations.questLockedCharacters[slot.index] == false))
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + (slot.index));
                        }
                        else 
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
                        }
                        break;
                    case 2:
                        if (Informations.enemiesUnlocked[slot.index] == true)
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Enemies/Enemy" + (slot.index));
                        }
                        else
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
                        }
                        break;
                    case 3:
                        if (Informations.questLockedBullets[slot.index] == false)
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + (slot.index));
                        }
                        else
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
                        }
                        break;
                    case 4:
                        if (Informations.spellsUnlocked[slot.index] == true && Informations.questLockedSpells[slot.index] == false)
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + (slot.index));
                        }
                        else
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
                        }
                        break;
                    case 5:
                        if (Informations.abilitiesUnlocked[slot.index] == true && Informations.questLockedAbilities[slot.index] == false)
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + (slot.index));
                        }
                        else
                        {
                            element.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Pytajnik");
                        }
                        break;
                }
                slots.Push(element);
                //Jeżeli to ostatni element w bazie danych to przestań tworzyć
                if (slot.index == PDAInformations.gameStrings[PDAoverlap].Length - 1)
                {
                    y = PDAheight;
                    break;
                }
            }
        }
    }

    void Start()
    {
        startY = this.transform.position.y;
        CreateSlots();
        PDA.SetActive(false);
        buyButton.SetActive(false);
        actualPrice.gameObject.SetActive(false);
        actualImage.gameObject.SetActive(false);
        if(Informations.straightToPDA == 1)
        {
            PDAActivation();
        }
    }
    void Update()
    {
        slide.value -= Input.mouseScrollDelta.y * 10f;
    }

    public void PDAActivation()
    {
        if (PDAActive == false)
        {
            PDAActive = true;
            PDA.SetActive(true);
            menuButtons.SetActive(false);
            Informations.straightToPDA = 0;

        }
        else if (PDAActive == true)
        {
            PDAActive = false;
            PDA.SetActive(false);
            menuButtons.SetActive(true);
        }
    }
    public void Refresh()
    {
        while (slots.Count > 0)
        {
            Destroy(slots.Pop());
        }
        CreateSlots();
    }

    public void ChangeOverlap(int num)
    {
        PDAoverlap = num;
        Refresh();
        //actualImage.gameObject.SetActive(false);
        //actualButton.SetActive(false);
        //actualPrice.gameObject.SetActive(false);
    }
    public void ScrollManager(float  _position)
    {
        PDApanel.transform.position = new Vector3(PDApanel.transform.position.x, startY + _position * PDAheight/12);
    }
    public void ShowInfo(int num)
    {
        buyText.GetComponent<Text>().text = "Buy";
        if (PDAoverlap == 1)
        {
            if (Informations.questLockedCharacters[num] == false)
            {
                infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
            }
            else
            {
                buyText.GetComponent<Text>().text = "Quest Reward";
                infoText.GetComponent<Text>().text = "Unlock by achieves in game";
            }
        }
        else if (PDAoverlap == 2 && Informations.enemiesUnlocked[num] == true)
        {
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
        }
        else if (PDAoverlap == 3)
        {
            if (Informations.questLockedBullets[num] == false)
            {
                infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
            }
            else
            {
                buyText.GetComponent<Text>().text = "Quest Reward";
                infoText.GetComponent<Text>().text = "Unlock by achieves in game";
            }
        }
        else if (PDAoverlap == 4)
        {
            if (Informations.questLockedSpells[num] == false)
            {
                infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
            }
            else
            {
                buyText.GetComponent<Text>().text = "Quest Reward";
                infoText.GetComponent<Text>().text = "Unlock by achieves in game";
            }
        }
        else if (PDAoverlap == 5)
        {
            if (Informations.questLockedAbilities[num] == false)
            {
                infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
            }
            else
            {
                buyText.GetComponent<Text>().text = "Quest Reward";
                infoText.GetComponent<Text>().text = "Unlock by achieves in game";
            }
        }
        else if(PDAoverlap == 0)
        {
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][num];
        }
        else
        {
            infoText.GetComponent<Text>().text = "Unlock to see...";
        }
    }
    public void Buy()
    {
        if (PDAoverlap == 1 && Informations.statistics[1] >= Informations.heroPrices[objectNum] && Informations.charactersUnlocked[objectNum] == false && Informations.questLockedCharacters[objectNum] == false)
        {
            Informations.statistics[1] -= Informations.heroPrices[objectNum];
            Informations.charactersUnlocked[objectNum] = true;
            actualButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Players/Player" + (objectNum));
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][objectNum];
            actualButton.GetComponent<PDAElement>().ShowInfo();
        }
        /*if (PDAoverlap == 3 && Informations.statistics[1] >= Informations.bulletsPrices[objectNum] && Informations.questLockedBullets[objectNum] == false)
        {
            Informations.statistics[1] -= Informations.bulletsPrices[objectNum];
            Informations.questLockedBullets[objectNum] = true;
            actualButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Bullets/Bullet_Hero" + (objectNum));
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][objectNum];
            actualButton.GetComponent<PDAElement>().ShowInfo();
        }*/
        if (PDAoverlap == 4 && Informations.statistics[1] >= Informations.spellPrices[objectNum] && Informations.spellsUnlocked[objectNum] == false && Informations.questLockedSpells[objectNum] == false)
        {
            Informations.statistics[1] -= Informations.spellPrices[objectNum];
            Informations.spellsUnlocked[objectNum] = true;
            actualButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Spell" + (objectNum));
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][objectNum];
            actualButton.GetComponent<PDAElement>().ShowInfo();
        }
        if (PDAoverlap == 5 && Informations.statistics[1] >= Informations.abilitesPrices[objectNum] && Informations.abilitiesUnlocked[objectNum] == false && Informations.questLockedAbilities[objectNum] == false)
        {
            Informations.statistics[1] -= Informations.abilitesPrices[objectNum];
            Informations.abilitiesUnlocked[objectNum] = true;
            actualButton.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/CustomizationStuff/Ability" + (objectNum));
            infoText.GetComponent<Text>().text = PDAInformations.gameStrings[PDAoverlap][objectNum];
            actualButton.GetComponent<PDAElement>().ShowInfo();
        }

    }
}
