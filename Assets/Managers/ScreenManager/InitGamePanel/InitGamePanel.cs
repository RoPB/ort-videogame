using System;
using UnityEngine;
using TMPro;

public class InitGamePanel : BasePanel
{
    public TMP_InputField playerNameInput;

    private EventHandler<GameState> gameStateChanged;

    private void Start()
    {
        AttachGameState(GameState.Init);
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

    public void ShowInitPanel()
    {
        GameManager.Instance.ShowPanel(GameState.Init);
    }

    public void ShowOptions()
    {
        GameManager.Instance.ShowPanel(GameState.Options);
    }

    public void ShowCredits()
    {
        GameManager.Instance.ShowPanel(GameState.Credits);
    }

    public void OpenLeaderBoard()
    {
        GameManager.Instance.ShowPanel(GameState.LeaderBoard);
    }
}

