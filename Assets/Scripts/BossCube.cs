using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCube : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public Transform barrilTop;
    public Transform barrilLeft;
    public Transform barrilRight;
    public Rigidbody2D r;
    public float speed;

    bool attacking;


    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.up = player.transform.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        if (!hitInfo.collider.CompareTag("ExtrasTileMap"))
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= 2)
            {
                move(player.transform.position, speed);
            }
            else if (Vector3.Distance(transform.position, player.transform.position) < 1.8f)
            {
                move(player.transform.position, -speed);
            }
            if (!attacking && hitInfo.collider.CompareTag("Player"))
            {
                StartCoroutine(attack());
            }
        }
    }

    IEnumerator attack()
    {
        attacking = true;
        GameObject pro = Instantiate(projectile, barrilTop.position, barrilTop.rotation);
        GameObject pro1 = Instantiate(projectile, barrilLeft.position, barrilLeft.rotation);
        GameObject pro2 = Instantiate(projectile, barrilRight.position, barrilRight.rotation);

        pro.GetComponent<Rigidbody2D>().velocity = barrilTop.up * 10f;
        pro1.GetComponent<Rigidbody2D>().velocity = barrilLeft.up * 10f;
        pro2.GetComponent<Rigidbody2D>().velocity = barrilRight.up * 10f;


        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    void move(Vector2 target, float speedy)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speedy * Time.deltaTime);
    }
}
