using System;
using UnityEngine;
using TMPro;

public class CreditsPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;

    private void Start()
    {
        AttachGameState(GameState.Credits);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }


}

