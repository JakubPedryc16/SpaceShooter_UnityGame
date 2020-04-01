﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {

    public int num = 0;
    public int amount = 0;

    public float cooldown;


    private void Update()
    {
        cooldown -= Time.deltaTime;
        if(cooldown <= 0f)
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

