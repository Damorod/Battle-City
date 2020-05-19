using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : MonoBehaviour
{
    public Rigidbody2D r;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ExtrasTileMap") || coll.gameObject.CompareTag("WallTileMap"))
        {
            Destroy(gameObject);
        }
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<Player>().TakeDamage(20);
            Destroy(gameObject);
        }
        if (coll.gameObject.CompareTag("Shield"))
        {
            coll.gameObject.GetComponentInParent<Player>().TakeDamageShield(5);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
