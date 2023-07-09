using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class AsteroidsPanel : BasePanel
{
    
    private void Start()
    {
        AttachGameState(GameState.PlayingAsteroids);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

}

