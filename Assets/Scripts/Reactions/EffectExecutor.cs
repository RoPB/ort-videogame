﻿using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class EffectExecutor : Reaction
{
    public string effectType;

    private Effect _effect;

    public EffectExecutor() : base("EffectExecutor")
    {

    }

    void Start()
    {
        var parent = transform.parent.gameObject;
        var type = Type.GetType(effectType);
        _effect = parent.GetComponentInChildren(type,true) as Effect;
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        //Debug.Log("EXECUTING EFFECT "+ $"{effectType}");
        _effect.PlayEffect(collider, collision);
    }


    protected override void OnReactionStopped()
    {
        _effect.StopEffect();
        base.OnReactionStopped();
    }

}

