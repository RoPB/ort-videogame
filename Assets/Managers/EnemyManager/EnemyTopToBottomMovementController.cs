using System;
using UnityEngine;

public class EnemyTopToBottomMovementController : IEnemyMovementController
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        //_rigidbody.isKinematic ESTE LO GUARDAMOS PRA PODER APLICARLE ALGUNA FUERZA
        //Y QUE NO SE APLIQUE VELOCIDAD ???
        if (_rigidbody.bodyType.Equals(RigidbodyType2D.Dynamic))
            _rigidbody.velocity = new Vector2(0, -1);

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

