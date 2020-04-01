using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    public int num = 1;
    // Use this for initialization
    void Start () {
        transform.position = new Vector3(12f, 3.6f);
        
	}
	
	// Update is called once per frame
	void Update () {
        switch (num)
        {
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0f, 3.6f), 5f * Time.deltaTime);
            break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(-12f, 3.6f), 5f * Time.deltaTime);
                if(transform.position == new Vector3(-12,3.6f))
                {
                    num = 1;
                    Destroy(this.gameObject);
                }
                break;
        }
	}
}
