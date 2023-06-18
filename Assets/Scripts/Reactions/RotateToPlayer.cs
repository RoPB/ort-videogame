using UnityEngine;
using System.Collections;

public class RotateToPlayer : Reaction
{
    private Rigidbody2D _rigidbody;
    private Vector2 _forceDirection;
    private Vector2 _lastVelocity;

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        Vector2 forceDirection = collider.transform.position - transform.position;
        forceDirection.Normalize();
        _forceDirection = forceDirection;
        _lastVelocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void ExecuteReaction(Collider2D collider)
    {
        Vector3 targetPosition = collider.attachedRigidbody.position;

        Quaternion targetRotation = Quaternion.LookRotation(_rigidbody.transform.position, targetPosition);

        float rotationSpeed = 200f; // Adjust as needed

        var newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the new rotation to the transform
        _rigidbody.SetRotation(newRotation);

    }

    protected override void OnReactionStopped()
    {
        _rigidbody.velocity = _lastVelocity;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        base.OnReactionStopped();
    }
}

