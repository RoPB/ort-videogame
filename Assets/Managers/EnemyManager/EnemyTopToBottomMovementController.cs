using System;
using UnityEngine;

public class EnemyTopToBottomMovementController : EnemyMovementController
{
    [SerializeField]
    [Range(-1, -0.1f)]
    public float velocity;

    private Rigidbody2D _rigidbody;
    private Quaternion _currentRotation;

    void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        _currentRotation = transform.rotation;
    }

    void OnEnable()
    {
        transform.rotation = _currentRotation;
    }

    void FixedUpdate()
    {
        if (_rigidbody.bodyType.Equals(RigidbodyType2D.Dynamic))
        {
            _rigidbody.velocity = new Vector2(0, velocity);
            RotateEnemy();
        }
            

        if (this.IsOutOfScene())
        {
            var enemy = this.gameObject.GetComponent<Enemy>();
            enemy.ReturnToOriginPool();
        }

    }

    private bool IsOutOfScene()
    {
        return GameManager.Instance.IsLocatedAtTheBottomOfTheScene(this.transform.position, this.transform.localScale);
    }
}

