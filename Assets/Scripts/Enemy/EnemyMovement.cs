using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    //GameObject model;


    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }
    private void Start()
    {        
        nav.speed = GetComponent<EnemyParameters>().Speed;
        //anim = GetComponentInChildren<Animator>(); // аниматор висит на модельке, а не на основном объекте
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
        if (enemyHealth.CurrentHealth > 0f && playerHealth.CurrentHealth > 0f)
        {
            nav.SetDestination(player.position);
            anim.SetBool("IsRunning", true);
           // model.transform.localPosition = Vector3.zero; // делаем для того, чтобы коллайдеры модельки не смещались при анимации. Так как коллайдеры висят на пустом объекте, а не на самой модельке            
        }
        else
        {
            nav.enabled = false;
            anim.SetBool("IsRunning", false);
        }
    }
}


