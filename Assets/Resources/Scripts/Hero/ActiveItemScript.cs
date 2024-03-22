using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActiveItemScript : MonoBehaviour {

    int num;
    float cooldown;
    float _cooldown;
    int amount;
    float duration;
    bool itemActive;
    //bool itemActivated;
    public KeyCode useItem;
    GameObject bullet;
    public GameObject bulletPosition;

    public GameObject itemImage;
    public GameObject itemImageShadow;
    public GameObject itemImageCooldownShadow;
    public GameObject itemImageCooldownText;
    public Text itemAmountText;

    void Start() {
        RefreshStats();
        SetImages(1);
        itemImage.SetActive(false);
        itemImageCooldownShadow.SetActive(false);
        itemImageShadow.SetActive(false);
        itemImageCooldownText.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(useItem) && _cooldown <= 0f && num != -1)
        {
            UseItem();
        }
        if (itemActive == true)
        {

        }
        else if (itemActive == true)
        {
            itemActive = false;
            RefreshStats();
        }

        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
        if (amount > 0)
        {
            if (_cooldown > 0)
            {
                SetImages(0);
            }
            else
            {
                SetImages(1);
            }
            itemImageCooldownShadow.GetComponent<Image>().fillAmount = 1 - (_cooldown / cooldown);

        }
    }
    public void UseItem()
    {
        switch (num)
        {
            case 0:
                KniveX5();
                break;
            case 1:
                TripleShot();
                break;
            case 2:
                SurroundShoot();
                break;
        }
    }
    public void bulletStats(int num, float _speed, float _dmg, float _disappearTime, int _durability, float direction)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Ability/Bullet" + num);
        bullet.GetComponent<BulletMobility>().damage = _dmg * Characters.charactersUpgrades.damage;
        bullet.GetComponent<BulletMobility>().speed = _speed;
        //bullet.GetComponent<BulletMobility>().disappearTime = _disappearTime;
        bullet.GetComponent<BulletMobility>().durability = _durability;
        bullet.transform.position = bulletPosition.transform.position;
        bullet.GetComponent<BulletMobility>().direction = direction;
    }
    public void RefreshStats()
    {
        num = -1;
        amount = 0;
        cooldown = 0f;
        duration = 0f;
        itemImage.SetActive(false);
        itemAmountText.gameObject.SetActive(false);
    }
    public void SetStats(int _amount, float _cooldown, float _duration)
    {
        amount = _amount;
        cooldown = _cooldown;
        duration = _duration;
    }
    public void CheckAmount()
    {
        itemAmountText.text = "" + amount;
        if (amount <= 0)
        {
            RefreshStats();
        }
    }
    public void SetImages(int num)
    {
        switch (num)
        {
            case 0:
                itemImageShadow.SetActive(true);
                itemImageCooldownText.SetActive(true);
                itemImageCooldownShadow.SetActive(true);
                itemImage.GetComponent<Image>().color = new Color32(255, 255, 255, 150);
                itemImageCooldownShadow.GetComponent<Image>().color = new Color32(200, 200, 255, 110);
                itemImageShadow.GetComponent<Image>().color = new Color32(0, 0, 0, 220);
                itemImageCooldownText.GetComponent<Text>().text = "" + (Mathf.Round(_cooldown * 10f) / 10f);
                break;
            case 1:
                itemImageShadow.SetActive(false);
                itemImageCooldownText.SetActive(false);
                itemImageCooldownShadow.SetActive(false);
                itemImage.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                itemImageCooldownShadow.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                itemImageShadow.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
                break;
        }
    }
    public void KniveX5()
    {

        BulletStats(0, 8f, Characters.characters[Characters.characterStatsNum].damage * Characters.charactersUpgrades.damage, 4f, 1, 0f);
        Shoot(new float[] { 0f });

    }
    public void TripleShot()
    {

        BulletStats(1, 8f, Characters.characters[Characters.characterStatsNum].damage * Characters.charactersUpgrades.damage, 4f, 1, 0f);
        Shoot(new float[] {90f, 60f, 0f, -60f, -90f });

    }
    public void SurroundShoot()
    {
        BulletStats(0, 8f, Characters.characters[Characters.characterStatsNum].damage * Characters.charactersUpgrades.damage, 4f, 1, 0f);
        Shoot(new float[] { 0f, 30f, 60f, 90f, 120f, 150f, 180f, 210f, 240f, 270f, 300f, 330f, 360f });

    }
    public void Shoot(float[] directions)
    {
        for(int i = 0; i < directions.Length; i++)
        {
            bullet.GetComponent<BulletMobility>().direction = directions[i];
            Instantiate(bullet);
        }
        amount--;
        CheckAmount();
        _cooldown = cooldown;
    }
    public void BulletStats(int num, float _speed, float _dmg, float _disappearTime, int _durability, float direction)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_ActiveItem/Bullet" + num);
        bullet.GetComponent<BulletMobility>().damage = _dmg * Characters.charactersUpgrades.damage;
        bullet.GetComponent<BulletMobility>().speed = _speed;
        //bullet.GetComponent<BulletMobility>().disappearTime = _disappearTime;
        bullet.GetComponent<BulletMobility>().durability = _durability;
        bullet.transform.position = bulletPosition.transform.position;
        bullet.GetComponent<BulletMobility>().direction = direction;
        bullet.transform.position = bulletPosition.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "item")
        {
            FindObjectOfType<AudioManager>().Play("ItemGrab");
            amount = col.GetComponent<Items>().amount;
            cooldown = col.GetComponent<Items>().cooldown;
            num = col.GetComponent<Items>().num;
            itemAmountText.gameObject.SetActive(true);
            //itemActivated = false;
            num = col.gameObject.GetComponent<Items>().num;
            itemImage.SetActive(true);
            itemImage.GetComponent<Image>().sprite = Resources.Load<GameObject>("Prefabs/Pickups/Items/Item" + num).GetComponent<SpriteRenderer>().sprite;
            //UseItem();
            itemAmountText.text = "" + amount;
        }
    }
}
