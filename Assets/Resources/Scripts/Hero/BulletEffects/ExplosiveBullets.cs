using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullets : MonoBehaviour {

    public int num;
    public float damage;
    BulletMobility bulletMobility;
    public int effect = 0; // 0-nothing, 1-freeze
	// Use this for initialization
	void Start () {
        bulletMobility = GetComponent<BulletMobility>();
        //bulletMobility.effect = effect;
        bulletMobility.explosive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(bulletMobility.disappearTime <= 0 || bulletMobility.durability <= 0)
        {
            GameObject explosion = Resources.Load<GameObject>("Prefabs/Bullets_1Spell/Explosions/Explosion" + num);
            explosion.GetComponent<BulletMobility>().damage = damage * Informations.upgradesAmount.damageMultiplier[Informations.upgrades[1]];
            explosion.transform.position = transform.position;
            explosion.GetComponent<BulletMobility>().effect = effect;
            Instantiate(explosion);
            Destroy(bulletMobility.gameObject);

        }
	}
}
