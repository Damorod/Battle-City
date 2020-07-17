using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenHealth : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<Player>().health.GetCurrentHealth() < 100)
            {
                collision.gameObject.GetComponent<Player>().AddHealth(30);
                Destroy(gameObject);
            }
        }
    }
}
