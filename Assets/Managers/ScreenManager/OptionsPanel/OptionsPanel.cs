using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class OptionsPanel : BasePanel
{
    public Slider volumeSlider;

    private EventHandler<GameState> gameStateChanged;
    

    private void Start()
    {
        volumeSlider.value = GameManager.Instance.GetVolume();
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

    public void ChangeVolumen()
    {
        GameManager.Instance.SetVolume(volumeSlider.value);
    }

}

