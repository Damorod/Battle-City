﻿using System.Collections;
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

    public GameObject shield;
    public List<string> typeWeapon;

    bool attacking;
    bool shieldOn;

    public FireBar bar;

    public HealthSystem health;

    public ShieldBar sh;
    public int maxShield = 70;
    public int currentShield;

    public Inventory inv;
    public HealthBar healthBar;
    void Start()
    {
        r = this.GetComponent<Rigidbody2D>();
        shield.SetActive(false);
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
            }
            if (Input.GetKey(KeyCode.Alpha2) && typeWeapon.Contains("Fire"))
            {
                projectile.GetComponent<Projectile>().changeColor(1);
            }
            if (Input.GetKey(KeyCode.Alpha3) && typeWeapon.Contains("Ice"))
            {
                projectile.GetComponent<Projectile>().changeColor(2);
            }
        }
        direccion = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angulo - 90f);
        move();
        if(currentShield > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
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
            shield.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (!attacking && !shieldOn)
            {
                StartCoroutine(attack());
            }
        }

    }

    IEnumerator attack()
    {
        attacking = true;
        bar.Ready(0);
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        bar.Ready(1);

    }

    void move()
    {
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
        currentShield += heal;
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

