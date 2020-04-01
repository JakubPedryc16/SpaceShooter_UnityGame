using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour {

    public int prize;
    public GameObject effector;
    public GameObject prizeText;

	// Use this for initialization
	void Start () {
        prize = Random.Range(50, 100);
        effector = Resources.Load<GameObject>("Prefabs/Effectors/effector" + Random.Range(0,3));
        GetComponent<SpriteRenderer>().sprite = effector.GetComponent<SpriteRenderer>().sprite;
        prizeText.GetComponent<Text>().text =  "" + prize ;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        string _tag = col.gameObject.tag;
        if (_tag == "player")
        {
            if (GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().moneyStatus >= prize)
            {
                GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>().moneyStatus -= prize;
                Instantiate(effector,new Vector3(transform.position.x,transform.position.y),new Quaternion());
                Destroy(this.gameObject);
            }
        }
    }
}
