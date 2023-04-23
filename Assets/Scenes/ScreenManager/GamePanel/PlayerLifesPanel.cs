using System;
using TMPro;
using UnityEngine;

public class PlayerLifesPanel : BasePanel
{
    public TextMeshProUGUI playerLifesLabel;
    public TextMeshProUGUI playerLifesValue;

    private EventHandler<GameState> gameStateChanged;

    // Use this for initialization
    private void Start()
    {
        playerLifesLabel.text = "Lifes";
        AttachGameState(GameState.Playing);
        GameManager.Instance.PlayerLifesChanged += GameManager_PlayerLifesChanged;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerLifesChanged -= GameManager_PlayerLifesChanged;
        DettachGameState();
    }

    void UpdateCounter()
    {
        playerLifesValue.text = "X" + GameManager.Instance.playerLifes.currentLifes;
    }

    private void GameManager_PlayerLifesChanged(object sender, PlayerLifes e)
    {
        UpdateCounter();
    }
}

