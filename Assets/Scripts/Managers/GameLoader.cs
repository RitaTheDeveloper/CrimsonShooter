using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField]
    GameObject uiManager;

    [SerializeField]
    GameObject gameController;

    private void Awake()
    {
        if (UIManager.instance == null)
        {
            Instantiate(uiManager);
        }

        if (GameController.instance == null)
        {
            Instantiate(gameController);
        }
    }
}
