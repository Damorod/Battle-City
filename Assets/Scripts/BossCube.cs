using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCube : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public Transform barrilTop;
    public Transform barrilLeft;
    public Transform barrilRight;
    public Rigidbody2D r;
    public float speed;

    bool attacking;
    bool attackingRage = false;

    public HealthSystem healthSystem;

    public GameObject deathEfect;
    public GameObject deathParticules;

    private CamShake shake;

    public HealthBarEnemy healthBar;



    // Start is called before the first frame update
    void Start()
    {
        healthSystem.SetMaxHealth(150);
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();

        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());

        speed = 2f;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.up = player.transform.position - transform.position;
        if (healthSystem.GetCurrentHealth() < 0)
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

    public void At()
    {
        if (!attacking)
        {
            StartCoroutine(attack());
        }
    }
    IEnumerator attack()
    {
        attacking = true;
        GameObject pro = Instantiate(projectile, barrilTop.position, barrilTop.rotation);
        GameObject pro1 = Instantiate(projectile, barrilLeft.position, barrilLeft.rotation);
        GameObject pro2 = Instantiate(projectile, barrilRight.position, barrilRight.rotation);

        pro.GetComponent<Rigidbody2D>().velocity = barrilTop.up * 10f;
        pro1.GetComponent<Rigidbody2D>().velocity = barrilLeft.up * 10f;
        pro2.GetComponent<Rigidbody2D>().velocity = barrilRight.up * 10f;


        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    public void AtRage()
    {
        if (!attackingRage)
        {
            StartCoroutine(AttackRage());
        }
    }
    IEnumerator AttackRage()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        if (hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield"))
        {
            attacking = true;
            GameObject pro = Instantiate(projectile, barrilTop.position, barrilTop.rotation);
            GameObject pro1 = Instantiate(projectile, barrilLeft.position, barrilLeft.rotation);
            GameObject pro2 = Instantiate(projectile, barrilRight.position, barrilRight.rotation);

            pro.GetComponent<Rigidbody2D>().velocity = barrilTop.up * 10f;
            pro1.GetComponent<Rigidbody2D>().velocity = barrilLeft.up * 10f;
            pro2.GetComponent<Rigidbody2D>().velocity = barrilRight.up * 10f;


            yield return new WaitForSeconds(0.1f);
            attacking = false;
        }
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
        healthBar.SetHealth(healthSystem.GetCurrentHealth());
    }
}
