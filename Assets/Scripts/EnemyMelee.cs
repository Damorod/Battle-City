using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public GameObject player;
    public Transform barril;

    public Animator anim;
    public GameObject bloodStain;
    private CamShake shake;

    public float shootRate;
    private float m_shootRateTimeStamp;
    public float speed;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public int numberCode;

    public HealthSystem healthSystem;

    public HealthBarEnemy health;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        shootRate = 2;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        healthSystem.SetMaxHealth(50);
        health.SetMaxHealth(healthSystem.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-1, 1, 1);
        }
        //GetComponent<SpriteRenderer>().flipX = player.transform.position.x < transform.position.x;
        if (healthSystem.GetCurrentHealth() <= 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Instantiate(bloodStain, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            barril.transform.up = player.transform.position - barril.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(barril.position, barril.up);
            anim.SetBool("isRunning", false);

            anim.ResetTrigger("Attacking");
            if (hit.collider != null && !hit.collider.CompareTag("ExtrasTileMap"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) > 1.9f && !hit.collider.CompareTag("EnemyTriangule"))
                {
                    anim.SetBool("isRunning", true);
                    move(player.transform.position, speed);
                }else if(Vector3.Distance(transform.position, player.transform.position) <= 1.9f)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetTrigger("Attacking");
                }
               
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(barril.transform.position, 0.9f);
    }

    public void attacking()
    {
        Collider2D hits = Physics2D.OverlapCircle(barril.transform.position, 0.9f, 1 << 10);
        if (hits != null && hits.CompareTag("Player"))
        {
           hits.GetComponent<Player>().TakeDamage(10);
        }
    }
    public void UpdateHealthBar()
    {
        health.SetHealth(healthSystem.GetCurrentHealth());
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

    IEnumerator attack(RaycastHit2D hit)
    {

        yield return new WaitForSeconds(.5f);
        m_shootRateTimeStamp =+ Time.time + shootRate;
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
        health.SetHealth(healthSystem.GetCurrentHealth());
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void move(Vector2 target, float speedy)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speedy * Time.deltaTime);
    }
}
