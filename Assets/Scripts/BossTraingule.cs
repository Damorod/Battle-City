using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTraingule : MonoBehaviour
{
    public GameObject player;
    //public GameObject projectile;
    public Transform barrilTop;
    public Transform barrilLeft;
    public Transform barrilRight;
    public Rigidbody2D r;
    public float speed;
    public LineRenderer lr;
    public LineRenderer lrR;
    public LineRenderer lrL;

    public float shootRate;
    private float m_shootRateTimeStamp;

    //bool attacking;


    // Start is called before the first frame update
    void Start()
    {
        shootRate = 2f;

        speed = 2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(Time.time);
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        RaycastHit2D hitInfoR = Physics2D.Raycast(barrilRight.position, barrilRight.up);
        RaycastHit2D hitInfoL = Physics2D.Raycast(barrilLeft.position, barrilLeft.up);
        if((int)Time.time%10 == 1)
        {
            if (!lr.enabled)
            {
                lr.enabled = true;
                lrL.enabled = true;
                lrR.enabled = true;
            }
            transform.RotateAround(transform.position, Vector3.forward, 2f);
            lr.SetPosition(0, new Vector3(barrilTop.position.x, barrilTop.position.y, -1));
            lr.SetPosition(1, new Vector3(hitInfo.point.x, hitInfo.point.y, -1));
            lrL.SetPosition(0, new Vector3(barrilLeft.position.x, barrilLeft.position.y, -1));
            lrL.SetPosition(1, new Vector3(hitInfoL.point.x, hitInfoL.point.y, -1));
            lrR.SetPosition(0, new Vector3(barrilRight.position.x, barrilRight.position.y, -1));
            lrR.SetPosition(1, new Vector3(hitInfoR.point.x, hitInfoR.point.y, -1));
        }
        else
        {
            lr.enabled = false;
            lrL.enabled = false;
            lrR.enabled = false;
            transform.up = player.transform.position - transform.position;
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
                if (hitInfo.collider.CompareTag("Player") || hitInfo.collider.CompareTag("Shield"))
                {
                    if (!lr.enabled)
                    {
                        lr.enabled = true;
                        lrL.enabled = true;
                        lrR.enabled = true;
                    }
                    if (Time.time > m_shootRateTimeStamp)
                    {

                        lr.SetPosition(0, new Vector3(barrilTop.position.x, barrilTop.position.y, -1));
                        lr.SetPosition(1, new Vector3(hitInfo.point.x, hitInfo.point.y, -1));
                        lrL.SetPosition(0, new Vector3(barrilLeft.position.x, barrilLeft.position.y, -1));
                        lrL.SetPosition(1, new Vector3(hitInfoL.point.x, hitInfoL.point.y, -1));
                        lrR.SetPosition(0, new Vector3(barrilRight.position.x, barrilRight.position.y, -1));
                        lrR.SetPosition(1, new Vector3(hitInfoR.point.x, hitInfoR.point.y, -1));
                        StartCoroutine(attack(hitInfo));
                        StartCoroutine(attack(hitInfoR));
                        StartCoroutine(attack(hitInfoL));


                    }
                    else
                    {
                        lr.enabled = false;
                        lrL.enabled = false;
                        lrR.enabled = false;
                    }
                }
            }
        }
    }

   IEnumerator attack(RaycastHit2D hit)
    {
        yield return new WaitForSeconds(.5f);
        m_shootRateTimeStamp =+ Time.time + shootRate;
    }

    void move(Vector2 target, float speedy)
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, target.y), speedy * Time.deltaTime);
    }
}
