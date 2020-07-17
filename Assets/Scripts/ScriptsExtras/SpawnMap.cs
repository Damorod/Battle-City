using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    //float spawnTime = 0.5f;
    //public GameObject enemyT;

    public GameObject bossOne;
    public GameObject bossOneLife;
    public GameObject bossTwo;
    public GameObject bossTwoLife;

    public GameObject fire;
    public GameObject ice;
    public GameObject shield;
    public GameObject health;

    float randomFire;
    float randomIce;
    public float randomShield;
    public float randomHealth;

    bool fireSpawned;
    bool iceSpawned;
    public bool bossSpawned;

    public int maxHealth;
    public int maxShield;

    public int maxEnemys = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomFire = Random.Range(0, 15);
        randomHealth = Random.Range(0, 30);
        randomIce = Random.Range(0, 15);
        randomShield = Random.Range(0, 30);
       
        //StartCoroutine(sapwnTime());

    }

    private void FixedUpdate()
    {
        //Debug.Log(GameObject.FindGameObjectWithTag("EnemyRange"));
        //if ((GameObject.FindGameObjectsWithTag("EnemyRange").Length == 0 && GameObject.FindGameObjectsWithTag("EnemyMelee").Length == 0)
        //    && !bossSpawned && bossOne != null && bossTwo != null)
        //{
        //    bossSpawned = true;
        //    bossOne.SetActive(true);
        //    bossOneLife.SetActive(true);
        //    bossTwo.SetActive(true);
        //    bossTwoLife.SetActive(true);
        //}
        //else if(bossSpawned)
        //{
        //    if(GameObject.FindGameObjectsWithTag("BossTriangule").Length == 0)
        //    {
        //        GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().bossDead = true;
        //    }
        //}
        if (!fireSpawned)
        {
            if ((int)Time.time == randomFire)
            {
                fireSpawned = true;
                RandomSpwan(fire);
            }
        }
        if (maxHealth < 2)
        {
            if ((int)Time.time == (int)randomHealth)
            {
                randomHealth = Random.Range(randomHealth, 30);
                maxHealth++;
                RandomSpwan(health);
            }
        }
        if (!iceSpawned)
        {
            if ((int)Time.time == randomIce)
            {
                iceSpawned = true;
                RandomSpwan(ice);
            }
        }
        if (maxShield < 2)
        {
            if ((int)Time.time == (int)randomShield)
            {
                randomShield = Random.Range(randomShield, 30);
                maxShield++;
                RandomSpwan(shield);
            }
        }
    }

    public void RandomSpwan(GameObject type)
    {
        Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
        Collider2D hits = Physics2D.OverlapCircle(new Vector3(positionSpawn.x, positionSpawn.y, 0), 0.1f);
        if(hits == null)
        {
            Instantiate(type, positionSpawn, Quaternion.identity);
        }
        else
        {
            RandomSpwan(type);
        }

    }
    // Update is called once per frame
    void LateUpdate()
    {
    }

    //void spawn()
    //{
    //    if (maxEnemys < 10)
    //    {
    //        Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
    //        Collider2D hits = Physics2D.OverlapCircle(new Vector3(positionSpawn.x, positionSpawn.y, 0), 0.1f);
    //        if (hits == null)
    //        {
    //            GameObject spawned = Instantiate(enemyT, positionSpawn, Quaternion.identity) as GameObject;
    //            spawned.gameObject.GetComponent<EnemyMelee>().player = GameObject.FindGameObjectWithTag("Player");
    //            spawned.gameObject.GetComponent<EnemyMelee>().numberCode = i;
    //            test.Add(spawned.gameObject.GetComponent<EnemyMelee>().numberCode);
    //            maxEnemys++;
    //            i++;
    //        }
    //        else
    //        {
    //            Debug.Log("Funda");
    //        }
    //    }
    //}

    //IEnumerator sapwnTime()
    //{
    //    while(maxEnemys < 10)
    //    {
    //        spawn();
    //        yield return new WaitForSeconds(spawnTime);
    //    }
    //}

}