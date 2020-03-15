using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenShield : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().AddShield(20);
            Destroy(gameObject);
        }
    }
}
