using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informations {


    public static int saveNum = 0;
    public static bool firstMenuLoaded = false;
    public static int straightToPDA = 0;


    public static int[] statistics = new int[]
    {
        0,  //chapterNum            0
        0,  //Crystals              1
        0,  //actualBullet          2
        0,  //actualCharacter       3
        0,  //chaptersUnlocked      4
        1   //difficulty            5
    };

    public static int[] quests = new int[]
    {
        0  //enemies killed
    };
    public static int[] upgrades = new int[]
    {
        0,
        0,
        0
    };
    public static int[] actualAbility = new int[]
    {
        0,
        0,
        0
    };
    public static bool[] charactersUnlocked = new bool[]
    {
        true,
        true,
        true,
        true
    };
    public static bool[] questLockedCharacters = new bool[]
{
        false,
        false,
        false,
        true
};
    public static bool[] questLockedBullets = new bool[]
    {
        false,
        true,
    };
    public static bool[] abilitiesUnlocked = new bool[]
    {
        true,
        true,
        true
    };
    public static bool[] questLockedAbilities = new bool[]
{
        false,
        false,
        true
};
    public static bool[] spellsUnlocked = new bool[]
{
        true,
        true,
        true
};
    public static bool[] questLockedSpells = new bool[]
{
        false,
        false,
        true
};
    public static bool[] enemiesUnlocked = new bool[]
    {
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false,
        false   //num:10 amount:11
    };
    public class UpgradesMultiplier
    {
        public float[] cooldownMultiplier;
        public float[] damageMultiplier;
        public float[] healthMultiplier;
        public float[] manaMultiplier;

        public UpgradesMultiplier(float[] _cooldownMultiplier, float[] _damageMultiplier, float[] _healthMultiplier, float[] _manaMultiplier)
        {
            cooldownMultiplier = _cooldownMultiplier;
            damageMultiplier = _damageMultiplier;
            healthMultiplier = _healthMultiplier;
            manaMultiplier = _manaMultiplier;
        }
    }
    public static UpgradesMultiplier upgradesAmount = new UpgradesMultiplier(
        new float[12] { 1f, 0.95f, 0.9f, 0.85f, 0.8f, 0.75f, 0.7f, 0.67f, 0.64f, 0.62f, 0.6f, 0.55f },
        new float[12] { 1f, 1.2f, 1.4f, 1.5f, 1.7f, 1.85f, 2f, 2.15f, 2.3f, 2.45f, 2.6f, 3f },
        new float[12] { 1f, 1.2f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2f, 2.1f, 2.2f, 2.5f },
        new float[12] { 1f, 1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.85f, 1.9f, 2f }
        );

    public static int[] prices = new int[]
    {
        50, 150, 300, 500, 800, 1400, 2000, 2800, 4000, 6000, 10000
    };
    public static int[] heroPrices = new int[]
    {
        0,0,50,0
    };
    public static int[] bulletsPrices = new int[]
    {
        0,50
    };
    public static int[] spellPrices = new int[]
{
        0,0,100
};
    public static int[] abilitesPrices = new int[]
    {
        0,0,100
    };
    public static float[] spellManaCost = new float[3]
    {
            9f,13f,9f,
    };
    public static float[] spellCooldown = new float[3]
    {
            1.6f,6f,2.5f
    };
    public static float[] abilityManaCost = new float[3]
        {
            4f,8f,14f,
        };
    public static float[] abilityCooldown = new float[3]
    {
            18f,18f,12f
    };
    public static float[] specialAbilityPointsNeeded = new float[4]
    {
        20f,20f,4f,1f
    };
    public static float[] specialAbilityMaxPoints = new float[4]
    {
        40f,20f,20f,30f
    };
    public static DifficultyStats[] difficultyStats = new DifficultyStats[3]
        {
            new DifficultyStats (
                _enemyHealthMultiplier: 0.75f,
                _enemyMovementSpeedMultiplier: 0.8f,
                _enemyDamageMultiplier: 0.6f,
                _enemyBulletSpeedMultiplier: 0.8f,
                _enemyAbilityCooldownsMultiplier: 0.75f,
                _bossDamageMultiplier: 0.6f,
                _bossCooldownsMultiplier: 1.35f,
                _bossHealthMultiplier: 0.8f,
                _additionalBossMoves: -1,
                _timeMeterTempo: 1.2f,
                _timeMeterLimits: 0.8f
            ),
            new DifficultyStats (1f,1f,1f,1f,1f,1f,1f,1f,0,1f,1f),
            //new DifficultyStats (1.2f,1.06f,1.2f,1.2f,0.8f,1.2f,0.8f,1.2f,1.05f,1,0.95f,1.15f),
            new DifficultyStats (
                _enemyHealthMultiplier: 1.2f,
                _enemyMovementSpeedMultiplier: 1.06f,
                _enemyDamageMultiplier: 1.2f,
                _enemyBulletSpeedMultiplier: 1.2f,
                _enemyAbilityCooldownsMultiplier: 1.35f,
                _bossDamageMultiplier: 1.1f,
                _bossCooldownsMultiplier: 0.75f,
                _bossHealthMultiplier: 1.2f,
                _additionalBossMoves: 2,
                _timeMeterTempo: 0.8f,
                _timeMeterLimits: 1.4f
            )
        };
    public class DifficultyStats
    {
        public float enemyHealthMultiplier = 1f;
        public float enemyMovementSpeedMultiplier = 1f;
        public float enemyDamageMultiplier = 1f;
        public float enemyBulletSpeedMultiplier = 1f;
        public float enemyAbilityCooldownsMultiplier = 1f;
        public float bossDamageMultiplier = 1f;
        public float bossCooldownsMultiplier = 1f;
        public float bossHealthMultiplier = 1f;
        public int additionalBossMoves = 0;
        public float timeMeterTempo = 1f;
        public float timeMeterLimits = 1f;

        public DifficultyStats (
            float _enemyHealthMultiplier,
            float _enemyMovementSpeedMultiplier,
            float _enemyDamageMultiplier,
            float _enemyBulletSpeedMultiplier,
            float _enemyAbilityCooldownsMultiplier,
            float _bossDamageMultiplier,
            float _bossCooldownsMultiplier,
            float _bossHealthMultiplier,
            int _additionalBossMoves,
            float _timeMeterTempo,
            float _timeMeterLimits
        ){
            enemyHealthMultiplier = _enemyHealthMultiplier;
            enemyMovementSpeedMultiplier = _enemyMovementSpeedMultiplier;
            enemyDamageMultiplier = _enemyDamageMultiplier;
            enemyBulletSpeedMultiplier = _enemyBulletSpeedMultiplier;
            enemyAbilityCooldownsMultiplier = _enemyAbilityCooldownsMultiplier;
            bossDamageMultiplier = _bossDamageMultiplier;
            bossCooldownsMultiplier = _bossCooldownsMultiplier;
            bossHealthMultiplier = _bossHealthMultiplier;
            additionalBossMoves = _additionalBossMoves;
            timeMeterTempo = _timeMeterTempo;
            timeMeterLimits = _timeMeterLimits;
        }
    }
}
