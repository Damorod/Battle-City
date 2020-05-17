using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttack : MonoBehaviour
{
    public Transform barril;
    public GameObject hammer;
    public GameObject Player;
    Vector3 target;
    Vector3 origin;
    public bool flying;

    // Start is called before the first frame update
    void Start()
    {
        flying = false;
        origin = hammer.transform.position;
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if(!flying)
        //{
        //    //hammer.transform.RotateAround(hammer.transform.position, Vector3.forward, 2f);
        //    hammer.transform.position = Vector2.MoveTowards(hammer.transform.position, target, 3f * Time.deltaTime);
        //    if (hammer.transform.position == target)
        //    {
        //        hammer.transform.position = Vector2.MoveTowards(hammer.transform.position, origin, 3f + Time.deltaTime);
        //    }
        //}
        
    }

    public void AttackMace()
    {
        Collider2D hits = Physics2D.OverlapCircle(barril.position, 0.9f, 1 << 10);
        if (hits != null && hits.CompareTag("Player") || hits.CompareTag("Shield"))
        {
            if (hits.CompareTag("Player"))
            {
                hits.GetComponent<Player>().TakeDamage(10);
            }else if (hits.CompareTag("Shield"))
            {
                hits.GetComponent<Player>().TakeDamageShield(15);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(barril.transform.position, 0.9f);
    }
}
