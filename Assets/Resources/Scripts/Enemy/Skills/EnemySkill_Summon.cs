using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill_Summon : MonoBehaviour {

    float _cooldown;
    public float cooldown = 3f;
    public int type = 0;
    public int amount = 1;
    public int enemiesAtOnce = 1;
    public float[] positions;
    public GameObject enemy;
	// Use this for initialization
	void Start () {
        _cooldown = cooldown;
        enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy" + type);
	}
	
	// Update is called once per frame
	void Update () {
        if(_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
        }
        if(_cooldown <= 0f && amount > 0)
        {
            for (int i = 0; i < enemiesAtOnce; i++)
            {
                enemy.transform.position = new Vector3(transform.position.x - 0.8f, transform.position.y + positions[i], 0f);
                Instantiate(enemy);
                _cooldown = cooldown;
            }
            amount--;
        }
		
	}
}
