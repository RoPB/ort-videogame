using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamagePlayer : TakesDamage
{
    public override void TakeDamage(int damage)
    {
        GameManager.Instance.playerLifeManager.PlayerLostLife();
    }
}
