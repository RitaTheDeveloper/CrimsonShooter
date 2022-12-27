using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Data/Ability", order = 54)]
public class AbilityData : ScriptableObject
{
    [Header("Имя способности")]
    [SerializeField]
    string _name;
    public string Name
    {
        get { return _name; }
    }

    [Header("Иконка способности")]
    [SerializeField]
    Sprite sprite;
    public Sprite GetSprite
    {
        get { return sprite; }
    }

    [Header("Описание способности")]
    [TextArea(3,6)]
    [SerializeField]
    string description;
    public string Description
    {
        get { return description; }
    }
  
}
