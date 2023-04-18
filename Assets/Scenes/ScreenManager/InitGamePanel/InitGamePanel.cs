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
        var playerName = playerNameInput.text ?? "ANONYMOUS";
        GameManager.Instance.StartGame(playerName);
    }

    public void OpenLeaderBoard()
    {
        GameManager.Instance.ShowLeaderboard();
    }
}

