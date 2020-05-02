using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{

    float spawnTime = 0.5f;
    public GameObject enemyT;

    public GameObject bossTriangule;
    public GameObject bossLife;
    public List<int> test;

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
    bool bossSpawned;
    //bool healthSpawned;
    int i = 0;

    public int maxHealth;
    public int maxShield;

    //public Vector2 positionSpawn;
    public int maxEnemys = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomFire = Random.Range(0, 15);
        randomHealth = Random.Range(0, 30);
        randomIce = Random.Range(0, 15);
        randomShield = Random.Range(0, 30);
       
        StartCoroutine(sapwnTime());

    }

    private void FixedUpdate()
    {
        //if (GameObject.FindGameObjectWithTag("EnemyTriangule") == null && !bossSpawned)
        //{

        //    bossTriangule.SetActive(true);
        //    bossLife.SetActive(true);
        //    bossSpawned = true;
        //}
        if (!fireSpawned)
        {
            if ((int)Time.time == randomFire)
            {
                fireSpawned = true;
                Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
                Instantiate(fire, positionSpawn, Quaternion.identity);
            }
        }
        if (maxHealth < 2)
        {
            if ((int)Time.time == (int)randomHealth)
            {
                randomHealth = Random.Range(randomHealth, 30);
                maxHealth++;
                Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
                Instantiate(health, positionSpawn, Quaternion.identity);
            }
        }
        if (!iceSpawned)
        {
            if ((int)Time.time == randomIce)
            {
                iceSpawned = true;
                Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
                Instantiate(ice, positionSpawn, Quaternion.identity);
            }
        }
        if (maxShield < 2)
        {
            if ((int)Time.time == (int)randomShield)
            {
                randomShield = Random.Range(randomShield, 30);
                maxShield++;
                Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
                Instantiate(shield, positionSpawn, Quaternion.identity);
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
    }

    void spawn()
    {
        if (maxEnemys < 10)
        {
            Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
            Collider2D hits = Physics2D.OverlapCircle(new Vector3(positionSpawn.x, positionSpawn.y, 0), 0.1f);
            if (hits == null)
            {
                GameObject spawned = Instantiate(enemyT, positionSpawn, Quaternion.identity) as GameObject;
                spawned.gameObject.GetComponent<EnemyTriangule>().player = GameObject.FindGameObjectWithTag("Player");
                spawned.gameObject.GetComponent<EnemyTriangule>().numberCode = i;
                test.Add(spawned.gameObject.GetComponent<EnemyTriangule>().numberCode);
                maxEnemys++;
                i++;
            }
            else
            {
                Debug.Log("Funda");
            }
        }
    }

    IEnumerator sapwnTime()
    {
        while(maxEnemys < 10)
        {
            spawn();
            yield return new WaitForSeconds(spawnTime);
        }
    }

}