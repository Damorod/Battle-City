using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour
{
    public Rigidbody2D r;
    public GameObject player;
    public Transform barril;
    public Vector3 target;

    public int maxHealth = 50;
    public int currentHealth;

    public float speed;

    public HealthBarEnemy health;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public GameObject bloodStain;
    private CamShake shake;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        target = player.transform.position;
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Instantiate(bloodStain, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            transform.up = player.transform.position - transform.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up);
            if (hitInfo.collider.CompareTag("ExtrasTileMap"))
            {
                target = transform.position;
            }
            else if ((target == transform.position))
            {
                target = player.transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
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
        currentHealth -= damage;
        StartCoroutine(flash());
        health.SetHealth(currentHealth);
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
}
