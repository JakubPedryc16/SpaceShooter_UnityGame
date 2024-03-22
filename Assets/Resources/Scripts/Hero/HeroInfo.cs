using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "potion")
        {
            /*if(col.gameObject.GetComponent<Items>().num == 0)
            {
                GetComponent<HeroManaAndAbilities>().RestoreMana(col.gameObject.GetComponent<Items>().amount);
            }*/
            if (col.gameObject.GetComponent<Items>().num == 0)
            {
                GetComponent<HeroHealthScript>().HealHero(col.gameObject.GetComponent<Items>().amount);
            }
            else if (col.gameObject.GetComponent<Items>().num == 1)
            {
                GetComponent<HeroSpecialAbility>().GetPoints(col.gameObject.GetComponent<Items>().amount);
            }

        }
    }
}
