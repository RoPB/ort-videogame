using UnityEngine;
using System.Collections;

public class RotateTo : Reaction
{
    public ImageOrientation imageOrientation;
    public bool rotateInPlace;
    private Rigidbody2D _rigidbody;
    private Vector2 _lastVelocity;

    public RotateTo() : base("RotateTo")
    {

    }

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        //Debug.Log("ROTATION TO ");
        var targetPosition = collider.transform.position;
        var newRotation = Helper.getRotationToTarget(imageOrientation, transform, targetPosition);
        _rigidbody.SetRotation(newRotation);

    }
}

