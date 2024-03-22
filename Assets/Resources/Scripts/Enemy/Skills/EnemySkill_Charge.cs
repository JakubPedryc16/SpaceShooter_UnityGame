using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill_Charge : MonoBehaviour {

    public float chargeActivationRange;
    GameObject playerPos;
    EnemyMobility enemyMobility;
    public float speedBoostPerSec = 1f;

    bool chargeOnColor = false;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("player");
        enemyMobility = GetComponent<EnemyMobility>();
    }

    void Update()
    {
        if (transform.position.y < playerPos.transform.position.y + chargeActivationRange && transform.position.y > playerPos.transform.position.y - chargeActivationRange)
        {
            enemyMobility.speed += speedBoostPerSec * 0.01f;
            if (chargeOnColor == false)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 150, 150, 255);
                chargeOnColor = true;
                enemyMobility.speed += 1.5f * speedBoostPerSec;
            }
        }
        else
        {
            enemyMobility.speed = enemyMobility.basicSpeed;
            if (chargeOnColor == true)
            {
                GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                chargeOnColor = false;
            }
        }
    }
}
