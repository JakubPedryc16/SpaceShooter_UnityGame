using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkerMobility : MonoBehaviour
{
    //public float cooldown = 0f;
    //float _cooldown = 0f;
    float basicSpeed;
    float speed;
    int num;
    int moveNum;
    int endOfAbilityNum = 1;
    Vector3 basePosition;
    Vector3 target;
    Vector3 screenSize;
    Vector3 screenPosition;
    GameObject bullet;

    float moveLeftNum;
    float leftSpeed;
    float angle;



    string[] typesOfMovement =
    {
        "GoTop",
        "GoBot",
        "GoRandom",
        "GoCircle"
    };
    void Start()
    {
        basicSpeed = GetComponent<EnemyMovementController>().speed;
        speed = GetComponent<EnemyMovementController>().speed;
        //_cooldown = cooldown;
        screenSize = new Vector3(Screen.width, Screen.height, 0f);
        screenPosition = Camera.main.ScreenToWorldPoint(screenSize);
        basePosition = new Vector3( 5f, 0f , 0f);
        transform.position = basePosition;
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet2");
        num = Random.Range(0, typesOfMovement.Length);
        endOfAbilityNum = 1;
    }

    void Update()
    {
        /*
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
        */
        if (endOfAbilityNum == 1)
        {
            moveNum = Random.Range(0, typesOfMovement.Length);
            endOfAbilityNum = 0;
        }
        Invoke(typesOfMovement[moveNum], 0);

    }

    public void GoTop()
    {
        switch (num)
        {
            case 0:
                PositionTop();
                num = 1;
                break;
            case 1:
                MoveToPosition(speed);
                if(transform.position == target)
                {
                    num++;
                }
                break;
            case 2:
                ComeBack(speed * 1.5f);
                if (transform.position == basePosition)
                {
                    num++;
                }
                break;
            case 3:
                endOfAbilityNum = 1;
                num = 0;
                break;
               
        }
    }

    public void GoBot()
    {
        switch (num)
        {
            case 0:
                PositionBot();
                num = 1;
                break;
            case 1:
                MoveToPosition(speed);
                if (transform.position == target)
                {
                    num++;
                }
                break;
            case 2:
                ComeBack(speed * 2f);
                if (transform.position == basePosition)
                {
                    num++;
                }
                break;
            case 3:
                endOfAbilityNum = 1;
                num = 0;
                break;

        }
    }
    public void GoRandom()
    {
        switch (num)
        {
            case 0:
                PositionRandom();
                num = 1;
                break;
            case 1:
                MoveToPosition(speed);
                if (transform.position == target)
                {
                    num++;
                }
                break;
            case 2:
                ComeBack(speed * 2f);
                if (transform.position == basePosition)
                {
                    num++;
                }
                break;
            case 3:
                endOfAbilityNum = 1;
                num = 0;
                break;

        }
    }
    public void GoCircle()
    {
        switch (num)
        {
            case 0:
                moveLeftNum = 0f;
                angle = 0f;
                num = 1;
                target = new Vector3(basePosition.x - 3f, basePosition.y, 0f);
                break;
            case 1:
                MoveToPosition(speed);
                if(transform.position == target)
                {
                    num++;
                }
                break;
            case 2:
                CircleMovement();
                if (angle < -3*Mathf.PI)
                {
                    num++;
                }
                break;
            case 3:
                ComeBack(speed * 2f);
                if (transform.position == basePosition)
                {
                    num++;
                }
                break;
            case 4:
                endOfAbilityNum = 1;
                num = 0;
                break;

        }
    }

    public void MoveToPosition(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
    public void PositionTop()
    {
        target = new Vector3(basePosition.x, screenPosition.y - 0.5f, 0);
    }
    public void PositionBot()
    {
        target = new Vector3(basePosition.x, -screenPosition.y + 0.5f, 0);
    }
    public void PositionRandom()
    {
        target = new Vector3(Random.Range(-screenPosition.x + 0.5f, screenPosition.x - 0.5f), Random.Range(-screenPosition.y + 0.5f, screenPosition.y - 0.5f));
    }
    public void CircleMovement()
    {
        moveLeftNum += speed * Time.deltaTime;
        angle -= speed * Time.deltaTime;
        transform.localPosition = new Vector2((basePosition.x -3f) + 2 - (Mathf.Cos(angle) * 2f),basePosition.y + (Mathf.Sin(angle) * 2f));
    }
    public void ComeBack(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, basePosition, speed * Time.deltaTime);
    }
}
