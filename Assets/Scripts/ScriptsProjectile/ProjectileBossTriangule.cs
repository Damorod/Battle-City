using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBossTriangule : MonoBehaviour
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ExtrasTileMap") || collision.gameObject.CompareTag("WallTileMap"))
        {
            StartCoroutine(bounce());
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(15);
            Destroy(gameObject);
        }
    }

    IEnumerator bounce()
    {
        r.velocity = new Vector2(-r.velocity.x, -r.velocity.y);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
