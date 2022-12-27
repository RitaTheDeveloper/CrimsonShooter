using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public SpawnLevelData[] spawnLevelDatas;
    int level;
    public void SetLevel(int lvl)
    {
        Debug.Log(level);
        level = lvl;
    }
    public SpawnLevelData GetSpawnLvlData()
    {
        return spawnLevelDatas[level];
    }

    bool playerIsDead;
    public bool PlayerIsDead
    {
        get { return playerIsDead; }
        set { playerIsDead = value; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        playerIsDead = false;
    }

    public void GameOver()
    {
        UIManager.instance.GameOver();
    }

    public void Win()
    {
        UIManager.instance.Win();
    }
}
