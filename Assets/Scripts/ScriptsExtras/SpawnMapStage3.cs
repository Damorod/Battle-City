using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapStage3 : MonoBehaviour
{
    float spawnTime = 0.5f;
    public GameObject enemy;

    public GameObject bossCube;

    public List<GameObject> test;

    public GameObject shield;
    public GameObject health;
    public GameObject bossLife;

    float randomShield;
    int maxShield;
    float randomHealth;
    int maxHealth;

    bool bossSpawned;

    public List<int> activeEnemys;

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
        if (GameObject.FindGameObjectWithTag("Enemy") == null && !bossSpawned)
        {
            bossCube.SetActive(true);
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
        int i = 0;
        if(maxEnemys < 10)
        {
            Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
            Collider2D hits = Physics2D.OverlapCircle(new Vector3(positionSpawn.x, positionSpawn.y, 0), 0.1f);
            if (hits == null)
            {
                GameObject spawned = Instantiate(enemy, positionSpawn, Quaternion.identity) as GameObject;
                spawned.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
                spawned.gameObject.GetComponent<Enemy>().setNumber(i);
                maxEnemys++;
                i++;
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
