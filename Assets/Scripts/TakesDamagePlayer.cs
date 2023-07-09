using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamagePlayer : TakesDamage
{
    [SerializeField]
    [Range(0, 5)]
    private float inmortalityPeriod = 1.5f;
    private float lastDamageTime = 0;
    public override void TakeDamage(Collision2D collision, int damage)
    {
        if (Time.time - lastDamageTime < inmortalityPeriod)
            return;
        lastDamageTime = Time.time;
        GameObject.FindObjectOfType<Player>().TookDamage(collision, damage);
    }
}
