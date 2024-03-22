using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAnim : MonoBehaviour
{
    public GameObject animStartLook;
    public float cooldownAnim;
    float _cooldownAnim;
    bool animLookRefreshed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldownAnim > 0)
        {
            GetComponent<EnemyMobility>().speed = GetComponent<EnemyMobility>().basicSpeed * 3f;
            _cooldownAnim -= Time.deltaTime;
        }
        else if (animLookRefreshed == false)
        {
            GetComponent<EnemyMobility>().speed = GetComponent<EnemyMobility>().basicSpeed;
            animStartLook.SetActive(true);
            animLookRefreshed = true;
        }
        else if (GetComponent<EnemyShooting>()._cooldown <= 0.01f)
        {
            _cooldownAnim = cooldownAnim;
            animLookRefreshed = false;
            animStartLook.SetActive(false);
        }
    }
}
