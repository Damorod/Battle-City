using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapStage3 : MonoBehaviour
{
    public GameObject bossOne;
    public GameObject bossOneLife;

    public GameObject shield;
    public GameObject health;

    public float randomShield;
    public float randomHealth;

    public bool bossSpawned;

    public int maxHealth;
    public int maxShield;

    public int maxEnemys = 0;
    // Start is called before the first frame update
    void Start()
    {
        randomHealth = Random.Range(0, 30);
        randomShield = Random.Range(0, 30);

    }

    private void FixedUpdate()
    {
        if ((GameObject.FindGameObjectsWithTag("EnemyRange").Length == 0 && GameObject.FindGameObjectsWithTag("EnemyMelee").Length == 0)
            && !bossSpawned && bossOne != null)
        {
            bossSpawned = true;
            bossOne.SetActive(true);
            bossOneLife.SetActive(true);
        }
        else if (bossSpawned)
        {
            if (GameObject.FindGameObjectsWithTag("BossTriangule").Length == 0)
            {
                GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().bossDead = true;
            }
        }
    }
}
