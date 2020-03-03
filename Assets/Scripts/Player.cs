using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    float height;
    float widht;
    public GameObject projectile;
    public Transform barril;
    private Vector2 direccion;
    private float angulo;
    private Rigidbody2D r;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar health;
    void Start()
    {
        height = transform.localScale.y;
        widht = transform.localScale.x;
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        projectile.GetComponent<Projectile>().changeColor(0);
        r = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direccion = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
        move();
        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PFire"))
        {
            collision.gameObject.SetActive(false);
            projectile.GetComponent<Projectile>().changeColor(1);            
        }
        if (collision.CompareTag("PIce"))
        {
            collision.gameObject.SetActive(false);
            projectile.GetComponent<Projectile>().changeColor(2);
        }
    }

    void move()
    {
        float x = Input.GetAxisRaw("Horizontal") * 5;
        float y = Input.GetAxisRaw("Vertical") * 5;
        r.velocity = new Vector2(x, y);
    }

    void shoot()
    {
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
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
}

