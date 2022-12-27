using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyWave", menuName = "Data/EnemyWave", order = 53)]
public class SpawnWaveData : ScriptableObject
{
    List<EnemyData> enemyDatas;

    [Header("Тип врага")]
    [SerializeField]
    EnemyData enemyData;

    [Header("Число этих врагов в одной волне")]
    [SerializeField]
    int countOfEnemies;

    [Header("Кулдаун спавна волны")]
    [SerializeField]
    float cdWave;
    public float CdWave
    {
        get { return cdWave; }
    }


    [Header("Используем область спавна прямоугольник")]
    [SerializeField]
    public bool isRectangle = true;

    [Header("Координаты спавна по Х")]
    [SerializeField]
    [Range(-27f, 27f)]
    public float xMin = -27f;

    [SerializeField]
    [Range(-27f, 27f)]
    public float xMax = 27f;

    [Header("Координаты спавна по Z")]
    [SerializeField]
    [Range(-20f, 20f)]
    public float zMin = -20f;

    [SerializeField]
    [Range(-20f, 20f)]
    public float zMax = 20f;    

    [Header("Используем область спавна окружность")]
    [SerializeField]
    bool isCircle = false;

    [Header("Радиус области спавна")]
    [SerializeField]
    float radius;

    [Header("Центр окружности")]
    [SerializeField]
    float x0, z0; // c этим может быть проблема, потому что выходят ошибки, если враги на равном расстоянии от героя, герой не может определиться в кого стрелять

    [Header("Спавнить рандомно внутри круга")]
    [SerializeField]
    bool randomInCircle = false;

    Vector3 center;


    public void SpawnWave()
    {
        if (isRectangle) SpawnByRectangle();
        else if (isCircle)
        {
            center = new Vector3(x0, enemyData.Prefab.transform.position.y, z0);
            SpawnByCircle();
        }        
    }

    public List<EnemyData> EnemyDatas()
    {
        enemyDatas = new List<EnemyData>();
        for (int i = 0; i < countOfEnemies; i++)
        {
            enemyDatas.Add(enemyData);
        }

        return enemyDatas;
    }

    // метод для юнита-спавнера
    public void SpawnWave(Vector3 centerCircle)
    {
        isCircle = true;
        isRectangle = false;
        center = new Vector3(centerCircle.x, enemyData.Prefab.transform.position.y, centerCircle.z);
        SpawnByCircle();
    }

    private void SpawnByCircle()
    {               
        for (int i = 0; i < countOfEnemies; i++)
        {
            Vector3 pos;

            if (randomInCircle)
            {
                Vector3 posRandom = Random.insideUnitCircle * radius;
                pos = new Vector3(posRandom.x + x0, center.y, posRandom.y + z0);
            }
            else
            {
                int angle = 360 / countOfEnemies * i;
                pos = ArithmeticMethods.RandomCircle(center, radius, angle);
            }            
            // юниты смотрят в центр окружности
            Quaternion rot = Quaternion.LookRotation(center - pos);
           //Instantiate(enemyData.Prefab, pos, rot);
            ObjectPooler.instance.GetObject(enemyData.PrefabType, pos, rot);
            enemyData.Prefab.GetComponent<EnemyParameters>().SetEnemyData(enemyData);
        }
    }

    private void SpawnByRectangle()
    {
        foreach (EnemyData enemyData in EnemyDatas())
        {
            //вычисляем рандомную позицию внутри прямоугольника
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);
            //Instantiate(enemyData.Prefab, new Vector3(x, enemyData.Prefab.transform.position.y, z), Quaternion.identity);
            ObjectPooler.instance.GetObject(enemyData.PrefabType, new Vector3(x, enemyData.Prefab.transform.position.y, z), Quaternion.identity);
            enemyData.Prefab.GetComponent<EnemyParameters>().SetEnemyData(enemyData);
        }

    }
 
}
