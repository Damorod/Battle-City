using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyExplotion : MonoBehaviour
{
    Vector3 target;
    public GameObject player;
    bool live;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
       // target = player.transform.position;
        GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(target);
        if (!live)
        {
            if ((int)transform.position.x - 0.5f < (int)target.x &&
                 (int)target.x < (int)transform.position.x + 0.5f &&
                 (int)transform.position.y - 0.5f < (int)target.y &&
                 (int)target.y < (int)transform.position.y + 0.5f)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<ParticleSystem>().Play();
                StartCoroutine(TimeLive());
            }
            if (!GetComponent<SpriteRenderer>().enabled)
            {
                Collider2D hits = Physics2D.OverlapCircle(transform.position, 0.9f, 1 << 10);
                if (hits != false && hits.CompareTag("Player"))
                {
                    hits.GetComponent<Player>().TakeDamage(10);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator TimeLive()
    {
        yield return new WaitForSeconds(4f);
        live = true;
    }

}
