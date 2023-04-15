using UnityEngine;
using System.Collections;

public class PlayerLifeManager : MonoBehaviour
{
    [SerializeField]
    [Range(3, 5)]
    public int maxLifes = 3;
    private int _lifeLost = 0;
    public PlayerLifes playerLifes => new PlayerLifes() { maxLifes = maxLifes, currentLifes = maxLifes - _lifeLost };

    public void Init()
    {
        _lifeLost = 0;
    }

    public void PlayerLostLife()
    {
        _lifeLost++;
    }
}

public class PlayerLifes
{
    public int maxLifes;
    public int currentLifes;
}
