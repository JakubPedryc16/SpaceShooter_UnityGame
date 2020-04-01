using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Characters {

    
    public static int characterStatsNum = 0;

    public static ChoosenCharacter charactersUpgrades = new ChoosenCharacter(1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, false,false);


    public static ChoosenCharacter[] characters = new ChoosenCharacter[] //new ChoosenCharacter(10f,8f,1.2f,1f,10f,1.4f,40f,25f,0.4f,1f,true,true),
        {

            new ChoosenCharacter(10f,8f,1.2f,1f,10f,0.9f,40f,20f,0.4f,1f,true,true),
            new ChoosenCharacter(10f,8f,1.44f,1.2f,10f,0.9f,40f,26f,0.52f,1f,false,true),
            new ChoosenCharacter(10f,8f,1.2f,1f,10f,1.08f,48f,20f,0.4f,1f,true,true),
            new ChoosenCharacter(10f,8f,1.2f,1f,10f,0.9f,40f,20f,0.4f,1f,false,true)

        };
    
    public class ChoosenCharacter
    {
        public float damage;
        public float bulletSpeed;
        public float cooldown;
        public float magicDamageMultiplier;
        public float specialAbilityCooldown;
        public float movementSpeed;
        public float health;
        public float mana;
        public float manaRegeneration;
        public float magicCooldownsMultiplier;
        public bool specialBullet;
        public bool specialAbility;


        public ChoosenCharacter(float _damage,float _bulletSpeed,float _cooldown,float _magicDamageMultiplier, float _specialAbilityCooldown, float _movementSpeed, float _health, float _mana, float _manaRegeneration,float _magicCooldownsMultiplier, bool _specialBullet, bool _specialAbility)
        {
            damage = _damage;
            bulletSpeed = _bulletSpeed;
            cooldown = _cooldown;
            magicDamageMultiplier = _magicDamageMultiplier;
            specialAbilityCooldown = _specialAbilityCooldown;
            movementSpeed = _movementSpeed;
            health = _health;
            mana = _mana;
            manaRegeneration = _manaRegeneration;
            magicCooldownsMultiplier = _magicCooldownsMultiplier;
            specialBullet = _specialBullet;
            specialAbility = _specialAbility;
        }
    }
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

}
