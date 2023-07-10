using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamageEnemy : TakesDamage
{
    public override void TakeDamage(Collision2D collision, int damage)
    {
        this.GetComponent<Enemy>().TookDamage(collision, damage);
    }
}
