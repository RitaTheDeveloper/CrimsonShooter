using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "Data/Player", order = 53)]
public class PlayerData : ScriptableObject
{
    [Tooltip("Имя игрока")]
    [SerializeField] private string _name;
    public string Name
    {
        get { return _name; }
    }

    [Tooltip("Модель")]
    [SerializeField] private GameObject model;
    public GameObject Model
    {
        get { return model; }
    }

    [Tooltip("Здоровье стартовое")]
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

}
