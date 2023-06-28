using UnityEngine;
using System.Collections;

public class MoveTo : Reaction
{
    private Vector2 _forceDirection;
    private Rigidbody2D _rigidbody;
    private RigidbodyType2D _prevBodyType;
    private Vector2 _lastVelocity;
    private bool _movedToTriggered;

    public MoveTo() : base("MoveTo")
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
        _movedToTriggered = false;

        _forceDirection = collider.transform.position - _rigidbody.transform.position;
        _forceDirection.Normalize();

    }

    protected override void ExecuteReaction(Collider2D collider, ExecutionData executionData)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(_forceDirection * 1f, ForceMode2D.Impulse);

        //ESTO DEBIERA FUNCIONAR
        //if (!_movedToTriggered)
        //{
        //    _movedToTriggered = true;
        //    _rigidbody.velocity = Vector2.zero;
        //    Vector2 forceDirection = collider.transform.position - _rigidbody.transform.position;
        //    forceDirection.Normalize();
        //    _rigidbody.AddForce(forceDirection * 1f, ForceMode2D.Impulse);
        //    Debug.Log("POSITION" + forceDirection);
        //}


    }

    protected override void OnReactionStopped()
    {
        _rigidbody.velocity = _lastVelocity;
        _rigidbody.bodyType = _prevBodyType;
        _movedToTriggered = false;
        onReactionStopped?.Invoke(this, true);
    }

}

