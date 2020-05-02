using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCircle : MonoBehaviour
{
    public GameObject player;
    public GameObject smallCircle;

    public HealthSystem healthSystem;
    public HealthBarEnemy healthBar;
    public GameObject deathEfect;
    public Transform barril;

    public GameObject deathParticules;
    public Vector3 target;
    public float speed;

    public Collider2D test;

    public int maxCircles;

    private CamShake shake;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        healthSystem.SetMaxHealth(150);
        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SpawnCircle());
        if(healthSystem.GetCurrentHealth() == 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadNextScene();
        }
        else if (healthSystem.GetCurrentHealth() < 75)
        {
            GetComponent<Animator>().SetBool("AttackRage", true);
        }

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(15);
        }
    }

    public void SpawnSaw()
    {
        float limit;
        bool vertical;

        Vector2 positionSpawn = new Vector2(Random.Range(-14, 14), Random.Range(-9, 5));

        limit = Random.Range(1f, 4f);

        int a = Random.Range(0, 2) * 2 - 1;
        if (a == -1)
        {
            vertical = true;
        }
        else
        {
            vertical = false;
        }
        smallCircle.GetComponent<SmallCircle>().SetPos(limit);
        smallCircle.GetComponent<SmallCircle>().vertical = vertical;
        test = Physics2D.OverlapCircle(positionSpawn, limit);     
        if (!test)
        {
            Instantiate(smallCircle, positionSpawn, Quaternion.identity);
        }

    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
        healthBar.SetHealth(healthSystem.GetCurrentHealth());
    }

    public void slow(float sw)
    {
        StartCoroutine(slows(sw));
    }

    IEnumerator slows(float s)
    {
        speed = s;
        yield return new WaitForSeconds(1f);
        speed = 5f;
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SpawnSmallCircle()
    {

    }

    IEnumerator SpawnCircle()
    {
        if (maxCircles < 5)
        {
            SpawnSaw();
            maxCircles++;
        }
        yield return new WaitForSeconds(1f);
    }
}
