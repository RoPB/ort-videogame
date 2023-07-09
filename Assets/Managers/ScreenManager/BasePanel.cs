using System;
using UnityEngine;
using System.Collections.Generic;

public class BasePanel : MonoBehaviour
{
    public List<GameObject> itemsToToggleVisibility;

    private delegate void GameStateChanged();
    private EventHandler<GameState> gameStateChangedHandler;
    private GameState? _principalState;

    private void OnEnable()
    {
        if (GameManager.Instance != null && _principalState !=null && itemsToToggleVisibility != null && itemsToToggleVisibility.Count > 0)
        {
            var gameState = GameManager.Instance.gameState;
            foreach (var item in itemsToToggleVisibility)
            {
                var disableComponent = item.GetComponent<Disable>();
                if(item!=null)
                    disableComponent.OnDisabled(gameState == _principalState);
            }
        }
    }

    protected void AttachGameState(GameState state, List<GameState> otherStates=null)
    {
        _principalState = state;
        GameStateChanged gameStateChanged = () => gameObject.SetActive(GameManager.Instance.gameState == state || (otherStates!=null && otherStates.Contains(GameManager.Instance.gameState)));
        gameStateChangedHandler = (s, e) => gameStateChanged();
        GameManager.Instance.GameStateChanged += gameStateChangedHandler;
        gameStateChanged();
    }

    protected void DettachGameState()
    {
        GameManager.Instance.GameStateChanged -= gameStateChangedHandler;
    }
}

