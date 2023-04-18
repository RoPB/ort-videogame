using System;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    private string _playerName;

    public string playerName => _playerName;

    public void Init(string playerName)
	{
        _playerName = playerName;
        var player = GameObject.FindObjectOfType<Player>();
        player.playerMovementController.Init();
    }
}

