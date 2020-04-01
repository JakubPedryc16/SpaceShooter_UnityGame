using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour {

    float timeToDestroy = 10f;

    public string name;

    public int buffNumber;
    public int buffTime;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        if (_tag == "player")
        {
            Destroy(this.gameObject);
        }
    }
}
