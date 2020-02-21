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
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        Vector3 a = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + transform.localScale.x / 2, maxScreenBounds.x - transform.localScale.x / 2),
            Mathf.Clamp(transform.position.y, minScreenBounds.y + transform.localScale.y / 2, maxScreenBounds.y - transform.localScale.y / 2), transform.position.z);
        if(transform.position.x < a.x || transform.position.x > a.x || transform.position.y < a.y || transform.position.y > a.y)
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
            coll.gameObject.GetComponent<TankEnemy>().life--;
            Destroy(gameObject);
        }
    }
}
