using System;
using TMPro;
using UnityEngine;

public class PlayerLifesPanel : BasePanel
{
    public TextMeshProUGUI playerLifesValue;

    private EventHandler<GameState> gameStateChanged;

    // Use this for initialization
    private void Start()
    {
        AttachGameState(GameState.Playing);
        GameManager.Instance.PlayerLifesChanged += GameManager_PlayerLifesChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerLifesChanged -= GameManager_PlayerLifesChanged;
        DettachGameState();
    }

    public void UpdateCounter()
    {
        Debug.Log("playerLifes: " + GameManager.Instance.playerLifes);
        playerLifesValue.text = "X" + GameManager.Instance.playerLifes;
    }

    private void GameManager_PlayerLifesChanged(object sender, int e)
    {
        UpdateCounter();
    }
}

