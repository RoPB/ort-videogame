using System;
using UnityEngine;

public class InitGamePanel : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance.gameState == GameState.Init)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

}

