using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private string _playerName;

    public string playerName => _playerName;

    private Player _player => GameObject.FindObjectOfType<Player>();

    public float playerHeight  { get { return _player.playerHeight; } }

    public float playerWidth { get { return _player.playerWidth; } }

    public void Init(string playerName)
    {
        _playerName = playerName;
        _player.playerMovementController.Init();
    }
}

