using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour {

    public int effectNum;
    EnemyMobility enemyMobility;
    EnemyShooting enemyShooting;
    public float cooldown;
    float _cooldown;
    bool refreshed = true;
    Color32 color;
    float speedChanging;
    float cooldownTimeChanger;

	// Use this for initialization
	void Start () {
        enemyMobility = GetComponent<EnemyMobility>();
        if (GetComponent<EnemyShooting>() != null)
        {
            enemyShooting = GetComponent<EnemyShooting>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (_cooldown >= 0f)
        {
            if (GetComponent<SpriteRenderer>().color != new Color32(255, 72, 72, 255))
            {
                GetComponent<SpriteRenderer>().color = color;
            } 
            _cooldown -= Time.deltaTime;
        }
        else if(refreshed != true)
        {
            RefreshStats();
            refreshed = true;
        }
	}
    public void Freeze(float multiplier,float duration)
    {
        refreshed = false;
        color = new Color32(0, 180, 255, 255);
        _cooldown = duration;
        //speedChanging = strenght;
        //cooldownTimeChanger = strenght;
        if (enemyMobility)
        {
            enemyMobility.speed *= multiplier;
        }
        if (GetComponent<EnemyShooting>() != null)
        {
            enemyShooting.cooldownTimeChanger = multiplier;
        }
    }
    public void RefreshStats()
    {
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        if (enemyMobility)
        {
            enemyMobility.speed = enemyMobility.basicSpeed;
        }
        if (GetComponent<EnemyShooting>() != null)
        {
            enemyShooting.cooldownTimeChanger = 1f;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if ((_tag == "bullet" || _tag == "explosion" )&& col.GetComponent<BulletMobility>().explosion == true)
        {
            effectNum = col.GetComponent<BulletMobility>().effect;
            switch (effectNum)
            {
                case 1:
                    Freeze(0.3f,5f);
                    break;
            }
        }
    }
}
