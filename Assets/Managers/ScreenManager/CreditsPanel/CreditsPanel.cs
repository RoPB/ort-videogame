using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CreditsPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;

    private void Start()
    {
        AttachGameState(GameState.Credits, new List<GameState> { GameState.PlayingCredits });
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

    public void CloseCredits()
    {
        if (GameManager.Instance.gameState == GameState.PlayingCredits)
        {
            GameManager.Instance.ChangeGameState(GameState.PlayingInit);
        }
        else if (GameManager.Instance.gameState == GameState.Credits)
        {
            GameManager.Instance.ChangeGameState(GameState.Init);
        }
    }

}

