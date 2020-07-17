using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenShield : MonoBehaviour
{
    public GameObject asd;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Player>().currentShield < 80 && 
                collision.gameObject.GetComponent<Player>().currentShield > 0) 
            {
                collision.gameObject.GetComponent<Player>().AddShield(25);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("ASDASD");
                Instantiate(asd, new Vector3(0, 8.142858f, 0), Quaternion.identity);
            }
        }
    }
}
