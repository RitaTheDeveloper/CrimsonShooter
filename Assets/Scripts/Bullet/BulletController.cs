using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IPooledObject
{
    public ObjectPooler.ObjectInfo.ObjectType Type => type;

    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType type;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Bullet")
        {
            //Destroy(gameObject);
            ObjectPooler.instance.DestroyObject(gameObject);
        }

        if (other.gameObject.tag == "Enemy")
        {
            GameObject enemy = other.gameObject;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            enemy.GetComponent<EnemyHealth>().TakeDamage(player.GetComponent<Shooting>().Damage);
        }
    }

}
