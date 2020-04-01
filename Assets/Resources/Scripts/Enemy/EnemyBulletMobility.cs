using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMobility : MonoBehaviour {

    public bool sword;
    public int durability;
    public float disappearTime;
    public float damage = 10f;
    public Animator anim;


    public bool animActive;
    public string animName;
    //public float speedx;
    //public float speedy;

    public float speed = 0;
    public float precision = 0;

    GameMaster gm;
    Vector3 direction;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        if (animActive == true) 
        {
            anim.name = animName;
            anim.Play(animName);
        }
        //CheckDirection();
        transform.rotation =  Quaternion.Euler(0, 0, precision);
    }

    void Update()
    {

        if (sword == false)
        {
            disappearTime -= Time.deltaTime;
            if (disappearTime <= 0)
            {
                Destroy(this.gameObject);
            }
            if (durability <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        //transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime, transform.position.y + speedy * Time.deltaTime);

        //Transform.TransformDirection(Vector3 kierunekWKtórymcheszzrobićrzeczy);

        transform.position += transform.TransformDirection(Vector3.left * speed * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f);
        //transform.position = Vector3.MoveTowards(transform.position, direction + transform.position, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "player")
        {
            if (col.gameObject.GetComponent<HeroHealthScript>().immunity < 0.1f)
            {
                durability--;
            }
        }
        else if(_tag == "heroBlades" || _tag == "explosion")
        {
            durability = 0;

        }

    }
    public void CheckDirection()
    {

        if (precision < 90 && precision > -90)
        {
            direction = new Vector3(-1f, Mathf.Tan(precision * Mathf.Deg2Rad), 0);
        }
        else if ((precision >= 90 && precision <= 180) || (precision <= -90 && precision >= -180))
        {
            direction = new Vector3(1f, Mathf.Tan(-precision * Mathf.Deg2Rad), 0);
        }
        else if (precision > 180 && precision < 360)
        {
            direction = new Vector3(1f, Mathf.Tan((-precision - 180) * Mathf.Deg2Rad), 0);
        }
        else if (precision < -180 && precision > -360)
        {
            direction = new Vector3(1f, Mathf.Tan((-precision + 180) * Mathf.Deg2Rad), 0);
        }
        else if(precision >= 360)
        {
            while(precision >= 360f)
            {
                precision -= 360f;
            }
            CheckDirection();
        }
        else if (precision <= -360)
        {
            while (precision <= -360f)
            {
                precision += 360f;
            }
            CheckDirection();
        }
        direction.Normalize();

    }
}
