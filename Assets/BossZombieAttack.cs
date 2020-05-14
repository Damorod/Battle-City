using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossZombieAttack : MonoBehaviour
{
    public Transform barril;
    public GameObject hammer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hammer.transform.position = Vector2.zero;
        //GetComponent<Rigidbody2D>().velocity = barril.up * 5f;
    }

    public void AttackMace()
    {
        Collider2D hits = Physics2D.OverlapCircle(barril.position, 0.9f, 1 << 10);
        if (hits != null && hits.CompareTag("Player"))
        {
            hits.GetComponent<Player>().TakeDamage(10);
        }
    }

    //public void AttackFlyingHammer()
    //{
    //    hammer.transform.position = Vector3.zero;
    //}
}
