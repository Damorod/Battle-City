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

    private CamShake shake;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        healthSystem.SetMaxHealth(150);
        SpawnSmallCircle();
        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());

    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.GetCurrentHealth() == 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
        smallCircle.GetComponent<SmallCircle>().SetPos(2);
        Instantiate(smallCircle, transform.position, Quaternion.identity);
    }
}
