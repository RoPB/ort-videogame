using System;
using UnityEngine;
using TMPro;

public class OptionsPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;

    private void Start()
    {
        AttachGameState(GameState.Options);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

}

