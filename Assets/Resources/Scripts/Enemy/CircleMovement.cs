using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float angle = 0f;
    public float speed = 0f;
    float speed_;
    public float radius = 5;
    private void Start()
    {
    speed_ = (2f * Mathf.PI) / speed;
    }
    void Update()
    {
        angle -= speed * Time.deltaTime;
        transform.localPosition = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
    }
}
