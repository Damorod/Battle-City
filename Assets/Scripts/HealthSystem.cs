using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public bool boss;
    public bool player;


    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;

    }

    public void SetBoss(bool boss)
    {
        this.boss = boss;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Heal(int health)
    {

        currentHealth = Mathf.Clamp(GetCurrentHealth() + health, 0, 100);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }

    IEnumerator flash()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void TakeDamageA(int damage)
    {
        TakeDamage(damage);
        StartCoroutine(flash());
        if(!boss && !player)
        {
            if(gameObject.GetComponent<EnemyMelee>() != null)
            {
                gameObject.GetComponent<EnemyMelee>().UpdateHealthBar();
            }
            else
            {
                gameObject.GetComponent<EnemyRange>().UpdateHealthBar();
            }
        }
        else if(boss && !player)
        {
            gameObject.GetComponent<BossMovement>().UpdateHealthBar();            
        }

    }

}
