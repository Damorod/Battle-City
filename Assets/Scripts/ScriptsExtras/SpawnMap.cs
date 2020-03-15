using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{

    float spawnTime = 1.0f;
    public GameObject wall;
    public GameObject enemy;

    //public Vector2 positionSpawn;
    bool va;
    int maxEnemys = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sapwnTime());
    }

    // Update is called once per frame
    void LateUpdate()
    {
    }

    void spawn()
    {
        
        if(maxEnemys < 10)
        {
            //Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            //Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));
            Collider[] hits = Physics.OverlapSphere(positionSpawn, 3f);
            if (hits.Length == 0)
            {
                GameObject spawned = Instantiate(enemy, positionSpawn, Quaternion.identity) as GameObject;
                spawned.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
                maxEnemys++;
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