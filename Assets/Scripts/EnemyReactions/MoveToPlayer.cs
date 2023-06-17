using UnityEngine;
using System.Collections;

public class MoveToPlayer : Reaction
{
    private Rigidbody2D _rigidbody;
    private Vector2 _forceDirection;


    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void OnReactionStart(Collider2D collision)
    {
        Vector2 forceDirection = collision.transform.position - transform.position;
        forceDirection.Normalize();
        _forceDirection = forceDirection;
    }

    protected override void ExecuteReaction(Collider2D collision)
    {

        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(_forceDirection * 1f, ForceMode2D.Impulse);

    }
}

