using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobility : MonoBehaviour {

    public float speed;
    //float actualSpeed;
    public float basicSpeed; //potrzebne do przywracania bazowej szybkości przez inne skrypty

    private void Start()
    {
        basicSpeed = GetComponent<EnemyMovementController>().speed;
        RefreshStats();
    }
    void Update () {

        if (transform.position.y < -3.7f)
        {
            transform.position = new Vector3(transform.position.x, -3.695f);
        }
        else if (transform.position.y > 3.7f)
        {
            transform.position = new Vector3(transform.position.x, 3.705f);
        }    

        //transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime, transform.position.y + speedy * Time.deltaTime);
        transform.position += transform.TransformDirection(Vector3.left * speed * Time.deltaTime);
	}

    public void RefreshStats()
    {
        //basicSpeed = GetComponent<EnemyMovementController>().basicSpeed;
        speed = GetComponent<EnemyMovementController>().speed;
    }
}


