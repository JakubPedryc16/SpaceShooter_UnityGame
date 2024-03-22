using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float angle = 0f;
    public float speed = 0f;
    public float basicSpeed;
    float speed_;
    public float radius = 5;
    public bool moveLeft;
    float moveLeftNum;
    public float LeftSpeed;
    Vector2 basePosition;
    private void Start()
    {
        basePosition = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -2.8f, 2.8f));
        basicSpeed = GetComponent<EnemyMovementController>().speed;
        speed = GetComponent<EnemyMovementController>().speed;
        //speed_ = (2f * Mathf.PI) / speed;

    }
    void Update()
    {
        moveLeftNum += LeftSpeed * Time.deltaTime;
        angle -= speed * Time.deltaTime;
        if (moveLeft == false)
        {
            transform.localPosition = new Vector2(-moveLeftNum + Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }
        else
        {
            transform.position = new Vector2(basePosition.x - moveLeftNum + Mathf.Cos(angle) * radius, basePosition.y + Mathf.Sin(angle) * radius);
        }
        //transform.position += transform.TransformDirection(Vector3.left * speed_ * Time.deltaTime);
    }
}
