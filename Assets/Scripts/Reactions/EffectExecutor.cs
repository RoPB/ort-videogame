using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class EffectExecutor : Reaction
{
    public Effect effect;

    public EffectExecutor() : base("EffectExecutor")
    {

    }


    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        //Debug.Log("EXECUTING EFFECT "+ $"{effectType}");
        effect.PlayEffect(collider, collision);
    }


    protected override void OnReactionStopped()
    {
        effect.StopEffect();
        base.OnReactionStopped();
    }

}

