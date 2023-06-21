using UnityEngine;
using System.Collections;

public class RotateToPlayer : Reaction
{
    public ImageOrientation imageOrientation;
    private Player _player;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void ExecuteReaction(Collider2D collider, float executionProgress)
    {
        Debug.Log("ROTATION TO PLAYER");
        var targetPosition = _player.GetComponent<Rigidbody2D>().transform.position;
        var newRotation = Helper.getRotationToTarget(imageOrientation, transform, targetPosition);
        _rigidbody.SetRotation(newRotation);

    }
}

