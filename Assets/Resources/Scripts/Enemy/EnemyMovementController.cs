using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    public float speed;
    //float actualSpeed;
    //public float basicSpeed;

    float timeLeft;

    // Start is called before the first frame update
    void Awake()
    {
        //actualSpeed = wantedSpeed;
        speed *= Informations.difficultyStats[Informations.statistics[5]].enemyMovementSpeedMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
