using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Data/Weapon", order = 51)]
public class WeaponData : ScriptableObject
{
    [Tooltip("Тип пули")]
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType bulletType;
    public ObjectPooler.ObjectInfo.ObjectType BulletType
    {
        get { return bulletType; }
    }

    [Tooltip("Количество в обойме патронов")]
    [SerializeField] private int ammoCount;
    public int AmmoCount
    {
        get { return ammoCount; }
    }

    [Tooltip("Количество пуль за выстрел")]
    [SerializeField] private int numberOfBulletsPershot;
    public int NumberOfBulletsPershot
    {
        get { return numberOfBulletsPershot; }
    }

    [Tooltip("Скорость полета снаряда")]
    [SerializeField] private float shootSpeed;
    public float ShootSpeed
    {
        get { return shootSpeed; }
    }

    [Tooltip("Кд между выстрелами")]
    [SerializeField] private float cdBetweenShots;
    public float CdBetweenShots
    {
        get { return cdBetweenShots; }
    }

    [Tooltip("Кд обоймы")]
    [SerializeField] private float clipReload;
    public float ClipReload
    {
        get { return clipReload; }
    }

    [Tooltip("Максимальный рендж атаки")]
    [SerializeField] private float maxAttackRange;
    public float MaxAttackRange
    {
        get { return maxAttackRange; }
    }

    [Tooltip("Разброс угла выстрела")]
    [SerializeField] private float maxAngleDispersion;
    public float MaxAngleDispersion
    {
        get { return maxAngleDispersion; }
    }

    [Tooltip("Урон")]
    [SerializeField] private float damage;
    public float Damage
    {
        get { return damage; }
    }

    [Tooltip("Скорость бега игрока")]
    [SerializeField] private float speedOfPlayer;
    public float SpeedOfPlayer
    {
        get { return speedOfPlayer; }
    }

}
