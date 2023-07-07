using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamageShield : TakesDamage
{
    private double createdTime;
    private double duration = 5;
    void OnEnable()
    {
        createdTime = Time.time;
    }
    private void Update()
    {
        if (Time.time - createdTime > duration)
        {
            gameObject.SetActive(false);
        }
    }
    public override void TakeDamage(int damage)
    {
    }
}
