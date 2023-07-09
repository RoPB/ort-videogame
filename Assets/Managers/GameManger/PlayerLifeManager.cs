using UnityEngine;
using System.Collections;
using System;

public class PlayerLifeManager : MonoBehaviour
{
    public int lifes { get; private set; }

    public event EventHandler<int> PlayerLifesChanged;

    public void Init()
    {
        lifes = 3;
        PlayerLifesChanged?.Invoke(this, lifes);
    }

    public void PlayerLostLife(int damage)
    {
        lifes = lifes - damage < 0 ? 0 : lifes - damage;
        PlayerLifesChanged?.Invoke(this, lifes);
    }

}

