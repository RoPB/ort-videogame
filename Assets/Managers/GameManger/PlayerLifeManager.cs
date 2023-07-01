using UnityEngine;
using System.Collections;
using System;

public class PlayerLifeManager : MonoBehaviour
{

    public int lifes = 3;


    public void Init()
    {
        lifes = 3;
    }

    public void PlayerLostLife()
    {
        lifes--;
    }

}

