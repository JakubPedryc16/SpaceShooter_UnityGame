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
    int attacksFromRest = 0;
    int num;
    int num1;

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
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet0");
        attacksNum = Random.Range(0, attacks.Length);

    }
	
	// Update is called once per frame
	void Update () {

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
                ShootAround(0f,3f,20);
                break;
            case 2:
                MoveToPosition(2.2f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier); //sytuacja jak niżej
                if (cooldown <= 0f)
                {
                    bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet2");
                    bullet.GetComponent<EnemyBulletMobility>().speed = 5f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier; //bullet speed ma zbyt małą wartość zatem zastępuje tą wartością
                    bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(-20f + (Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves * 2f), 20f - (Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves * 2f));
                    Instantiate(bullet, spawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
                    cooldown = 0.14f * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(0f,3f,20);
                    moveNum++;
                    PositionBot();
                }
                break;
            case 3:
                MoveToPosition(2.2f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier); // sytuacja jak wyżej
                if (cooldown <= 0f)
                {
                    bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet2");
                    bullet.GetComponent<EnemyBulletMobility>().speed = 5f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier; //bullet speed ma zbyt małą wartość zatem zastępuje tą wartością
                    bullet.GetComponent<EnemyBulletMobility>().precision = Random.Range(-20f + (Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves * 2f), 20f - (Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves * 2f));
                    Instantiate(bullet, spawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));
                    FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
                    cooldown = 0.14f * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier;
                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(30f, 3f, 25);
                    moveNum++;
                }

                break;
            case 4:
                ComeBack(4f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier);// sytuacja jak wyżej
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
                ShootAround(0f, 4f,20);
                if (Informations.statistics[5] == 3)
                {
                    ShootAround(10f, 3f, 26);
                }
                break;
            case 2:
                MoveToPosition(7f);
                if (Vector3.Distance(transform.position, wantedPosition) < 4f && swing == false)
                {
                    anim.Play("swing");
                    Invoke("SwingSword", 0.11f);
                    ShootLeft(10f, 5f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier, 10f);
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
                ComeBack(3f * Informations.difficultyStats[Informations.statistics[5]].bossHealthHealthMultiplier); // aby zwiększyć wartość health zamiast speed
                if (transform.position == basicPosition)
                {
                    if (Informations.statistics[5] == 3)
                    {
                        ShootAround(10f, 2f, 30);
                        
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
                            ShootAround(0f, 4f,20);
                            cooldown = 1.5f;
                            shootnum = 2;
                        }

                        break;
                    case 2:
                        if (cooldown <= 0f)
                        {
                            ShootAround(10f, 4f,25);
                            cooldown = 1.5f;
                            shootnum = 1;
                        }

                        break;

                }
                if (transform.position == wantedPosition)
                {
                    ShootAround(30f, 5f,20);
                    moveNum++;
                }
                else if (stop > 99.5f)
                {
                    ShootAround(30f, 5f,20);
                    moveNum++;
                }


                break;
            case 3:
                ComeBack(3f);
                if (transform.position == basicPosition)
                {
                    ShootAround(30f, 5f, 39);
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
                SummonEnemies(4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves, new int[2] { 0, 0 });
                PositionTop();
                moveNum++;
                ShootAround(10f,1.5f,15);
                break;
            case 2:
                MoveToPosition(2.5f);
                if (transform.position == wantedPosition)
                {
                    SummonEnemies(3, new int[2] { 0, 2 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves});
                    moveNum++;
                    PositionBot();
                    ShootAround(0f,2.5f,20);
                }
                break;
            case 3:
                MoveToPosition(3f);
                if (transform.position == wantedPosition)
                {
                    moveNum++;
                    ShootAround(10f,4f,25);
                }
                break;
            case 4:
                ComeBack(4f);
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
                MoveToPosition(0.8f);
                switch(num1)
                    {
                    case 0:
                        if (attackTimeLeft <= 0f)
                        {
                            anim.Play("swing");
                            Invoke("SwingSword", 0.11f);
                            ShootLeft(25f, 6f, 8f);
                            attackTimeLeft = Random.Range(0.5f, 0.8f * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier);
                            num++;
                            num1 = 1;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                if(Informations.statistics[5] == 3)
                                {
                                    ShootLeftMystic(10f, 4.5f, 8);
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
                            ShootLeft(25f, 6f, 5f);
                            attackTimeLeft = Random.Range(0.5f, 0.8f);
                            num++;
                            num1 = 0;
                            if (num >= 4 + Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves)
                            {
                                if (Informations.statistics[5] == 3)
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
                    ComeBack(6f);
                    if (transform.position == basicPosition)
                    {
                        anim.Play("swing");
                        Invoke("SwingSword", 0.11f);
                        ShootLeftMystic(18f, 4.5f, 7f + Mathf.Clamp(Informations.difficultyStats[Informations.statistics[5]].additionalBossMoves,-1,1));
                        if (Informations.statistics[5] == 3)
                        {
                            ShootAround(10f, 3f, 20);
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
                num = Random.Range(0, 3);
                PositionRandom();
                moveNum++;
                attackTimeLeft = 0.5f;
                ShootAround(0f, 2f,20);
                break;
            case 2:
                MoveToPosition(1f);
                if (attackTimeLeft <= 0f)
                {
                    anim.Play("swing");
                    Invoke("SwingSword", 0.11f);
                    ShootLeftSmall(18f, 4.2f,5f, Random.Range(-25f, 25f));
                    attackTimeLeft = Random.Range(0.5f, 0.7f * Informations.difficultyStats[Informations.statistics[5]].bossCooldownsMultiplier);
                    num++;
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
                        if (Informations.statistics[5] == 3)
                        {
                            ShootAround(10f, 3f, 20);
                        }
                        EndOfAbility();
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
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet0");
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = -180 + startPosition; i < 180 + startPosition; i += 360/amount)
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            bullet.GetComponent<EnemyBulletMobility>().precision = i;
            Instantiate(bullet, spawnPoint.transform.position, new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeft(float range, float speed, float count)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet2");
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = range; i >= -range; i -= (range * 2) / (count -1f) )
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound0");
            bullet.GetComponent<EnemyBulletMobility>().precision = -i;
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x,spawnPoint.transform.position.y), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeftMystic(float range, float speed, float count)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet1");
        bullet.GetComponent<EnemyBulletMobility>().speed = speed;
        for (float i = range; i >= -range; i -= (range * 2) / (count - 1f))
        {
            FindObjectOfType<AudioManager>().Play("EnemyShotSound1");
            bullet.GetComponent<EnemyBulletMobility>().precision = + i;
            Instantiate(bullet, new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y - i/20f), new Quaternion(0f, 0f, 0f, 0f));

        }
    }
    public void ShootLeftSmall(float range, float speed, float count, float direction)
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullets_Boss/Bullet3");
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
        wantedPosition = new Vector3(Random.Range(-screenPosition.x + 0.5f, screenPosition.x - 0.5f), Random.Range(-screenPosition.y + 0.5f, screenPosition.y - 0.5f));
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
            float enemyPosition = Random.Range(-3.9f, 3.9f);
            GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Enemy" + Random.Range(types[0], types[1]));
            enemy.transform.position = new Vector3(8.7f, enemyPosition);
            Instantiate(enemy);
        }
    }
    public void EndOfAbility()
    {
        int lastAttacksNum = attacksNum;
        attacksFromRest++;
        moveNum = 1;
        while(lastAttacksNum == attacksNum)
        {
            attacksNum = Random.Range(0, attacks.Length);
        }
        if (this.GetComponent<BossScript>().health > this.GetComponent<BossScript>().startHealth * 0.33f)
        {
            if (attacksFromRest <= 3)
            {
                _attacksCooldown = attacksCooldown;
            }
            else
            {
                _attacksCooldown = bossRestingTime;
                attacksFromRest = 0;
            }
        }
        else
        {
            if (attacksFromRest <= 5)
            {
                _attacksCooldown = attacksCooldown * 0.6f;
            }
            else
            {
                _attacksCooldown = bossRestingTime;
                attacksFromRest = 0;
            }
        }
    }

}
