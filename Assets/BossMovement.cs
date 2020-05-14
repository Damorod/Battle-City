using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Rigidbody2D r;
    public GameObject player;
    public Transform barril;
    public Vector3 target;

    public float speed;

    public Animator anim;

    public HealthSystem healthSystem;
    public HealthBarEnemy health;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public GameObject bloodStain;
    private CamShake shake;

    Vector2 inicial;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        healthSystem.SetMaxHealth(50);
        inicial = transform.position;
        health.SetMaxHealth(healthSystem.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        target = inicial;

        if (healthSystem.GetCurrentHealth() < 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Instantiate(bloodStain, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            barril.transform.up = player.transform.position - barril.transform.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up, 7);
            anim.ResetTrigger("Attacking");
            if (hitInfo.collider != null && !hitInfo.collider.CompareTag("ExtrasTileMap"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= 3 && !hitInfo.collider.CompareTag("Enemy"))
                {
                    anim.SetBool("isRunning", true);
                    move(player.transform.position, speed);
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < 2.8f && !hitInfo.collider.CompareTag("Enemy"))
                {
                    anim.SetBool("isRunning", true);
                    move(player.transform.position, -speed);
                }
                else
                {
                    anim.SetTrigger("Attacking");
                    anim.SetBool("isRunning", false);
                }
            }
        }

    }
    void move(Vector2 target, float speedy)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speedy * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
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
        speed = 5f;
    }
    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
