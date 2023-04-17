using System;
using UnityEngine;

public class LeaderBoardPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;

    private void Start()
    {
        AttachGameState(GameState.End);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }
}

