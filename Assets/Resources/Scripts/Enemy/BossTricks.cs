using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTricks : MonoBehaviour {

    public AudioSource bossSwordSound;
    public Animator anim;
    public string[] attacks;
    public int attacksNum = 0;
    float attacksTime = 0f;
    float _attacksCooldown;
    public float cooldownTimeChanger = 1f;
    public float speedChanger = 1f;
    public float bossRestingTime;
    public float attacksCooldown;
    public string alternativeString = "";
    public float weakeningMultiplier = 1f;
    int attacksFromRest = 0;
    int num;
    int num1;
    float multiplier;
    float lowHealthBuff;

    public GameObject spawnPoint;
    float cooldown;
    Vector3 basicPosition = new Vector3(6f,0f);
    Vector3 heroPosition;
    Vector3 wantedPosition;
    Vector3 screenSize;
    Vector3 screenPosition;
    int shootnum = 1;
    int moveNum = 1;
    public int shootTimes = 0;
    GameObject bullet;
    float attackTimeLeft;
    int abilitynum;

    public bool hasSword;
    public float swordAttackTime;
    public Collider2D swordColliderNormal;
    public Collider2D swordColliderAttack;

    bool swing = false;
    // Use this for initialization
    void Start () {
        _attacksCooldown = attacksCooldown;
        screenSize = new Vector3(Screen.width, Screen.height, 0f);
        screenPosition = Camera.main.ScreenToWorldPoint(screenSize);
        transform.position = basicPosition;
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet0" + alternativeString);
        attacksNum = Random.Range(0, attacks.Length);

    }
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.GetComponent<BossScript>().health <= 0f)
        {
            Destroy(gameObject.GetComponent<BossTricks>());
        }
        if (swordAttackTime > 0f)
        {
            swordAttackTime -= Time.deltaTime;
        }
        if (attackTimeLeft > 0f)
        {
            attackTimeLeft -= Time.deltaTime * cooldownTimeChanger;
        }
        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime * cooldownTimeChanger;
        }
        if (_attacksCooldown > 0f)
        {
            _attacksCooldown -= Time.deltaTime* cooldownTimeChanger;
        }
        else
        {
            Invoke(attacks[attacksNum], 0f);
        }
        if(hasSword == true)
        {
            if(swordAttackTime >= 0)
            {
                swordColliderAttack.enabled = true;
                swordColliderNormal.enabled = false;
            }
            else
            {
                swordColliderAttack.enabled = false;
                swordColliderNormal.enabled = true;
            }
        }
    }

    public void ShootInFront()
    {
        switch (moveNum)
        {
            case 1:
                PositionTop();
                moveNum++;
                ShootAround(0f,4f,16);
                multiplier = 1f;
                break;
            case 2:
                MoveToPosition(2.8f);
                if (cooldown <= 0f)
                {
                    ShootLeftRandom(16f, 7f * multiplier * weakeningMultiplier, 1,2);
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
                    cooldown = 0.25f / multiplier / weakeningMultiplier * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
                    multiplier += 0.005f;
                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(0f,3f,16);
                    moveNum++;
                    PositionBot();
                }
                break;
            case 3:
                MoveToPosition(3.2f);
                if (cooldown <= 0f)
                {                   
                    ShootLeftRandom(16f,8.5f * multiplier * weakeningMultiplier, 1,2);
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
                    cooldown = 0.23f / multiplier / weakeningMultiplier* Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
                    multiplier += 0.001f;
                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(30f, 5f, 20);
                    moveNum++;
                }

                break;
            case 4:
                ComeBack(5f * Informations.difficultyStats[Informations.statistics[5]].bossHealthMultiplier);
                if (cooldown <= 0f)
                    if (transform.position == basicPosition)
                {
                    EndOfAbility();
                }
                break;
        }
    }  
        public void Charge()
        {
            switch (moveNum)
            {
            case 1:
                moveNum++;
                PositionPlayer();
                ShootAround(0f, 5f,16);
                if (Informations.statistics[5] == 2)
                {
                    ShootAround(10f, 3f, 26);
                }
                break;
            case 2:
                MoveToPosition(7f * Informations.difficultyStats[Informations.statistics[5]].enemyBulletSpeedMultiplier);
                if (Vector3.Distance(transform.position, wantedPosition) < 3f && swing == false)
                {
                    anim.Play("swing");
                    Invoke("SwingSword", 0.11f);
                    ShootLeft(10f, 8f * Informations.difficultyStats[Informations.statistics[5]].enemyBulletSpeedMultiplier, 10,2);
                    swing = true;
                }
                if (transform.position == wantedPosition)
                {
                    moveNum++;
                    //ShootAround(0f, 2.5f,15);
                    swing = false;
                }

                break;
                case 3:
                ComeBack(4.5f * Informations.difficultyStats[Informations.statistics[5]].enemyBulletSpeedMultiplier);
                if (transform.position == basicPosition)
                {
                    if (Informations.statistics[5] == 2)
                    {
                        ShootAround(10f, 3f, 24);
                        
                    }
                    EndOfAbility();
                }
                break;
            }
        }
    public void MovingAndShooting()
    {
        switch (moveNum)
        {
            case 1:
                PositionRandom();
                moveNum++;
                break;
            case 2:
                float stop = Random.Range(0f,100f);
                MoveToPosition(1.8f);
                switch (shootnum)
                {
                    case 1:
                        if (cooldown <= 0f)
                        {
                            ShootAround(0f, 3f,16);
                            cooldown = 1.5f;
                            shootnum = 2;
                            if (Informations.statistics[5] == 2)
                            {
                                ShootLeftSmall(4f, 7f, 5, Random.Range(-25f, 25f), 2);
                            }
                        }

                        break;
                    case 2:
                        if (cooldown <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootAround(10f, 3f,22);
                            cooldown = 1.5f;
                            shootnum = 1;
                        }

                        break;

                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(30f, 5f,16);
                    moveNum++;
                }
                else if (stop > 99.5f)
                {
                    if (Informations.statistics[5] == 2)
                    {
                        ShootAround(30f, 6f, 16);
                    }
                    moveNum++;
                }


                break;
            case 3:
                ComeBack(3f);
                if (transform.position == basicPosition)
                {
                    ShootAround(30f, 6f, 32);
                    EndOfAbility();
                }
                break;
        }

    }
    public void SummonEnemies()
    {
        switch (moveNum)
        {
            case 1:
                /*for (int i = 0; i < 4; i++)
                {
                    float enemyPosition = Random.Range(-3.9f, 3.9f);
                    GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy" + Random.Range(0, 2));
                    enemy.transform.position = new Vector3(8.7f, enemyPosition);
                    Instantiate(enemy);
                }*/
                SummonEnemies(3 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves, new int[2] { 0, 0 });
                PositionTop();
                moveNum++;
                ShootAround(10f,2f,12);
                break;
            case 2:
                MoveToPosition(5f);
                if (transform.position == wantedPosition)
                {
                    SummonEnemies(2, new int[2] { 0, 2 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves});
                    moveNum++;
                    PositionBot();
                    ShootAround(0f,3f,16);
                }
                break;
            case 3:
                MoveToPosition(4f);
                if (transform.position == wantedPosition)
                {
                    if (alternativeString != "")
                    {
                        SummonEnemies(2, new int[2] { 0, 3 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves });
                    }
                    moveNum++;
                    ShootAround(10f,4f,2);

                }
                break;
            case 4:
                ComeBack(3f);
                if (transform.position == basicPosition)
                {
                    EndOfAbility();
                }
                break;
        }
    }
    public void Attack0()
    {
        switch(moveNum) //swing + magic swing
        {
            case 1:
                num = Random.Range(0, 3);
                
                PositionRandom();
                moveNum++;
                attackTimeLeft = 0.5f;
                //ShootAround(0f, 2f,20);
                break;
            case 2:
                MoveToPosition(1.2f);
                multiplier = 1f;
                switch(num1)
                    {
                    case 0:
                        if (attackTimeLeft <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootLeft(22f, 5.5f, 6,2);
                            attackTimeLeft = 0.6f / multiplier;
                            num++;
                            multiplier += 0.15f;
                            num1 = 1;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                if(Informations.statistics[5] == 2)
                                {
                                    ShootLeftMystic(8f, 4.5f, 7);
                                }
                                moveNum++;
                                num = 0;
                            }
                        }
                        break;
                    case 1:
                        if (attackTimeLeft <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootLeft(22f, 6f, 4,2);
                            attackTimeLeft =  0.75f;
                            num++;
                            multiplier += 0.15f;
                            num1 = 0;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                if (Informations.statistics[5] == 2)
                                {
                                    ShootLeftMystic(8f, 4.5f, 6);
                                }
                                moveNum++;
                                num = 0;
                            }
                        }
                        break;
                }
                break;
            case 3:
                if (attackTimeLeft <= 0f)
                {
                    ComeBack(4f);
                    if (transform.position == basicPosition)
                    {
                        anim.Play("swing");
                        Invoke("SwingSword", 0.11f);
                        ShootLeftMystic(18f, 4.5f, 5 + Mathf.Clamp(Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves,-1,1));
                        if (Informations.statistics[5] == 2)
                        {
                            ShootAround(10f, 3f, 16);
                        }
                        EndOfAbility();
                        num1 = 0;
                    }
                }
                break;
        }
    }
    public void Attack1() // swing random direction
    {
        switch (moveNum)
        {
            case 1:
                num = Random.Range(0, 2);
                PositionRandom();
                moveNum++;
                attackTimeLeft = 0.5f;
                ShootAround(0f, 3f,16);
                multiplier = 1f;
                break;
            case 2:
                MoveToPosition(1.4f);
                if (attackTimeLeft <= 0f)
                {
                    anim.Play("swing");
                    Invoke("SwingSword", 0.11f);
                    ShootLeftSmall(24f, 4.5f * multiplier * weakeningMultiplier, 5, Random.Range(-25f, 25f),3);
                    attackTimeLeft = Random.Range(0.6f, 0.8f * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier);
                    num++;
                    multiplier += 0.1f;
                    if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                    {
                        moveNum++;
                        num = 0;
                    }
                }
                break;
            case 3:
                if (attackTimeLeft <= 0f)
                {
                    ComeBack(6f);
                    if (transform.position == basicPosition)
                    {
                        if (Informations.statistics[5] == 2)
                        {
                            ShootAround(10f, 2f, 20);
                        }
                        EndOfAbility();
                    }
                    
                }
                break;
        }
    }
    public void Attack2()
    {
        switch (moveNum) //swing + magic swing
        {
            case 1:
                num = Random.Range(0, 3);

                PositionRandom();
                moveNum++;
                attackTimeLeft = 0.5f;
                //ShootAround(0f, 2f,20);
                break;
            case 2:
                MoveToPosition(1f);
                multiplier = 1f;
                switch (num1)
                {
                    case 0:
                        if (attackTimeLeft <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootLeft(22f, 4.5f * multiplier, 4,0);
                            attackTimeLeft = 0.75f;
                            num++;
                            multiplier += 0.15f;
                            num1 = 1;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                ShootLeft(4f, 5f * Informations.difficultyStats[Informations.statistics[5]].enemyBulletSpeedMultiplier, 3,2);
                                moveNum++;
                                num = 0;
                            }
                            }
                        break;
                    case 1:
                        if (attackTimeLeft <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootLeft(22f, 5f * multiplier, 4,0);
                            attackTimeLeft = 0.75f;
                            num++;
                            multiplier += 0.15f;
                            num1 = 0;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                ShootLeft(12f, 3.5f, 6 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves , 1);
                                moveNum++;
                                num = 0;
                            }
                            if (Informations.statistics[5] == 2)
                            {
                                ShootLeftSmall(4f, 6f, 4, Random.Range(-25f, 25f), 2);
                            }
                        }
                        break;
                }
                break;
            case 3:
                if (attackTimeLeft <= 0f)
                {
                    ComeBack(4f);
                    if (transform.position == basicPosition)
                    {
                        if (Informations.statistics[5] == 2)
                        {
                            ShootAround(10f, 4f, 16);
                        }
                        EndOfAbility();
                        num1 = 0;
                    }
                }
                break;
        }
    }
    public void SwingSword()
    {
        bossSwordSound.Play();
        swordAttackTime = 0.222f;
    }
    public void ShootAround(float startPosition,float speed,int amount)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet0" + alternativeString);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = -180 + startPosition; i < 180 + startPosition; i += 360/amount)
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            bullet.GetComponent<EnemyBulletMobility>().precision = i;
            Instantiate(bullet, spawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeftRandom(float range, float speed, int count, int type)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet" + type + alternativeString);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = 0; i < count; i++)
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
            bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(-range,range);
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
        public void ShootLeft(float range, float speed, int count, int type)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet" + type + alternativeString);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = range; i >= -range; i -= (range * 2) / (count -1f) )
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
            bullet.GetComponent<EnemyBulletMobility>().precision = -i;
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x,spawnPoint.transform.position.y), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeftMystic(float range, float speed, int count)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet1" + alternativeString);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = range; i >= -range; i -= (range * 2) / (count - 1f))
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            bullet.GetComponent<EnemyBulletMobility>().precision = i;
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y - i/20f), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeftSmall(float range, float speed, int count, float direction,int type)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets/Bullets_Boss/Bullet" + type + alternativeString);
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = range; i >= -range; i -= (range * 2) / (count - 1f))
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
            bullet.GetComponent<EnemyBulletMobility>().precision = i + direction;
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void PositionTop()
    {
        wantedPosition = new Vector3(basicPosition.x, screenPosition.y - 0.5f,0);
    }
    public void PositionBot()
    {
        wantedPosition = new Vector3(basicPosition.x, -screenPosition.y + 0.5f, 0);
    }
    public void PositionRandom()
    {
        wantedPosition = new Vector3(Random.Range(-screenPosition.x + 0.5f, screenPosition.x - 0.5f), 
            Random.Range(-screenPosition.y + 0.5f, screenPosition.y - 0.5f));
    }
    public void PositionPlayer()
    {
        wantedPosition = GameObject.FindGameObjectWithTag("player").transform.position;
    }
    public void MoveToPosition(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, speed * Time.deltaTime * speedChanger);
    }
    public void ComeBack(float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, basicPosition, speed * Time.deltaTime * speedChanger);
    }
    public void SummonEnemies(int amount,int[] types)
    {
        for (int i = 0; i < amount; i++)
        {
            float enemyPosition = Random.Range(-2.5f, 2.5f);
            GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy" + Random.Range(types[0], types[1]));
            enemy.transform.position = new Vector3(8.7f, enemyPosition);
            Instantiate(enemy);
        }
    }
    public void EndOfAbility()
    {

        if (transform.GetComponent<BossScript>().health / transform.GetComponent<BossScript>().startHealth > 60)
        {}
        else if (transform.GetComponent<BossScript>().health / transform.GetComponent<BossScript>().startHealth <= 60)
        {
            lowHealthBuff = 0.9f;
        }
        else if (transform.GetComponent<BossScript>().health / transform.GetComponent<BossScript>().startHealth < 30)
        {
            lowHealthBuff = 0.75f;
        }
        else if (transform.GetComponent<BossScript>().health / transform.GetComponent<BossScript>().startHealth < 15)
        {
            lowHealthBuff = 0.6f;
        }
        int lastAttacksNum = attacksNum;
        attacksFromRest++;
        moveNum = 1;
        while(lastAttacksNum == attacksNum)
        {
            attacksNum = Random.Range(0, attacks.Length);
        }
        if (attacksFromRest <= 3)
        {
            _attacksCooldown = attacksCooldown * lowHealthBuff;
        }
        else
        {
            _attacksCooldown = bossRestingTime * lowHealthBuff * lowHealthBuff;
            attacksFromRest = 0;
        }
    }

}
