using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDemonAttack : MonoBehaviour
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
        if (Vector3.Distance(transform.position, player.transform.position) >= 3 && hitInfo.collider != null
            && !hitInfo.collider.CompareTag("Enemy"))
        {
            if (hitInfo.collider != null && (!attacking && hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield")))
            {
                StartCoroutine(Shoot());
            }
        }
        //else
        //{
        //}
    }
    IEnumerator Shoot()
    {
        attacking = true;
        GameObject pro = Instantiate(projectile, barril.position, barril.rotation);
        pro.GetComponent<Rigidbody2D>().velocity = barril.up * 10f;
        yield return new WaitForSeconds(3f);
        attacking = false;
    }

    public void AttackClose()
    {
        Collider2D hits = Physics2D.OverlapCircle(barril.transform.position, 0.9f, 1 << 10);
        if (hits != false && hits.CompareTag("Player"))
        {
            hits.GetComponent<Player>().TakeDamage(20);
        }
    }
}
