using UnityEngine;
using System.Collections;

public class MoveTo : Reaction
{
    private Rigidbody2D _rigidbody;
    private Vector2 _forceDirection;


    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    protected override void OnInitBeforeReaction(Collider2D collider)
    {
        Vector2 forceDirection = collider.transform.position - transform.position;
        forceDirection.Normalize();
        _forceDirection = forceDirection;
    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        Debug.Log("MOVING TO ");
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(_forceDirection * 1f, ForceMode2D.Impulse);

    }
}

