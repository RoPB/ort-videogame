using UnityEngine;
using System.Collections;
using System;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField]
    [Range(3, 5)]
    public int maxLifes = 3;
    private int _lifeLost = 0;
    public PlayerLifes playerLifes => new PlayerLifes() { maxLifes = maxLifes, currentLifes = maxLifes - _lifeLost };

    public event EventHandler<PlayerLifes> PlayerLifesChanged;

    public void Init()
    {
        _lifeLost = 0;
    }

    public void PlayerLostLife()
    {
        _lifeLost++;
        PlayerLifesChanged?.Invoke(this,playerLifes);
    }
}

public class PlayerLifes
{
    public int maxLifes;
    public int currentLifes;
}
