using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitParameters : MonoBehaviour
{
    [SerializeField]
    PlayerData playerData;

    private float health;
    public float Health
    {
        get { return health; }
    }

    private float speed;
    public float Speed
    {
        get { return speed; }
    }

    BattleController battleController;

    private void Awake()
    {
        health = playerData.Health;
        speed = playerData.Speed;
        //battleController = GameObject.Find("BattleController").GetComponent<BattleController>();       
    }

    private void Start()
    {
        //WeaponData weaponData = battleController.GetSpawnLevelData.GetWeaponData;
        //speed = weaponData.SpeedOfPlayer;
    }
}
