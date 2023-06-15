using UnityEngine;
using System.Collections;

public class MoveToPlayer : IReaction
{
    private Rigidbody2D _rigidbody;
    private Vector2 _forceDirection;
    private int _movePlayerCounter = 0;

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (_movePlayerCounter>0)
        {
            --_movePlayerCounter;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(_forceDirection * 1f, ForceMode2D.Impulse);
        }
        
    }


    public override void React(Collider2D collision)
    {
        _movePlayerCounter = 10;
        Vector2 forceDirection = collision.transform.position - transform.position;
        forceDirection.Normalize();
        _forceDirection = forceDirection;
        
    }
}

