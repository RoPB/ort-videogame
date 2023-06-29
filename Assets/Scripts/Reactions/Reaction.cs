using System;
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

    private string _reactionName;

    public Reaction(string reactionName)
    {
        _reactionName = reactionName;
    }

    public EventHandler<bool> onReactionStopped;

    private bool _readyToReact;
    private Collider2D _collider;
    protected bool _reactionApplying;
    protected int _reactionsCounter;
    protected int _executionCounter;

    public void React(Collider2D collider, bool force = false)
    {
        if (force)
        {
            Initialize();
        }

        _collider = collider;
        _readyToReact = true;
    }

    protected virtual void OnInitBeforeReaction(Collider2D collider){}

    protected virtual void OnReactionStopped() {
        onReactionStopped?.Invoke(this,true);
    }

    protected abstract void ExecuteReaction(Collider2D collider, ExecutionData executionData);

    protected void Initialize()
    {
        _readyToReact = false;
        _reactionApplying = false;
        _collider = null;
        _reactionsCounter = 0;
        _executionCounter = 0;
    }

    protected bool CanApplyReaction ()
    {
        return maxReactions - _reactionsCounter > 0;
    }

    protected void StartReaction()
    {
        _executionCounter = 0;
        _reactionsCounter++;
        _reactionApplying = true;
    }

    protected void EndReaction()
    {
        _reactionApplying = false;
        _collider = null;
        _readyToReact = false;
        OnReactionStopped();
    }

    protected void StopReaction()
    {
        if (_reactionApplying)
        {
            EndReaction();
        }

    }

    protected bool IsApplyingReaction()
    {
        return _reactionApplying;
    }

    protected bool CanApplyExecution()
    {
        return neverEnds || executionsCount - _executionCounter > 0;
    }

    protected void ApplyExecution()
    {
        _executionCounter++;
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
        if (_disabled)
            return;


        if (_readyToReact)
        {
            if (IsApplyingReaction())
            {
                if (CanApplyExecution())
                {
                    ApplyExecution();
                    //if (_reactionName == "EffectExecutor" || _reactionName == "MoveTo")
                    //    UnityEngine.Debug.Log(_reactionName + " " + GetExecutionData().elapsed);
                    ExecuteReaction(_collider, GetExecutionData());
                }
                else
                    EndReaction();
            }
            else if (CanApplyReaction())
            {
                OnInitBeforeReaction(_collider);
                StartReaction();
            }
        }
        
    }

    private bool _disabled;

    private void OnEnable()
    {
        _disabled = false;
    }

    void OnDisable()
    {
        _disabled = true;
        //if (_reactionName == "EffectExecutor" || _reactionName == "MoveTo")
        //    UnityEngine.Debug.Log(_reactionName + " DISABLED" + DateTime.Now.Ticks);
        StopReaction();
        Initialize();
        //UnityEngine.Debug.Log("FINALZA" + _reactionName);
    }

}

