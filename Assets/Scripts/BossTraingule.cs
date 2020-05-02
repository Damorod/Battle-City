using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTraingule : MonoBehaviour
{
    public GameObject player;
    public Transform barrilTop;
    public LineRenderer lr;

    public HealthSystem healthSystem;

    public GameObject deathEfect;
    public GameObject deathParticules;

    private CamShake shake;

    public HealthBarEnemy healthBar;


    // Start is called before the first frame update
    void Start()
    {
        healthSystem.SetMaxHealth(150);
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CamShake>();

        healthBar.SetMaxHealth(healthSystem.GetMaxHealth());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (healthSystem.GetCurrentHealth() < 0)
        {
            shake.shaker();
            Instantiate(deathEfect, transform.position, Quaternion.identity);
            Instantiate(deathParticules, transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>().LoadNextScene();
        }
        else if(healthSystem.GetCurrentHealth() < 75)
        {
            GetComponent<Animator>().SetBool("AttackRage", true);
        }
            
    }

    public void asd()
    {
        //transform.up = player.transform.position - transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(barrilTop.position, barrilTop.up);
        if (!lr.enabled)
        {
            lr.enabled = true;
        }
        lr.SetPosition(0, new Vector3(barrilTop.position.x, barrilTop.position.y, -1));
        lr.SetPosition(1, new Vector3(hitInfo.point.x, hitInfo.point.y, -1));
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
        StartCoroutine(flash());
        healthBar.SetHealth(healthSystem.GetCurrentHealth());
    }


    //IEnumerator attack()
    //{
    //    yield return new WaitForSeconds(.5f);
    //    m_shootRateTimeStamp =+ Time.time + shootRate;
    //}
} 
