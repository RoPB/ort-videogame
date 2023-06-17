using System;
using UnityEngine;

public abstract class Reaction : MonoBehaviour
{
    public int maxReactions;
    public int animationsCount;

    private Collider2D _collision;
    protected bool _reactionApplying;
    protected int _reactionsCounter;
    protected int _animationCounter;


    public void React(Collider2D collision)
    {
        if (CanApplyReaction())
        {
            OnReactionStart(collision);
            _collision = collision;
            StartReaction();
        }

    }

    protected abstract void OnReactionStart(Collider2D collision);

    protected abstract void ExecuteReaction(Collider2D collision);

    protected void Reset()
    {
        _reactionApplying = false;
        _reactionsCounter = 0;
        _animationCounter = 0;
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
        _reactionApplying = false;
        EndAnimation();
    }

    protected bool IsApplyingReaction()
    {
        return _reactionApplying;
    }

    protected bool CanApplyAnimation()
    {
        return animationsCount - _animationCounter >= 0;
    }

    protected void ApplyAnimation()
    {
        _animationCounter++;
    }

    protected void EndAnimation()
    {
        _animationCounter=animationsCount;
    }

    void FixedUpdate()
    {
        if (IsApplyingReaction())
            if (CanApplyAnimation())
            {
                ApplyAnimation();
                ExecuteReaction(_collision);
            }
            else
                StopReaction();
    }

    void OnDisable()
    {
        Reset();
    }


}

