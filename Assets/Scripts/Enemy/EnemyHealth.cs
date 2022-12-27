using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    float startingHealth = 100; // начальное количество хп
    float currentHealth; // текущее количество хп
    public float CurrentHealth
    {
        get { return currentHealth; }
    }
    int currentHealthView;
    Animator anim;
    BattleController battleController;
    UpgradeController upgradeController;
    float expPerKill; // опыт за убийство юнита
    bool isDead;
    public bool IsDied
    {
        get { return isDead; }
    }

    private void Awake()
    {
        
    }

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        startingHealth = GetComponent<EnemyParameters>().Health;
        expPerKill = GetComponent<EnemyParameters>().ExperiancePerKill;
        currentHealth = startingHealth;
        currentHealthView =(int) ((currentHealth * 100) / startingHealth);
        HealthDisplay(currentHealthView);
        battleController = GameObject.Find("BattleController").GetComponent<BattleController>();
        upgradeController = battleController.GetComponent<UpgradeController>();
        isDead = false;
    }

    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
            Death();

        currentHealthView = (int)((currentHealth * 100) / startingHealth);
        HealthDisplay(currentHealthView);
    }

    void Death()
    {
        if (anim == null) anim = GetComponentInChildren<Animator>();

        isDead = true;
        if (anim != null) anim.SetTrigger("Dead");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<SphereCollider>().enabled = false;

        // вызываем метод увелечения текущего полученного опыта
        if( upgradeController != null) upgradeController.IncreaseInCurrExp(expPerKill);

        Destroy(gameObject, 2f);
    }

    private void HealthDisplay(int currHealth)
    {
        healthSlider.value = currHealth;
    }

}
