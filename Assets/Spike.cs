using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public bool attacking;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < transform.position.y)
        {
            if(Vector3.Distance(transform.position, player.transform.position) <= 0.1)
            {
                gameObject.GetComponent<Animator>().SetBool("isOnTop", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isOnTop", false);
            }
        }
        else if(Vector3.Distance(transform.position, player.transform.position) <= 0.9)
        {
            gameObject.GetComponent<Animator>().SetBool("isOnTop", true);
        }else
        {
            gameObject.GetComponent<Animator>().SetBool("isOnTop", false);
        }
    }

    public void Damage()
    {
        Collider2D hits = Physics2D.OverlapBox(transform.position, Vector3.one, 1 << 10);
        if (hits != null && hits.CompareTag("Player"))
        {
            hits.GetComponent<Player>().TakeDamage(1);
        }
    }
}
