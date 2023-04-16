using System;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
	public void Init()
	{
        var player = GameObject.FindObjectOfType<Player>();
        player.playerMovementController.Init();
    }

    public void Stop()
    {
        var player = GameObject.FindObjectOfType<Player>();
        player.playerMovementController.Stop();
    }
}

