using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearingText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Text>().color.a > 0)
        {
            GetComponent<Text>().color -= new Color32(0, 0, 0, 2);
        }
	}
}
