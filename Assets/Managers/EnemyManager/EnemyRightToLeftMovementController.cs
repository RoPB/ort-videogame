using System;
using UnityEngine;

public class EnemyRightToLeftMovementController : EnemyMovementController
{
    [SerializeField]
    [Range(-1, -0.1f)]
    public float velocity;

    private Rigidbody2D _rigidbody;
    private Quaternion _currentRotation;
    private bool _outOfScene = false;

    void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        _currentRotation = transform.rotation;
    }

    void OnEnable()
    {
        _outOfScene = false;
        transform.rotation = _currentRotation;
    }

    void FixedUpdate()
    {
        if (_rigidbody.bodyType.Equals(RigidbodyType2D.Dynamic))
        {
            _rigidbody.velocity = new Vector2(velocity, 0);
            RotateEnemy();
        }
    }


}

