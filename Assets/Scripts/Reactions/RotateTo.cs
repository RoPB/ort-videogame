using UnityEngine;
using System.Collections;

public class RotateTo : Reaction
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

    protected override void ExecuteReaction(Collider2D collider, float executionProgress)
    {
        Debug.Log("ROTATION TO ");
        var targetPosition = collider.attachedRigidbody.position;
        var newRotation = Helper.rotateToTarget(targetPosition, transform);
        _rigidbody.SetRotation(newRotation);

    }

    protected override void OnReactionStopped()
    {
        _rigidbody.velocity = _lastVelocity;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        base.OnReactionStopped();
    }
}

