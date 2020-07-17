using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform barril;
    private Vector2 direccion;
    private float angulo;
    private Rigidbody2D r;

    public GameObject proyectileAnim;

    public GameObject shield;
    public List<string> typeWeapon;

    bool attacking;
    bool shieldOn;

    public FireBar bar;

    public HealthSystem health;

    public ShieldBar sh;
    public int maxShield;
    public int currentShield;

    public Animator anim;

    public Inventory inv;
    public HealthBar healthBar;
    void Start()
    {
        r = this.GetComponent<Rigidbody2D>();
        shield.SetActive(false);
        maxShield = 80;
        currentShield = maxShield;
        health.SetMaxHealth(100);
        typeWeapon.Add("Normal");
        healthBar.SetMaxHealth(health.GetMaxHealth());
        sh.SetMaxShield(maxShield);

        projectile.GetComponent<Projectile>().changeColor(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(typeWeapon.Count > 0)
        {
            if(Input.GetKey(KeyCode.Alpha1) && typeWeapon.Contains("Normal"))
            {
                projectile.GetComponent<Projectile>().changeColor(0);
                inv.slots[0].gameObject.GetComponent<Image>().color = Color.white;
                inv.slots[1].gameObject.GetComponent<Image>().color = Color.grey;
                inv.slots[2].gameObject.GetComponent<Image>().color = Color.grey;
            }
            if (Input.GetKey(KeyCode.Alpha2) && typeWeapon.Contains("Fire"))
            {
                projectile.GetComponent<Projectile>().changeColor(1);
                inv.slots[1].gameObject.GetComponent<Image>().color = Color.white;
                inv.slots[0].gameObject.GetComponent<Image>().color = Color.grey;
                inv.slots[2].gameObject.GetComponent<Image>().color = Color.grey;
            }
            if (Input.GetKey(KeyCode.Alpha3) && typeWeapon.Contains("Ice"))
            {
                projectile.GetComponent<Projectile>().changeColor(2);
                inv.slots[2].gameObject.GetComponent<Image>().color = Color.white;
                inv.slots[0].gameObject.GetComponent<Image>().color = Color.grey;
                inv.slots[1].gameObject.GetComponent<Image>().color = Color.grey;
            }
        }
       
        direccion = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        barril.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
        move();
        if (Input.GetMouseButtonDown(0))
        {
            if (!attacking && !shieldOn)
            {
                StartCoroutine(attack());
            }
        }
        if (currentShield > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
                shield.transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
                shield.SetActive(true);
                shieldOn = true;
            }
            else
            {
                shield.SetActive(false);
                shieldOn = false;
            }

        }
        else
        {
            shieldOn = false;
            //shield.SetActive(false);
            Destroy(shield);
        }
        

    }

    IEnumerator attack()
    {
        attacking = true;
        bar.Ready(0);
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
        if(transform.localScale.x == 1)
        {
            proyectileAnim.transform.localScale = new Vector3(1, 1, 1);
            Instantiate(proyectileAnim, barril.transform.position, Quaternion.identity);
        }
        else
        {
            proyectileAnim.transform.localScale = new Vector3(-1, 1, 1);
            Instantiate(proyectileAnim, barril.transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        bar.Ready(1);

    }

    void move()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            anim.SetBool("isRunning", true);
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
       
        float x = Input.GetAxisRaw("Horizontal") * 5;
        float y = Input.GetAxisRaw("Vertical") * 5;
        r.velocity = new Vector2(x, y);
    }

    public void AddHealth(int heal)
    {
        health.Heal(heal);
        StartCoroutine(flashHeatlh());
        healthBar.SetHealth(health.GetCurrentHealth());
    }

    public void AddShield(int heal)
    {
        currentShield = Mathf.Clamp(currentShield + heal, 0, 100);
        sh.SetShield(currentShield);
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator flashHeatlh()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        StartCoroutine(flash());
        healthBar.SetHealth(health.GetCurrentHealth());

    }

    public void TakeDamageShield(int damage)
    {
        currentShield -= damage;
        sh.SetShield(currentShield);
    }
}

