using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriangule : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject player;
    public Transform barril;

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
            transform.up = player.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(barril.position, barril.up, 5);

            if (hit.collider != null && !hit.collider.CompareTag("ExtrasTileMap"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= 2 && !hit.collider.CompareTag("EnemyTriangule"))
                {
                    move(player.transform.position, speed);
                }
                else if(Vector3.Distance(transform.position, player.transform.position) < 1.9f && !hit.collider.CompareTag("EnemyTriangule"))
                {
                    move(player.transform.position, -speed);
                }
                if ((hit.collider.CompareTag("Player") || hit.collider.CompareTag("Shield")))
                {
                    if (!lr.enabled)
                        lr.enabled = true;
                    if(Time.time > m_shootRateTimeStamp)
                    {
                        lr.SetPosition(0, new Vector3(barril.position.x, barril.position.y, -1));
                        lr.SetPosition(1, new Vector3(hit.point.x, hit.point.y, -1));
                        if (hit.collider.CompareTag("Player"))
                        {
                            hit.collider.GetComponent<Player>().TakeDamage(1);
                        }
                        else
                        {
                            hit.collider.GetComponent<Player>().TakeDamageShield(1);
                        }
                        StartCoroutine(attack(hit));
                    }
                    else
                    {
                        lr.enabled = false;
                    }
                }
                else
                {
                    lr.enabled = false;
                }
            }
            else
            {
                lr.enabled = false;
            }
        }
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
