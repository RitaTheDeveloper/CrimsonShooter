using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    [Header("Начальное кол-во опыта, которое нужно получить, чтобы открыть способности")]
    [SerializeField]
    float startingingExperience;

    [Header("Множитель опыта")]
    [SerializeField]
    float experienceMultiplier;

    [Header("Максимальный левел")]
    [SerializeField]
    int maxLvl;

    float currentExperience; // текущее кол-во опыта
    
    int currentLvl;
    GameObject player;

    private void Start()
    {
        currentLvl = 1;
        currentExperience = 0f;
    }

    public void LevelUp()
    {
        if (currentLvl < maxLvl)
        {
            currentLvl++;
            currentExperience = 0f;
            UIManager.instance.LevelUp(currentLvl);
        }
    }

    public void IncreaseInCurrExp( float exp)
    {
        currentExperience += exp;
        float maxExp;            // максимальное количество опыта для получения следующего уровня
        maxExp = startingingExperience * Mathf.Pow(experienceMultiplier, currentLvl - 1);

        // если текущее кол-во опыта превышает максимальное для этого левела опыт, то вызываем метод повышения левела
        if (currentExperience >= maxExp)
        {
            LevelUp();
        }
    }
}
