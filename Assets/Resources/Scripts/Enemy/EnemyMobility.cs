using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobility : MonoBehaviour {

    public float wantedSpeed;
    public float actualSpeed;
    public float basicSpeed; //potrzebne do przywracania bazowej szybkości przez inne skrypty

    float timeLeft;

    private void Start()
    {
        actualSpeed = wantedSpeed;
        basicSpeed = wantedSpeed;
        basicSpeed *= Informations.difficultyStats[Informations.statistics[5]].enemyMovementSpeedMultiplier;
        wantedSpeed *= Informations.difficultyStats[Informations.statistics[5]].enemyMovementSpeedMultiplier;

    }
    void Update () {
        if(timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if(timeLeft > 0)
        {
            actualSpeed = -4f;
        }
        else if(actualSpeed != wantedSpeed)
        {
            actualSpeed = wantedSpeed;
        }
        if (transform.position.y < -3.7f)
        {
            transform.position = new Vector3(transform.position.x, -3.695f);
        }
        else if (transform.position.y > 3.7f)
        {
            transform.position = new Vector3(transform.position.x, 3.705f);
        }    

        //transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime, transform.position.y + speedy * Time.deltaTime);
        transform.position += transform.TransformDirection(Vector3.left * actualSpeed * Time.deltaTime);
	}
    private void OnTriggerEnter2D(Collider2D col)
    {        
        string _tag = col.gameObject.tag;

        if(_tag == "player")
        {
            GoBack();
        }
    }
    public void GoBack()
    {
        timeLeft = 1f;
    }
        Vector2 PixelPerfectMovement(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }

}
