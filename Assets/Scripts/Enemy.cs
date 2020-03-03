using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public Transform barril;
    public Rigidbody2D r;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public GameObject bloodStain;
    private CamShake shake;

    public int maxHealth = 50;
    public int currentHealth;

    public HealthBarEnemy health;

    bool attacking;

    Vector2 target;
    Vector2 inicial;

    // Start is called before the first frame update
    void Start()
    {
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        inicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        target = inicial;

        transform.up = player.transform.position - transform.position;
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
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up);
            if (!hitInfo.collider.CompareTag("ExtrasTileMap"))
            {
                move();
                if (!attacking && hitInfo.collider.CompareTag("Player"))
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
    void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y), 2f * Time.deltaTime);    
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
