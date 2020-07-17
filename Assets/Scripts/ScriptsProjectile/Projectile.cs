using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int color;

    public Rigidbody2D r;
    public GameObject explosion;
    public GameObject fireExplosion;
    public GameObject iceExplosion;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll != null && coll.gameObject.CompareTag("ExtrasTileMap") || coll.gameObject.CompareTag("WallTileMap"))
        {
            switch (color)
            {
                case 0:
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(fireExplosion, transform.position, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(iceExplosion, transform.position, Quaternion.identity);
                    break;
            }
            Destroy(gameObject);
        }
        else if (coll != null && !coll.gameObject.GetComponent<HealthSystem>().boss && !coll.gameObject.CompareTag("Player") 
            && !coll.gameObject.CompareTag("Item"))
        {
            switch (color)
            {
                case 0:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(5);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    break;
                case 1:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(6);
                    if (coll.gameObject.CompareTag("EnemyMelee") || coll.gameObject.CompareTag("EnemyRange"))
                    {
                        coll.gameObject.GetComponent<EnemyRange>().FireDamage(1);
                    }
                    Instantiate(fireExplosion, transform.position, Quaternion.identity);
                    break;
                case 2:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(4);
                    if (coll.gameObject.CompareTag("EnemyMelee") || coll.gameObject.CompareTag("EnemyRange"))
                    {
                        coll.gameObject.GetComponent<EnemyRange>().slow(0.5f);
                    }
                    Instantiate(iceExplosion, transform.position, Quaternion.identity);
                    break;
            }
            Destroy(gameObject);
        } else if (coll != null && coll.gameObject.GetComponent<HealthSystem>().boss && !coll.gameObject.CompareTag("Item"))
        {
            switch (color)
            {
                case 0:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(5);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    break;
                case 1:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(6);
                    //if (coll.gameObject.CompareTag("Boss") || coll.gameObject.CompareTag("Boss"))
                    //{
                    //    coll.gameObject.GetComponent<BossMovement>().FireDamage(1);
                    //}
                    Instantiate(fireExplosion, transform.position, Quaternion.identity);
                    break;
                case 2:
                    coll.gameObject.GetComponent<HealthSystem>().TakeDamageA(4);
                    //if (coll.gameObject.CompareTag("Boss") || coll.gameObject.CompareTag("Boss"))
                    //{
                    //    coll.gameObject.GetComponent<BossMovement>().slow(0.5f);
                    //}
                    Instantiate(iceExplosion, transform.position, Quaternion.identity);
                    break;
            }
            Destroy(gameObject);
        }
        //if (coll.gameObject.CompareTag("BossTriangule"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<BossMovement>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<BossMovement>().TakeDamage(6);
        //            coll.gameObject.GetComponent<Enemy>().FireDamage(1);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<BossMovement>().TakeDamage(4);
        //            coll.gameObject.GetComponent<Enemy>().slow(0.5f);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}
        //if (coll.gameObject.CompareTag("BossCube"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<BossCircle>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<BossCircle>().TakeDamage(6);
        //            coll.gameObject.GetComponent<Enemy>().FireDamage(1);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<BossCircle>().TakeDamage(4);
        //            coll.gameObject.GetComponent<Enemy>().slow(0.5f);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}
       
        //if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("EnemySmall"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<Enemy>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<Enemy>().TakeDamage(6);
        //            coll.gameObject.GetComponent<Enemy>().FireDamage(1);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<Enemy>().TakeDamage(4);
        //            coll.gameObject.GetComponent<Enemy>().slow(0.5f);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}
        //if (coll.gameObject.CompareTag("EnemyCircle"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<EnemyCircle>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<EnemyCircle>().TakeDamage(6);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<EnemyCircle>().TakeDamage(4);
        //            coll.gameObject.GetComponent<EnemyCircle>().slow(0.5f);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}

        //if (coll.gameObject.CompareTag("SmallEnemyCircle"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<SmallCircle>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<SmallCircle>().TakeDamage(6);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<SmallCircle>().TakeDamage(4);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}

        //if (coll.gameObject.CompareTag("EnemyTriangule"))
        //{
        //    switch (color)
        //    {
        //        case 0:
        //            coll.gameObject.GetComponent<EnemyTriangule>().TakeDamage(5);
        //            Instantiate(explosion, transform.position, Quaternion.identity);
        //            break;
        //        case 1:
        //            coll.gameObject.GetComponent<EnemyTriangule>().TakeDamage(6);
        //            Instantiate(fireExplosion, transform.position, Quaternion.identity);
        //            break;
        //        case 2:
        //            coll.gameObject.GetComponent<EnemyTriangule>().TakeDamage(4);
        //            coll.gameObject.GetComponent<EnemyTriangule>().slow(0.5f);
        //            Instantiate(iceExplosion, transform.position, Quaternion.identity);
        //            break;
        //    }
        //    Destroy(gameObject);
        //}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
    public void changeColor(int color1)
    {
        color = color1;
        switch (color1)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                gameObject.GetComponent<TrailRenderer>().startColor = Color.white;
                gameObject.GetComponent<TrailRenderer>().endColor = Color.white;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                gameObject.GetComponent<TrailRenderer>().startColor = Color.red;
                gameObject.GetComponent<TrailRenderer>().endColor = Color.yellow;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
                gameObject.GetComponent<TrailRenderer>().startColor = Color.cyan;
                gameObject.GetComponent<TrailRenderer>().endColor = Color.white;
                break;
        }
    }

}
