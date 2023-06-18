using System;
using UnityEngine;
using System.Collections.Generic;

public class ReactionSequencer : MonoBehaviour
{
    public List<Reaction> reactions;

    private Collider2D _collider;
    private Reaction _currentRection { get { try { return reactions[_currentReactionIndex]; } catch (Exception ex) { return null; } } }
    private int _currentReactionIndex;
    private bool _gettingReaction;
    private bool _sequenceRunning;

    public void StartReactionSequence(Collider2D collider)
    {
        _collider = collider;
        _sequenceRunning = true;
        _gettingReaction = true;
    }

    private void SetCurrentReaction()
    {
        _currentReactionIndex++;
        if(_currentRection!=null)
            _currentRection.onReactionStopped += Reaction_OnReactionStopped;
    }

    private void Reaction_OnReactionStopped(object sender,bool stopped)
    {
        _gettingReaction = true;
        _currentRection.onReactionStopped -= Reaction_OnReactionStopped;
        _sequenceRunning = _currentReactionIndex < reactions.Count;
        
    }

    void FixedUpdate()
    {
        if (_sequenceRunning)
        {
            if (_gettingReaction)
            {
                _gettingReaction = false;
                SetCurrentReaction();
                _currentRection?.React(_collider);
            }
        }
    }

    void OnDisable()
    {
        _sequenceRunning = false;
        _gettingReaction = false;
        _currentReactionIndex = -1;
        
    }

}

