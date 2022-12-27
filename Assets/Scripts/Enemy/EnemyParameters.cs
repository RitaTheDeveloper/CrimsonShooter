using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameters : MonoBehaviour
{
    [Tooltip("Ссылка на сриптейблобжект")]
    [SerializeField]
    EnemyData enemyData;
    public void SetEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
    }

    private float health;
    public float Health
    {
        get { return enemyData.Health; }
    }

    private float speed;
    public float Speed
    {
        get { return enemyData.Speed; }
    }

    private float damage;
    public float Damage
    {
        get { return enemyData.Damage; }
    }

    private float cdAttack;
    public float CdAttack
    {
        get { return enemyData.CdAttack; }
    }

    private float experiencePerKill;
    public float ExperiancePerKill
    {
        get { return enemyData.ExperiencePerKill; }
    }

    GameObject newModel;
    GameObject oldModel;

    private void OnEnable()
    {
        //Init();
    }


    public void Init()
    {
        oldModel = gameObject.transform.Find("Model").gameObject;
        //newModel = enemyData.Model;
        Destroy(oldModel); // уничтожаем старую модель
        GameObject model = Instantiate(newModel, transform.position, transform.rotation); // создаем новую модель
        model.transform.parent = gameObject.transform; // дочерним объектом
        model.name = "Model"; // называем ее так, как нам нужно
    }
}
