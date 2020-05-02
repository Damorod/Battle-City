using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThrows : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public Transform barril;
    public bool attacking;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(barril.position, barril.up, 7);
        if ((!attacking && hitInfo && hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield")) )
        {
            StartCoroutine(Shoot());
        }  

    }
    IEnumerator Shoot()
    {

        attacking = true;
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
        yield return new WaitForSeconds(3f);
        attacking = false;
    }
}
