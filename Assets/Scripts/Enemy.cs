using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public GameObject enemySmall;
    public Transform barril;
    public Rigidbody2D r;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public GameObject bloodStain;
    private CamShake shake;

    public float speed;

    public int maxHealth = 50;
    public int currentHealth;

    public HealthBarEnemy health;

    bool attacking;

    Vector2 target;
    Vector2 inicial;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        inicial = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Vector3.Distance(transform.position, player.transform.position));

        target = inicial;

        transform.up = player.transform.position - transform.position;
        if (currentHealth < 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Instantiate(bloodStain, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if (gameObject.CompareTag("Enemy"))
            {
                GameObject test = Instantiate(enemySmall, new Vector2(transform.position.x + 0.5f, transform.position.y + .5f), Quaternion.identity);
                test.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
                GameObject test1 = Instantiate(enemySmall, new Vector2(transform.position.x + 0.5f, transform.position.y - .5f), Quaternion.identity);
                test1.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
                GameObject test2 = Instantiate(enemySmall, new Vector2(transform.position.x - 0.5f, transform.position.y + .5f), Quaternion.identity);
                test2.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
                GameObject test3 = Instantiate(enemySmall, new Vector2(transform.position.x - 0.5f, transform.position.y - .5f), Quaternion.identity);
                test3.gameObject.GetComponent<Enemy>().player = GameObject.FindGameObjectWithTag("Player");
            }
        }
           
        else
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up);
            if (!hitInfo.collider.CompareTag("ExtrasTileMap"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= 2)
                {
                    move(player.transform.position, speed);
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < 1.8f)
                {
                    move(player.transform.position, -speed);
                }
                if (!attacking && (hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield")))
                {
                    StartCoroutine(attack());
                }
            }
        }
    }
    IEnumerator attack()
    {
        attacking = true;
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
    void move(Vector2 target, float speedy)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speedy * Time.deltaTime);
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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
        speed = 2f;
    }

    public void FireDamage(int fireDamage)
    {
        for(int i = 0; i <=4; i++)
        {
            StartCoroutine(Fire(fireDamage));
            //StartCoroutine(flash());
            //health.SetHealth(currentHealth);
        }
    }

    IEnumerator Fire(int dmg)
    {
        currentHealth -= dmg;
        StartCoroutine(flash());
        health.SetHealth(currentHealth);
        yield return new WaitForSeconds(1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
