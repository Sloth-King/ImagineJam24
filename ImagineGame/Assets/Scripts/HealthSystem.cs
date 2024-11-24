using UnityEngine;
using System;
using UnityEngine.UI;

public class HealthSystem
{
    public int maxHealth;
    public int currentHealth;

    public HealthSystem(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        //Debug.Log("Dealt Damage: " + damageAmount);
        currentHealth -= damageAmount;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void Heal(int healAmount)
    {
        Debug.Log("Healed: " + healAmount);
        currentHealth += healAmount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    public void FullHeal()
    {
        currentHealth = maxHealth;
    }
}
