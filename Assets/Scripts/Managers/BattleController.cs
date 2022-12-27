using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    SpawnLevelData spawnLevelData;
    public SpawnLevelData GetSpawnLevelData
    {
        get { return spawnLevelData; }
    }
    public GameObject prefabEnemy;
    float cdWave, timer;
    bool IsStart;
    List<Wave> waves;
    GameObject player;
    PlayerHealth playerHealth;
    List<EnemyData> enemyDatas;     // список всех врагов, которые будут созданы за уровень
    int enemyKilledCounter;               // счетчик убитых врагов
    public int EnemyKilledCounter
    {
        get { return enemyKilledCounter; }
    }

    private void Awake()
    {
        // узнаем у GameController какой левел загрузить, то есть узнать нужный spawnLevelData
        spawnLevelData = GameController.instance.GetSpawnLvlData();
        // создаем список всех волн
        waves = new List<Wave>();
        for (int i = 0; i < spawnLevelData.SpawnWaveDatas.Count; i++)
        {
            // для каждой волны зададим параметр переменной Bool = false. еще ни одна волна не создана
            Wave wave = new Wave(spawnLevelData.SpawnWaveDatas[i], false);
            waves.Add(wave);
        }

        enemyDatas = new List<EnemyData>();
        foreach (SpawnWaveData waveData in spawnLevelData.SpawnWaveDatas)
        {
            enemyDatas.AddRange(waveData.EnemyDatas());
        }
        Debug.Log("всего врагов будет " + enemyDatas.Count);
              
        timer = 0f;       
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void FixedUpdate()
    {
        SpawnLevel();
        WinCheck();
    }

    public void SpawnLevel()
    {
        if (playerHealth.CurrentHealth <= 0)
        {
            GameController.instance.PlayerIsDead = true;
            GameController.instance.GameOver();
            return;
        }

        timer += Time.deltaTime;
        for (int i = 0; i < waves.Count; i++)
        {
            // если таймер пришел и волна не была создана до этого
            if (timer >= waves[i].GetSpawnWaveData.CdWave && !waves[i].ISCreated)
            {
                // то делаем проверку, если это первая волня, то просто ждем таймера и создаем
                if (i == 0)
                {
                    Debug.Log("спавню 0" + waves[0].GetSpawnWaveData.EnemyDatas().Count);
                    waves[0].GetSpawnWaveData.SpawnWave();
                    waves[0].ISCreated = true;
                    timer = 0f;
                }
                // если это не первая волна, то смотрим была ли создана до этого волна, если создана - то запускаем следующую через нужный кд
                else if (i > 0 && waves[i - 1].ISCreated)
                {
                    Debug.Log("спавню " + waves[i].GetSpawnWaveData.EnemyDatas().Count);
                    waves[i].GetSpawnWaveData.SpawnWave();
                    waves[i].ISCreated = true;
                    timer = 0f;
                }
                
            }          
        }
        
    }

    // класс для того, чтобы можно было создавать списки с одиноковыми spawnWavesData и отслеживать их спавн (bool isCreated)
    class Wave
    {
        bool isCreated;
        public bool ISCreated
        {
            get { return isCreated; }
            set { isCreated = isCreated = value; }
        }
        SpawnWaveData spawnWaveData;
        public SpawnWaveData GetSpawnWaveData
        {
            get { return spawnWaveData; }
            set { spawnWaveData = value; }
        }

        public Wave (SpawnWaveData spawnWaveData, bool isCreated)
        {
            this.spawnWaveData = spawnWaveData;
            this.isCreated = isCreated;
        }
    }

    private bool DidAllWavesComeOut()
    {
        // возвращаем true, если все волны были заспавнены
        int someVal = 0;
        foreach (Wave wave in waves)
        {
            if (!wave.ISCreated)
                someVal++;
        }

        return (someVal > 0) ? false : true;
    }

    private bool AreThereStillEnemiesOnTheMap()
    {
        // возвращаем true, если есть хотя бы один враг на карте
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0) return true;
        else return false;

    }

    public void WinCheck()
    {
        // если все волны заспавнились, то проверяем есть ли еще враги на карте, если нет, то вызываем метот "победа" из GameController
        if (DidAllWavesComeOut())
        {
           // Debug.Log("Все волны заспавнились");
            if (!AreThereStillEnemiesOnTheMap())
            {
                //Debug.Log("врагов нет");
                GameController.instance.Win();
            }
           // else
               // Debug.Log("враги еще есть"); 
        }
    }

}
