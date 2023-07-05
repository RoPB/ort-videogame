using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamageEnemy : TakesDamage
{
    private float inmortalityPeriod = 1f;
    private float lastDamageTime = 0;
    public override void TakeDamage(int damage)
    {
        if (Time.time - lastDamageTime < inmortalityPeriod)
            return;
        lastDamageTime = Time.time;
        this.GetComponent<Enemy>().TookDamage(damage);
    }
}
