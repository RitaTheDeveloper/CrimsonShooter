using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnWaveData wave;
    float timer;
    GameObject player;
    PlayerHealth playerHealth;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= wave.CdWave && !gameObject.GetComponent<EnemyHealth>().IsDied && !playerHealth.IsDead)
        {
            wave.SpawnWave(transform.position);
            timer = 0f;
        }
    }
}
