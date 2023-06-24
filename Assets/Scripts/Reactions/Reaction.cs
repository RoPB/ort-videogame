using System;
using UnityEditor;
using UnityEngine;

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

    public void React(Collider2D collider)
    {
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

    protected abstract void ExecuteReaction(Collider2D collider, float executionProgress);

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

    private float GetExecutionProgress()
    {
        if (neverEnds)
            return 0f;

        return (float)_executionCounter / executionsCount;
    }

    void FixedUpdate()
    {
        if (IsApplyingReaction())
            if (CanApplyExecution())
            {
                ApplyExecution();
                ExecuteReaction(_collider, GetExecutionProgress());
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

