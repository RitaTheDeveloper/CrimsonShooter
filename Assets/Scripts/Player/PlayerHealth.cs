using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float startingHealth;
    float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    PlayerMovement playerMovement;
    Shooting playerShooting;
    Animator anim;
    float currentHealthView;

    bool isDead;
    public bool IsDead
    {
        get { return isDead; }
    }

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponent<Shooting>();
        startingHealth = GetComponent<UnitParameters>().Health;
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealthView = (currentHealth * 100) / startingHealth;
        UIManager.instance.HealthPlayerDisplay(currentHealthView);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }

        currentHealthView = (currentHealth * 100) / startingHealth;
        UIManager.instance.HealthPlayerDisplay(currentHealthView);

    }

    void Death()
    {
        Debug.Log("игрок мертв");
        isDead = true;
        //когда игрок умирает, он больше не может стрелять из своего пистолета
        playerShooting.enabled = false;
        playerMovement.enabled = false;

        anim.SetTrigger("Die");
    
        playerMovement.enabled = false;
    }

    public void HealthRegen(float healthRechargeRate)
    {
       
        //if (currentHealth < startingHealth)
        //{
            Debug.Log("текущие " + currentHealth + " ск регена " + healthRechargeRate);
            currentHealth += healthRechargeRate;
        if (currentHealth >= startingHealth) currentHealth = startingHealth;
            currentHealthView = (currentHealth * 100) / startingHealth;
            UIManager.instance.HealthPlayerDisplay(currentHealthView);
        //}
    }

}

