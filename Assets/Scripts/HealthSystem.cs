using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthMaxChange;
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;

    [SerializeField] private int healthAmount;
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        healthAmount = maxHealth;
    }

    public void Damage(int damageAmount)
    {
        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (isDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmount)
    {
        healthAmount += healAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        healthAmount = maxHealth;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthAmountMax(int maxHealth, bool updateHealthAmount)
    {
        if (maxHealth == 0)
        {
            this.maxHealth = healthAmount;
        }
        else
        {
            this.maxHealth = maxHealth;
        }


        if (updateHealthAmount)
        {
            healthAmount = maxHealth;
        }
        
        OnHealthMaxChange?.Invoke(this, EventArgs.Empty);
    }

    public bool IsFullHealth()
    {
        return healthAmount == maxHealth;
    }

    public bool isDead()
    {
        return healthAmount == 0;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public int GetHealthAmountMax()
    {
        return maxHealth;
    }

    public float GetHealthAmountNormalized()
    {
        Debug.Log(healthAmount);
        Debug.Log(maxHealth);
        return (float) healthAmount / maxHealth;
    }
}