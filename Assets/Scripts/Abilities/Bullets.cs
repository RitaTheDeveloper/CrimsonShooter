using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : Ability
{
    [SerializeField]
    private AbilityData abilityData;

    [SerializeField]
    Transform player;

    [SerializeField]
    private ObjectPooler.ObjectInfo.ObjectType bulletType;

    [SerializeField]
    private int numberOfBullets;

    [SerializeField]
    private float cdOfAbility;

    [SerializeField]
    private float speedOfBullet;

    [SerializeField]
    private float radius;

    private bool isActive = false;

    private float timer;

    public override AbilityData MyAbilityData { get => abilityData; }
    public override void ActivateAbility()
    {
        Debug.Log("больше пуль активирован");
        isActive = true;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= cdOfAbility && isActive)
        {
            Init();
        }
    }

    private void Init()
    {
        timer = 0f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Debug.Log("Пули уииии");
            int angle = 360 / numberOfBullets * i;
            Vector3 pos = ArithmeticMethods.RandomCircle(new Vector3(player.position.x, 0.5f, player.position.z), radius, angle);
            Quaternion rot = Quaternion.LookRotation(pos - player.position);
            var bullet = ObjectPooler.instance.GetObject(bulletType, pos, rot);
            bullet.GetComponent<Rigidbody>().velocity = speedOfBullet * bullet.transform.forward;          
        }
    }
}
