using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStates : MonoBehaviour {

    BossScript bossScript;
    BossTricks bossTricks;
    Color32 color;
    public float cooldown;
    float _cooldown;
    float cooldownTimeChanger = 1f;
    float SpeedChanger = 1f;
    bool refreshed = true;
    int effectNum;
	// Use this for initialization
	void Start () {
        color = new Color32(255, 255, 255, 255);
        bossScript = GetComponent<BossScript>();
        bossTricks = GetComponent<BossTricks>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_cooldown >= 0f)
        {
            _cooldown -= Time.deltaTime;
            if (GetComponent<SpriteRenderer>().color != new Color32(255, 72, 72, 255))
            {
                GetComponent<SpriteRenderer>().color = color;
            }
        }
        else if (refreshed != true)
        {
            refreshed = true;
            RefreshStats();
        }
	}
    public void Freeze(float multiplier,float duration)
    {
        color = new Color32(0, 180, 255, 255);
        refreshed = false;
        //cooldownTimeChanger = multiplier;
        //SpeedChanger = multiplier;
        bossTricks.speedChanger = multiplier;
        bossTricks.cooldownTimeChanger = multiplier;
        _cooldown = duration;
    }
    public void RefreshStats()
    {
        bossTricks.cooldownTimeChanger = 1f;
        bossTricks.speedChanger = 1f;
        GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if ((_tag == "bullet" || _tag == "explosion") && col.GetComponent<BulletMobility>().explosion == true )
        {
            effectNum = col.GetComponent<BulletMobility>().effect;
            switch (effectNum)
            {
                case 1:
                    Freeze(0.6f,2.2f);
                    break;
            }
        }
    }
}
