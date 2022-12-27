using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regen : Ability
{
    [SerializeField]
    private AbilityData abilityData;

    [Header("Скорость восстановления здоровья")]
    [SerializeField]
    private float healthRechargeRate = 0.05f;

    [SerializeField]
    private PlayerHealth playerHealth;

    private bool isActivate = false;

    public override AbilityData MyAbilityData { get => abilityData; }

    public override void ActivateAbility()
    {
        Debug.Log("реген активирован");
        isActivate = true;
    }

    private void FixedUpdate()
    {
        if (isActivate && !playerHealth.IsDead)
        {
            Init();
        }
    }

    private void Init()
    {
        playerHealth.HealthRegen(healthRechargeRate);
    }
}
