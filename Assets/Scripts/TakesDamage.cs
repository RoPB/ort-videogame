using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour
{
    public void TakeDamage(int damage)
    {
        Debug.Log("Damage taken: " + damage);
        Destroy(this.gameObject);
    }
}
