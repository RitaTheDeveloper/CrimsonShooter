using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Data/Enemy", order = 52)]
public class EnemyData : ScriptableObject
{
    [Tooltip("Название врага")]
    [SerializeField] private string _name;
    public string Name
    {
        get { return _name; }
    }

    [Header("Префаб юнита, его фабрика")]
    [SerializeField] private GameObject prefab;
    public GameObject Prefab
    {
        get { return prefab; }
    }

    [Tooltip("Тип префаба")]
    [SerializeField] private ObjectPooler.ObjectInfo.ObjectType prefabType;
    public ObjectPooler.ObjectInfo.ObjectType PrefabType
    {
        get { return prefabType; }
    }

    //[Tooltip("Модель врага")]
    //[SerializeField] private GameObject model;
    //public GameObject Model
    //{
    //    get { return model; }
    //}

    [Tooltip("Здоровье врага")]
    [SerializeField] private float health;
    public float Health
    {
        get { return health; }
    }

    [Tooltip("Скорость движения")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
    }

    [Tooltip("Урон атаки")]
    [SerializeField] private float damage;
    public float Damage
    {
        get { return damage; }
    }

    [Tooltip("Кд атаки")]
    [SerializeField] private float cdAttack;
    public float CdAttack
    {
        get { return cdAttack; }
    }

    [Tooltip("Кол-во опыта за убийство")]
    [SerializeField] private float experiencePerKill;
    public float ExperiencePerKill
    {
        get { return experiencePerKill; }
    }

    
}
