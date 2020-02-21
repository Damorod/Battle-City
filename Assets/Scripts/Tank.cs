﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    // Start is called before the first frame update

    float height;
    float widht;
    public GameObject projectile;
    public Transform barril;
    private Vector2 direccion;
    private float angulo;
    private Rigidbody2D r;

    void Start()
    {

        height = transform.localScale.y;
        widht = transform.localScale.x;
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

}

