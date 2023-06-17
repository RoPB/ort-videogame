using UnityEngine;
using System.Collections;

public class MoveToPlayer : IReaction
{
    public int movementCounts;
    private int _movementCountsSet;
    private bool _movingToPlayer;
    private Rigidbody2D _rigidbody;
    private Vector2 _forceDirection;
    private int _movePlayerCounter;

    void OnEnable()
    {
        _movingToPlayer = false;
        _movePlayerCounter = 0;
        _movementCountsSet = movementCounts;
    }

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
            if (_movePlayerCounter == 0)
                _movingToPlayer = false;
        }
        
    }

    public override void React(Collider2D collision)
    {
        if(!_movingToPlayer && movementCounts > 0)
        {
            Debug.Log("COLISIONA");
            _movingToPlayer = true;
            --movementCounts;
            _movePlayerCounter = 10;
            Vector2 forceDirection = collision.transform.position - transform.position;
            forceDirection.Normalize();
            _forceDirection = forceDirection;
        }
        
    }

    void OnDisable()
    {
        _movingToPlayer = false;
        _movePlayerCounter = 0;
        movementCounts = _movementCountsSet;
    }
}

