using System;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private delegate void GameStateChanged();
    private EventHandler<GameState> gameStateChangedHandler;

    protected void AttachGameState(GameState state)
    {
        GameStateChanged gameStateChanged = () => gameObject.SetActive(GameManager.Instance.gameState == state);
        gameStateChangedHandler = (s, e) => gameStateChanged();
        GameManager.Instance.GameStateChanged += gameStateChangedHandler;
        gameStateChanged();
    }

    protected void DettachGameState()
    {
        GameManager.Instance.GameStateChanged -= gameStateChangedHandler;
    }
}

