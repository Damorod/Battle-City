using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapStage2 : MonoBehaviour
{
    float spawnTime = 4f;
    public GameObject enemyC;

    public GameObject bossCircle;

    public List<GameObject> test;

    public GameObject shield;
    public GameObject health;

    float randomShield;
    int maxShield;
    float randomHealth;
    int maxHealth;

    bool bossSpawned;
    public GameObject bossLife;
    //bool shieldSpawned;
    //bool healthSpawned;

    public int maxEnemys = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sapwnTimeCircle());
        randomHealth = Random.Range(0, 30);
        randomShield = Random.Range(0, 30);
    }
    private void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("EnemyCircle") == null && !bossSpawned)
        {

            bossCircle.SetActive(true);
            bossLife.SetActive(true);
            bossSpawned = true;
        }
        if (maxHealth <= 2)
        {
            if ((int)Time.time == randomHealth)
            {
                randomHealth = Random.Range(randomHealth, 30);
                maxHealth++;
                Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
                Instantiate(health, positionSpawn, Quaternion.identity);
            }
        }
        if (maxShield <= 2)
        {
            if ((int)Time.time == randomShield)
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

    void spawnCircle()
    {
        if (maxEnemys < 1)
        {
            Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
            Collider[] hits = Physics.OverlapSphere(new Vector3(positionSpawn.x, positionSpawn.y, 0), 3f);
            if (hits.Length == 0)
            {
                GameObject spawned = Instantiate(enemyC, positionSpawn, Quaternion.identity) as GameObject;
                spawned.gameObject.GetComponent<EnemyCircle>().player = GameObject.FindGameObjectWithTag("Player");
                maxEnemys++;
            }
        }
    }

    IEnumerator sapwnTimeCircle()
    {
        while (maxEnemys < 10)
        {
            spawnCircle();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
