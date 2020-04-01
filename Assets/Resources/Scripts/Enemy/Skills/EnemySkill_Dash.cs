using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill_Dash : MonoBehaviour {

    public float speed;
    public float cooldown;
    public float range = 1f;
    float _cooldown;
    GameObject enemyObject;
    Vector2 target;
    GameObject hero;
    public float cooldownMultiplierWhenInFront;
    public float multiplierRange;
	// Use this for initialization
	void Start () {
        hero = GameObject.FindGameObjectWithTag("player");
        enemyObject = this.gameObject;
        target = enemyObject.transform.position;
        _cooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
        if(hero.transform.position.y < enemyObject.transform.position.y + multiplierRange && hero.transform.position.y > enemyObject.transform.position.y - multiplierRange)
        {
            cooldown -= Time.deltaTime * (cooldownMultiplierWhenInFront - 1f);
        }
		if(cooldown >= 0f)
        {
            cooldown -= Time.deltaTime;
        }
        if(cooldown <= 0f)
        {
            if(transform.position.y < 2.5f && Random.Range(-1, 2) >= 0f)
            {
                target = new Vector2(enemyObject.transform.position.x - (range/2f), enemyObject.transform.position.y + range);
            }
            else if (transform.position.y > -2.5f && Random.Range(-1, 2) < 0f)
            {
                target = new Vector2(enemyObject.transform.position.x - (range/2f), enemyObject.transform.position.y - range);
            }
            else if(transform.position.y > 2.5f)
            {
                target = new Vector2(enemyObject.transform.position.x - (range / 2f), enemyObject.transform.position.y - range);
            }
            else if(transform.position.y < -2.5f)
            {
                target = new Vector2(enemyObject.transform.position.x - (range / 2f), enemyObject.transform.position.y + range);
            }
            cooldown = _cooldown;
        }
        if(target.x < enemyObject.transform.position.x)
        {
            transform.position = Vector2.MoveTowards(enemyObject.transform.position, target, speed * Time.deltaTime);
        }
	}
}
