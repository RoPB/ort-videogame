using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ReactionSequencer : MonoBehaviour
{
    public List<Reaction> reactions;

    private List<Reaction> _reactionsToApply;
    private Collider2D _collider;
    private Reaction _currentRection;
    private bool _gettingReaction;
    private bool _sequenceRunning;

    public void StartReactionSequence(Collider2D collider)
    {
        _reactionsToApply = new List<Reaction>(reactions.ToArray());
        _collider = collider;
        _sequenceRunning = true;
        _gettingReaction = reactions.Count > 0;
    }

    private bool SetCurrentReaction()
    {
        if (_reactionsToApply.Count>0)
        {
            _currentRection = _reactionsToApply[0];
            _reactionsToApply.RemoveAt(0);
            _currentRection.onReactionStopped += Reaction_OnReactionStopped;
            return true;
        }

        return false;
    }

    private void Reaction_OnReactionStopped(object sender,bool stopped)
    {
        _currentRection.onReactionStopped -= Reaction_OnReactionStopped;
        _sequenceRunning = _reactionsToApply.Count>0;
        _gettingReaction = true;
    }


    void FixedUpdate()
    {
        if (_sequenceRunning)
        {
            if (_gettingReaction)
            {
                _gettingReaction = false;
                if(SetCurrentReaction())
                    _currentRection?.React(_collider);
            }
        }
    }

    void OnDisable()
    {
        _sequenceRunning = false;
        _gettingReaction = false;
    }

}

