using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;


    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;

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
        currentHealth += health;
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;
    }
}
