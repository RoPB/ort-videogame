﻿using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;

public class ReactionSequencer : MonoBehaviour
{
    public bool executeInLoop;
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
        while (_reactionsToApply.Count > 0 && _reactionsToApply[0].isSequencedButNotAwaitable)
        {
            _reactionsToApply[0].React(_collider);
            _reactionsToApply.RemoveAt(0);
        }

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
        if (_reactionsToApply.Count==0 && executeInLoop)
        {
            _reactionsToApply = new List<Reaction>(reactions.ToArray());
        }
        else
        {
            _sequenceRunning = _reactionsToApply.Count > 0;
        }
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
                    _currentRection?.React(_collider, executeInLoop);
            }
        }
    }

    void OnDisable()
    {
        _sequenceRunning = false;
        _gettingReaction = false;
    }

}

