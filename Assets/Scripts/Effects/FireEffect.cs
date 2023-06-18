using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : Effect
{

    public override void PlayEffect()
    {
        gameObject?.SetActive(true);
    }

    public override void StopEffect()
    {
        gameObject?.SetActive(false);
    }
}
