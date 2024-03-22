using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public int num = 0;
    public int amount = 0;

    public float cooldown;

    public float lifeTime = 0;


    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "player")
        {
            FindObjectOfType<AudioManager>().Play("ItemGrab");
            Destroy(this.gameObject);
        }
    }
}

