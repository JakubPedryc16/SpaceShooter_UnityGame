using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour {

    public float scrollSpeed;
    public float Distance;

    public float cooldown;

    private Vector3 startPosition;
    void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        if (cooldown <= 0)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, Distance);
            transform.position = startPosition + Vector3.left * newPosition;
        }
	}
}
