using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InitGamePanel : BasePanel
{
    public TMP_InputField playerNameInput;

    private void Start()
    {
        AttachGameState(GameState.Init, new List<GameState>() { GameState.PlayingInit});
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

    public void PlayGame()
    {
        var playerName = Guid.NewGuid().ToString();//playerNameInput.text ?? "ANONYMOUS";
        GameManager.Instance.StartGame(playerName);
    }

    public void ShowOptions()
    {
        if (GameManager.Instance.gameState == GameState.PlayingInit)
        {
            GameManager.Instance.ChangeGameState(GameState.PlayingOptions);
        }
        else if (GameManager.Instance.gameState == GameState.Init)
        {
            GameManager.Instance.ChangeGameState(GameState.Options);
        }
        
    }

    public void ShowCredits()
    {

        if (GameManager.Instance.gameState == GameState.PlayingInit)
        {
            GameManager.Instance.ChangeGameState(GameState.PlayingCredits);
        }
        else if (GameManager.Instance.gameState == GameState.Init)
        {
            GameManager.Instance.ChangeGameState(GameState.Credits);
        }
       
    }

    public void OpenLeaderBoard()
    {
        GameManager.Instance.ChangeGameState(GameState.LeaderBoard);
    }
}

