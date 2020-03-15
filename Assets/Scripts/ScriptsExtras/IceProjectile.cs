using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IceProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().typeWeapon.Add("Ice");
            collision.gameObject.GetComponent<Player>().projectile.gameObject.GetComponent<Projectile>().changeColor(2);
            collision.gameObject.GetComponent<Player>().inv.slots[2].gameObject.GetComponent<Image>().color = Color.cyan;
            Destroy(gameObject);
        }
    }
}
