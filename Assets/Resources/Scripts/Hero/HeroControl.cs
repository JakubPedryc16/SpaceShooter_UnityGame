using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour {

    public float friction = 1f;
    public float acceleration;
    //public float fatigue = 1;

    GameMaster gm;

    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;

    public float speedx;
    public float speedy;

    float force = 1f;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("gameMaster").GetComponent<GameMaster>();
        acceleration = Characters.characters[Informations.statistics[3]].movementSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (transform.position.x < -7.264f)
        {
            transform.position = new Vector3(-7.259f, transform.position.y);
        }
        else if (transform.position.x > 7.264f)
        {
            transform.position = new Vector3(7.259f, transform.position.y);
        }
        if (transform.position.y < -3.9f)
        {
            transform.position = new Vector3(transform.position.x, -3.905f);
        }
        else if (transform.position.y > 3.9f)
        {
            transform.position = new Vector3(transform.position.x, 3.895f);
        }


        if (Input.GetKey(moveUp))
        {
            speedy += acceleration;
            force -= 0.14f;
        }
        if (Input.GetKey(moveDown))
        {
            speedy -= acceleration;
            force -= 0.14f;
        }
        if (Input.GetKey(moveRight))
        {
            speedx += acceleration;
            force -= 0.14f;
        }
        if (Input.GetKey(moveLeft))
        {
            speedx -= acceleration;
            force -= 0.14f;
        }

        speedx *= friction;
        speedy *= friction;
        //fatigue = System.Math.Clamp(fatigue + 0.005f, 0.5f, 1f);

        transform.position = new Vector2(transform.position.x + speedx * 0.025f * force, transform.position.y + speedy * 0.025f * force);
        force = 1f;

        //fatigue = System.Math.Clamp(fatigue + 0.001f, 0.5f, 1f);
    }
    /*Vector2 PixelPerfectMovement(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }*/

}
