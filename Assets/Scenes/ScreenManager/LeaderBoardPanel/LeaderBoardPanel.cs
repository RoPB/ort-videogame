using System;
using UnityEngine;

public class LeaderBoardPanel : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Instance.gameState == GameState.End)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}

