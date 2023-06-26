using UnityEngine;
using System.Collections;

public class Freeze : Reaction
{
    private Rigidbody2D _rigidbody;
    private Vector2 _lastVelocity;

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        _lastVelocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        Debug.Log("FREEZE");

    }

    protected override void OnReactionStopped()
    {
        _rigidbody.velocity = _lastVelocity;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        base.OnReactionStopped();
    }
}

