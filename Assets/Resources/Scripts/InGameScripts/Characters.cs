using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Characters {

    
    public static int characterStatsNum = 0;

    public static ChoosenCharacter charactersUpgrades = new ChoosenCharacter(1f, 1f, 1f, 1f, 1f, false,false);


    public static ChoosenCharacter[] characters = new ChoosenCharacter[] //new ChoosenCharacter(10f,8f,1.2f,1f,10f,1.4f,40f,25f,0.4f,1f,true,true),
        {
                                //dmg   bspeed  cooldwn    movspd   HP      specialbullet  ability    
            new ChoosenCharacter(10f,   8f,     1.2f,         0.8f,   50f,    true,          true),
            new ChoosenCharacter(10f,   8f,     0.7f,       1f,     50f,    false,         true),
            new ChoosenCharacter(10f,   8f,     0.6f,       1.15f,  60f,    true,          true),
            new ChoosenCharacter(10f,   8f,     0.7f,       1f,     50f,    false,         true)

        };

    public static Weapon[] weapons =
    {   // reload ClipSize gapTime bulletsCount
        new Weapon(3.2f,    12,   0.3f,    3),
        new Weapon(1.5f,  10,   0f,      1),
        new Weapon(0.8f,   3,   0f,      1),
        new Weapon(2.1f,     6,   0.1f,    2)

    };
    public class ChoosenCharacter
    {
        public float damage;
        public float bulletSpeed;
        public float cooldown;
        public float movementSpeed;
        public float health;
        public bool specialBullet;
        public bool specialAbility;


        public ChoosenCharacter(float _damage,float _bulletSpeed,float _cooldown, float _movementSpeed, float _health, bool _specialBullet, bool _specialAbility)
        {
            damage = _damage;
            bulletSpeed = _bulletSpeed;
            cooldown = _cooldown;
            movementSpeed = _movementSpeed;
            health = _health;
            specialBullet = _specialBullet;
            specialAbility = _specialAbility;
        }
    }
    public class Weapon
    {
        public float reloadTime;
        public int clipSize;
        public float gapTime;
        public int bulletsAtOnce;
        //public float fatigue;
        public Weapon(float _reloadTime, int _clipSize, float _gapTime, int _bulletsAtOnce)
        {
            reloadTime = _reloadTime;
            clipSize = _clipSize;
            gapTime = _gapTime;
            bulletsAtOnce = _bulletsAtOnce;
            //fatigue = _fatigue;
        }
    }
    /*
    public static BuffCharacter[] buffCharacters = new BuffCharacter[]
    {
            new BuffCharacter(5f,6f,0.18f,3f,180f,3,1.44f,1,0),
            new BuffCharacter(2.5f,3f,0.24f,1f,30f,3,1.44f,3,1),
            new BuffCharacter(5f,8f,0.2f,5f,0f,1,1.44f,2,2)

    };

    public class BuffCharacter
    {
        public float damage;
        public float bulletSpeed;
        public float cooldown;
        public float range;
        public float directionLimits;
        public int xTimes;
        public float movementSpeed;
        public int durability;
        public int playerNum;


        public BuffCharacter(float _damage, float _bulletSpeed, float _cooldown,float _range, float _directionLimits,int _xTimes, float _movementSpeed, int _durability, int _playerNum)
        {
            damage = _damage;
            bulletSpeed = _bulletSpeed;
            cooldown = _cooldown;
            range = _range;
            directionLimits = _directionLimits;
            xTimes = _xTimes;
            movementSpeed = _movementSpeed;
            durability = _durability;
            playerNum = _playerNum;
        }
    }
    */

}
