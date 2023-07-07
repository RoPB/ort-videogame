using UnityEngine;
using System.Collections;

public class Freeze : Reaction
{
    public Rigidbody2D rigidBodyToFreeze;
    private RigidbodyType2D _prevBodyType;
    private Vector2 _lastVelocity;

    public Freeze() : base("Freeze")
    {

    }


    protected override void OnInitBeforeReaction(Collider2D collider, Collision2D collision)
    {
        _prevBodyType = rigidBodyToFreeze.bodyType;
        _lastVelocity = rigidBodyToFreeze.velocity;
        rigidBodyToFreeze.velocity = Vector2.zero;
        rigidBodyToFreeze.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        //Debug.Log("FREEZE");
    }

    protected override void OnReactionStopped()
    {
        rigidBodyToFreeze.velocity = _lastVelocity;
        rigidBodyToFreeze.bodyType = _prevBodyType;
        base.OnReactionStopped();
    }
}

