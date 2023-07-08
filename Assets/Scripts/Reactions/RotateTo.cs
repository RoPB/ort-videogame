using UnityEngine;
using System.Collections;

public class RotateTo : Reaction
{
    public Rigidbody2D rigidBodyToRotate;
    public ImageOrientation imageOrientation;
    private Vector2 _lastVelocity;

    public RotateTo() : base("RotateTo")
    {

    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        //Debug.Log("ROTATION TO ");
        var targetPosition = collider.transform.position;
        var newRotation = Helper.getRotationToTarget(imageOrientation, transform, targetPosition);
        rigidBodyToRotate.SetRotation(newRotation);

    }
}

