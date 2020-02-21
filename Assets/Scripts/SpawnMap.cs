using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{

    float spawnTime = 1.0f;
    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sapwnTime());
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void spawn()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        GameObject spawn = Instantiate(wall) as GameObject;
        spawn.transform.position = new Vector2(Random.Range(minScreenBounds.x, maxScreenBounds.x), Random.Range(minScreenBounds.y, maxScreenBounds.y));
    }

    IEnumerator sapwnTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            spawn();
        }
    }
}