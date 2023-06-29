using UnityEngine;
using System.Collections;

public class Freeze : Reaction
{
    private Rigidbody2D _rigidbody;
    private RigidbodyType2D _prevBodyType;
    private Vector2 _lastVelocity;

    public Freeze() : base("Freeze")
    {

    }

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        _prevBodyType = _rigidbody.bodyType;
        _lastVelocity = _rigidbody.velocity;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        //Debug.Log("FREEZE");
    }

    protected override void OnReactionStopped()
    {
        _rigidbody.velocity = _lastVelocity;
        _rigidbody.bodyType = _prevBodyType;
        base.OnReactionStopped();
    }
}

