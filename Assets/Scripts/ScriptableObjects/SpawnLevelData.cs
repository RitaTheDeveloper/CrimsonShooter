using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Data/NewLevel", order = 54)]
public class SpawnLevelData : ScriptableObject
{
    [Header("Оружие")]
    [SerializeField]
    WeaponData weaponData;
    public WeaponData GetWeaponData
    {
        get { return weaponData; }
    }

    [Tooltip("Список волн")]
    [SerializeField]
    List<SpawnWaveData> spawnWaveDatas;
    public List<SpawnWaveData> SpawnWaveDatas
    {
        get { return spawnWaveDatas; }
    }
}
