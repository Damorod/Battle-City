using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireProjectile : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().typeWeapon.Add("Fire");
            collision.gameObject.GetComponent<Player>().projectile.gameObject.GetComponent<Projectile>().changeColor(1);
            collision.gameObject.GetComponent<Player>().inv.slots[1].gameObject.GetComponent<Image>().color = Color.red;
            Destroy(gameObject);
        }
    }
}
