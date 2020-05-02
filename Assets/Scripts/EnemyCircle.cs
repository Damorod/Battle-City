using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        target = player.transform.position;
        healthSystem.SetMaxHealth(50);
        health.SetMaxHealth(healthSystem.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {

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
                //face right
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            barril.transform.up = player.transform.position - barril.transform.position;
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up, 2);
            if (hitInfo.collider != null && (hitInfo.collider.CompareTag("ExtrasTileMap") || hitInfo.collider.CompareTag("EnemyCircle")))
            {
                target = transform.position;
            }
            else if ((target == transform.position))
            {
                target = player.transform.position;
            }
            else
            {
                anim.SetBool("isRunning", true);
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
