using System;
using UnityEngine;

public class LeaderBoardPanel : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("QUEDA" + GameManager.Instance);
        if (GameManager.Instance.gameState == GameState.End)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}

