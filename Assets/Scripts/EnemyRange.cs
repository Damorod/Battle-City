using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    //public GameObject enemySmall;
    public Transform barril;
    public Rigidbody2D r;

    public GameObject deathEfect;
    public GameObject deathParticules;

    public GameObject bloodStain;
    private CamShake shake;

    public int numberCode;

    public float speed;

    public Animator anim;

    public HealthSystem healthSystem;

    public HealthBarEnemy healthBar;

    bool attacking;

    Vector2 target;
    Vector2 inicial;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();
        healthSystem.SetMaxHealth(50);
        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());
        inicial = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
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
                //face right
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player.transform.position.x < transform.position.x)
            {
                //face left
                transform.localScale = new Vector3(-1, 1, 1);
            }
            barril.transform.up = player.transform.position - barril.transform.position;
            anim.ResetTrigger("isAttacking");
            RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up, 7);
            if (hitInfo.collider != null && !hitInfo.collider.CompareTag("ExtrasTileMap"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) >= 2 && !hitInfo.collider.CompareTag("Enemy"))                 
                {
                    anim.SetBool("isRunning", true);
                    move(player.transform.position, speed);
                }
                else if (Vector3.Distance(transform.position, player.transform.position) < 1.8f && !hitInfo.collider.CompareTag("Enemy"))
                {
                    anim.SetBool("isRunning", true);
                    move(player.transform.position, -speed);
                }
                else
                {
                    anim.SetBool("isRunning", false);
                }
                if ((hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield")))
                {
                    anim.SetTrigger("isAttacking");
                }
            }
        }
    }

    public void Shoot()
    {
        GameObject pro = Instantiate(projectile, barril.position, Quaternion.identity);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
    }

    public void setNumber(int num)
    {
        numberCode = num;
    }
    public IEnumerator attack()
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

    public void UpdateHealthBar()
    {
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
        speed = 2f;
    }

    public void FireDamage(int fireDamage)
    {
        for(int i = 0; i <=4; i++)
        {
            StartCoroutine(Fire(fireDamage));
        }
    }

    IEnumerator Fire(int dmg)
    {
        healthSystem.TakeDamage(dmg);
        StartCoroutine(flash());
        healthBar.SetHealth(healthSystem.GetCurrentHealth());
        yield return new WaitForSeconds(1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 3);
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
