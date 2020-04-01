using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour {

    public int CrystalsValue;
    float timeToDestroy = 5f;

    void Update()
    {
        if (timeToDestroy > 0f)
        {
            timeToDestroy -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
        transform.position = new Vector2(transform.position.x + -1f * Time.deltaTime, transform.position.y + 0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if(_tag == "player")
        {
            GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().EarnMoney(CrystalsValue);
            Destroy(this.gameObject);
        }
    }
}
