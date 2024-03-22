using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemySkillSniper : MonoBehaviour
{
    public float sniperModeCooldownTimer;
    public float sniperModeRange;
    bool colorBack;
    EnemyShooting enemyShooting;
    GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        enemyShooting = GetComponent<EnemyShooting>();
        hero = GameObject.FindGameObjectWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        if (hero.transform.position.y >= transform.position.y - sniperModeRange && hero.transform.position.y <= transform.position.y + sniperModeRange)
        {
            colorBack = false;
            GetComponent<SpriteRenderer>().color = new Color32(255, 150, 150, 255);
            enemyShooting._cooldown -= Time.deltaTime * sniperModeCooldownTimer;
        }
        else if (colorBack == false)
        {
            colorBack = true;
            GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        }
    }
}
