using System;
using System.Collections.Generic;
using UnityEngine;

class BulletLoader
{
    readonly float speed = 0;
    readonly float damage = 0;
    public BulletLoader(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
    public GameObject Load(string bulletKind)
    {
        GameObject bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Enemy/Bullet" + bulletKind);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        bullet.GetComponent<EnemyBulletMobility>().damage = damage;
        return bullet;
    }
}
