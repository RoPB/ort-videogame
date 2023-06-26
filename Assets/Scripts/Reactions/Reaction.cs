﻿using System;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class ExecutionData
{
    public float progress { get; set; }
    public int elapsed { get; set; }
    public int to { get; set; }
}

public abstract class Reaction : MonoBehaviour
{
    public int maxReactions;
    public int executionsCount;
    public bool isSequencedButNotAwaitable;
    public bool neverEnds;

    public EventHandler<bool> onReactionStopped;

    private Collider2D _collider;
    protected bool _reactionApplying;
    protected int _reactionsCounter;
    protected int _executionCounter;

    public void React(Collider2D collider, bool force = false)
    {
        if (force)
        {
            Reset();
        }

        if (CanApplyReaction())
        {
            OnInitBeforeReaction(collider);
            _collider = collider;
            StartReaction();
        }

    }

    protected virtual void OnInitBeforeReaction(Collider2D collider){}

    protected virtual void OnReactionStopped() {
        onReactionStopped?.Invoke(this,true);
    }

    protected abstract void ExecuteReaction(Collider2D collider, ExecutionData executionData);

    protected void Reset()
    {
        _reactionApplying = false;
        _reactionsCounter = 0;
        _executionCounter = 0;
    }

    protected bool CanApplyReaction ()
    {
        return maxReactions - _reactionsCounter >= 0;
    }

    protected void StartReaction()
    {
        _reactionApplying = true;
    }

    protected void StopReaction()
    {
        if (_reactionApplying)
        {
            _reactionApplying = false;
            EndExecution();
            OnReactionStopped();
        }
       
    }

    protected bool IsApplyingReaction()
    {
        return _reactionApplying;
    }

    protected bool CanApplyExecution()
    {
        return neverEnds || executionsCount - _executionCounter >= 0;
    }

    protected void ApplyExecution()
    {
        _executionCounter++;
    }

    protected void EndExecution()
    {
        _executionCounter=executionsCount;
    }

    private ExecutionData GetExecutionData()
    {
        return new ExecutionData()
        {
            progress = neverEnds ? 0 : (float)_executionCounter / executionsCount,
            elapsed = _executionCounter,
            to = executionsCount
        };
    }

    void FixedUpdate()
    {
        if (IsApplyingReaction())
            if (CanApplyExecution())
            {
                ApplyExecution();
                ExecuteReaction(_collider, GetExecutionData());
            }
            else
                StopReaction();
    }

    void OnDisable()
    {
        StopReaction();
        Reset();
    }


}

