using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCircle : MonoBehaviour
{
    bool vertical;
    float limitX = 2f;
    float limitY = 2f;
    public Rigidbody2D rb;

    Vector2 nextPos;

    public LineRenderer lr;

    Vector3 posLr;

    public GameObject player;
    public HealthSystem healthSystem;
    public HealthBarEnemy healths;



    // Start is called before the first frame update
    void Start()
    {
        posLr = transform.position;
        healthSystem.SetMaxHealth(20);
        healths.SetMaxHealth(20);

        int a = Random.Range(0, 2) * 2 - 1;
        if (a == -1)
        {
            vertical = true;
            nextPos = new Vector2(transform.position.x, transform.position.y + limitY);
        }
        else
        {
            vertical = false;
            nextPos = new Vector2(transform.position.x + limitX, transform.position.y);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.GetCurrentHealth() == 0)
        {
            Destroy(gameObject);
        }
        if (vertical)
        {
            lr.SetPosition(0, new Vector3(transform.position.x, posLr.y + limitY, -1));
            lr.SetPosition(1, new Vector3(transform.position.x, posLr.y - limitY, -1));
            transform.position = Vector2.MoveTowards(transform.position, nextPos, 3f * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPos) <= 0.1)
            {
                changePos();
            }
        }
        else
        {
            lr.SetPosition(0, new Vector3(posLr.x + limitX, transform.position.y, -1));
            lr.SetPosition(1, new Vector3(posLr.x - limitX, transform.position.y, -1));
            transform.position = Vector2.MoveTowards(transform.position, nextPos, 3f * Time.deltaTime);
            if (Vector2.Distance(transform.position, nextPos) <= 0.1)
            {
                changePos();
            }
        }
    }

    public void SetPos(float limit)
    {
        if (vertical)
        {
            limitX = limit;
        }
        else
        {
            limitY = limit;
        }
    }

    void changePos()
    {
        if (vertical)
        {
            if (nextPos.y > transform.position.y)
            {
                nextPos = new Vector2(transform.position.x, transform.position.y - (limitY*2));
            }
            else
            {
                nextPos = new Vector2(transform.position.x, transform.position.y + (limitY*2));
            }
        }
        else
        {
            if (nextPos.x > transform.position.x)
            {
                nextPos = new Vector2(transform.position.x - (limitX*2) , transform.position.y);
            }
            else
            {
                nextPos = new Vector2(transform.position.x + (limitX*2), transform.position.y);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallTileMap") || collision.gameObject.CompareTag("ExtrasTileMap"))
        {
            changePos();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamageShield(1);
        }else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
        healths.SetHealth(healthSystem.GetCurrentHealth());
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
