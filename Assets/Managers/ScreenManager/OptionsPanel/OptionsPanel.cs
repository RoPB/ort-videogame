using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;

public class OptionsPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;
    

    private void Start()
    {
        AttachGameState(GameState.Options, new List<GameState> { GameState.PlayingOptions });
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

    public void CloseOptions()
    {
        if(GameManager.Instance.gameState == GameState.PlayingOptions)
        {
            GameManager.Instance.ChangeGameState(GameState.PlayingInit);
        }
        else if(GameManager.Instance.gameState == GameState.Options)
        {
            GameManager.Instance.ChangeGameState(GameState.Init);
        }
    }

}

