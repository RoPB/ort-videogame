using UnityEngine;
using System.Collections;

public class AnimatorEffect : Effect
{
    public string animationParam;
    private int _animateHash;
    private bool _playExecuted;
    private Animator _animator;

    private void Awake()
    {
        _animateHash = Animator.StringToHash(animationParam);
    }

    private void OnEnable()
    {
        _playExecuted = false;
        _animator = gameObject.GetComponentInChildren<Animator>(); 
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        if (!_playExecuted)
        {
            _playExecuted = true;
            _animator.SetBool(_animateHash, true);
        }
        

    }

    public override void StopEffect()
    {
        _playExecuted = false;
        _animator.SetBool(_animateHash, false);
    }
}

