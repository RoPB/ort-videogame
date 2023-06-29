using UnityEngine;
using System.Collections;

public class RotateToPlayer : Reaction
{
    public ImageOrientation imageOrientation;
    private Player _player;
    private Rigidbody2D _rigidbody;

    public RotateToPlayer() : base("RotateToPlayer")
    {

    }

    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        //Debug.Log("ROTATION TO PLAYER");
        var targetPosition = _player.GetComponent<Rigidbody2D>().transform.position;
        var newRotation = Helper.getRotationToTarget(imageOrientation, transform, targetPosition);
        _rigidbody.SetRotation(newRotation);
    }
}

