using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControl : MonoBehaviour {

    public float friction;
    public float acceleration;

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
        GetComponent<HeroControl>().acceleration = Characters.characters[Informations.statistics[3]].movementSpeed;// * Characters.charactersUpgrades.movementSpeed;
    }

    // Update is called once per frame
    void Update()
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
            if (force > 0.9f)
            {
                force -= 0.1f;
            }
        }
        if (Input.GetKey(moveDown))
        {
            speedy -= acceleration;
            if (force > 0.9f)
            {
                force -= 0.1f;
            }

        }
        if (Input.GetKey(moveRight))
        {
            speedx += acceleration;
            if (force > 0.8f)
            {
                force -= 0.1f;
            }

        }
        if (Input.GetKey(moveLeft))
        {
            speedx -= acceleration;
            if (force > 0.8f)
            {
                force -= 0.1f;
            }

        }

        speedx *= friction;
        speedy *= friction;

        transform.position = new Vector2(transform.position.x + speedx * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * force, transform.position.y + speedy * (Time.deltaTime + (Time.deltaTime / gm.tempoMeter)) * 0.5f * force);
        //transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime * force, transform.position.y + speedy * Time.deltaTime * force);
        force = 1f;
    }
    /*Vector2 PixelPerfectMovement(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }*/

}
