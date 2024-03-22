using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMobility : MonoBehaviour {

    public float damage = 0f;
    public float speed = 0f;
    public float direction = 0f;
    public int durability = 0;
    public float disappearTime = 0f;

    public bool speedingUp;
    public float speedingUpValue = 1f;
    public float speedingUpJumpValue;

    public bool explosive = false;
    public bool explosion = false;
    public int effect;

    GameMaster gm;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        speedingUpValue = 1f;

        transform.rotation =  Quaternion.Euler(0, 0, direction);
    }

    void Update () {
        disappearTime -= Time.deltaTime;

        if (disappearTime <= 0 && explosive != true)
        {
            Destroy(this.gameObject);
            
        }
        if (durability <= 0 && explosive != true && explosion != true)
        {
            Destroy(this.gameObject);
        }
        if(speedingUp == true)
        {
            speedingUpValue = Mathf.Clamp(speedingUpValue + speedingUpJumpValue, 1f, 3f);
        }

        transform.position += transform.TransformDirection(Vector3.right * speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * speedingUpValue);


	}
    void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "enemy" || _tag == "boss" || _tag == "stalker")
        {
            durability--;
            if (durability <= 0 && explosive != true && explosion != true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
