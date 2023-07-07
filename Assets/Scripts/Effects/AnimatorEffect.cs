using UnityEngine;
using System.Collections;

public class AnimatorEffect : Effect
{
    public Animator animator;
    public string animationParam;
    private int _animateHash;
    private bool _playExecuted;

    private void Awake()
    {
        _animateHash = Animator.StringToHash(animationParam);
    }


    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        if (!_playExecuted)
        {
            _playExecuted = true;
            animator.SetBool(_animateHash, true);
        }
        

    }

    public override void StopEffect()
    {
        _playExecuted = false;
        animator.SetBool(_animateHash, false);
    }
}

