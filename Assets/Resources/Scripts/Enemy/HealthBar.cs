using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public SpriteRenderer healthBar;
    EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.health > enemyHealth.maxHealth / 2 )
        {
            healthBar.color = new Color32((byte)((enemyHealth.maxHealth / enemyHealth.health - 1f) * 255f - 255f), 255, 0, 255);
        }
        else 
        {
            healthBar.color = new Color32(255, (byte)(255f - (enemyHealth.maxHealth / (enemyHealth.health + (0.5f * enemyHealth.maxHealth)) - 1f) * 255f), 0, 255);
        }
    }
}
