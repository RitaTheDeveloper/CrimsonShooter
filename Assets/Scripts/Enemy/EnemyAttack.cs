using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float timeBetweenAttacks;
    float attackDamage;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        attackDamage = GetComponent<EnemyParameters>().Damage;
        timeBetweenAttacks = GetComponent<EnemyParameters>().CdAttack;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        //если время больше, чем время между двумя атаками и игрок в радиусе атаки, то враг его атакует
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.CurrentHealth > 0f)
        {
            Attack();
        }
    }

    void Update()
    {
        

        // если здоровье игрока равно нулю, срабатывает триггер "игрок мертв"
        if (playerHealth.CurrentHealth <= 0f)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack()
    {
        timer = 0f;
       
        if (playerHealth.CurrentHealth > 0f)
        {
            playerHealth.TakeDamage(attackDamage);
            if (anim == null)
            {
                Debug.Log("аниматор не найден");
            }
                anim.SetTrigger("Attack");
        }
    }
}


