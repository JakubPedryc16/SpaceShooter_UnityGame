using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effectors : MonoBehaviour {

    public string name;

    //GameObject Player;
    public int damageModification = 0;
    public float bulletSpeedModification = 0f;
    public float precisionModification = 0f;
    public float cooldownModification = 0f;
    public int xTimesModification = 0;
    public float speedChangingModification = 0f;

    public float bonusSize;
    public float extraDoubleSizeChance;
    public float extraDamageChance;
    public float doubleBulletCountChance;

    public int prize;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "player")
        {
            //col.gameObject.GetComponent<HeroShoot>().damageModifier += damageModification;
            //col.gameObject.GetComponent<HeroShoot>().speedModifier += bulletSpeedModification;
            //col.gameObject.GetComponent<HeroShoot>().precisionModifier += precisionModification;
            col.gameObject.GetComponent<HeroShoot>().cooldownModifier += cooldownModification;
            Destroy(this.gameObject);
        }
    }
}
