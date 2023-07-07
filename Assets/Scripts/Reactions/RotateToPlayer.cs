using UnityEngine;
using System.Collections;

public class RotateToPlayer : Reaction
{
    public Rigidbody2D rigidBodyToRotate;
    public ImageOrientation imageOrientation;
    private Player _player;
    

    public RotateToPlayer() : base("RotateToPlayer")
    {

    }

    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        //Debug.Log("ROTATION TO PLAYER");
        var targetPosition = _player.GetComponent<Rigidbody2D>().transform.position;
        var newRotation = Helper.getRotationToTarget(imageOrientation, transform, targetPosition);
        rigidBodyToRotate.SetRotation(newRotation);
    }
}

