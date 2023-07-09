using System;
using TMPro;
using UnityEngine;

public class PlayerLifesPanel : BasePanel
{
    public TextMeshProUGUI playerLifesValue;

    private EventHandler<GameState> gameStateChanged;

    private PlayerLifeManager _playerLifeManager;

    // Use this for initialization
    private void Start()
    {
        AttachGameState(GameState.Playing);
        _playerLifeManager = GameManager.Instance.playerLifeManager;
        _playerLifeManager.PlayerLifesChanged += GameManager_PlayerLifesChanged;
    }

    private void OnDestroy()
    {
        _playerLifeManager.PlayerLifesChanged -= GameManager_PlayerLifesChanged;
        DettachGameState();
    }

    public void UpdateCounter()
    {
        Debug.Log("playerLifes: " + _playerLifeManager.lifes);
        playerLifesValue.text = "X" + _playerLifeManager.lifes;
    }

    private void GameManager_PlayerLifesChanged(object sender, int e)
    {
        UpdateCounter();
    }
}

