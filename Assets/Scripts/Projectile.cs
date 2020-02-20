using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D r;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y || 
            (transform.position.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).x))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.CompareTag("Enemy"))
        {
        }
    }
}
