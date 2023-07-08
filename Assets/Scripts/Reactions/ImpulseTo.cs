using UnityEngine;
using System.Collections;

public class ImpulseTo : Reaction
{
    public Rigidbody2D rigidBodyToMove;
    private Vector2 _forceDirection;
    private RigidbodyType2D _prevBodyType;
    private Vector2 _lastVelocity;
    private bool _movedToTriggered;

    public ImpulseTo() : base("ImpulseTo")
    {

    }

    protected override void OnInitBeforeReaction(Collider2D collider, Collision2D collision)
    {
        _prevBodyType = rigidBodyToMove.bodyType;
        _lastVelocity = rigidBodyToMove.velocity;
        _movedToTriggered = false;

        _forceDirection = collider.transform.position - rigidBodyToMove.transform.position;
        _forceDirection.Normalize();

    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        rigidBodyToMove.velocity = Vector3.zero;
        rigidBodyToMove.AddForce(_forceDirection * 1f, ForceMode2D.Impulse);

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
        rigidBodyToMove.velocity = _lastVelocity;
        rigidBodyToMove.bodyType = _prevBodyType;
        _movedToTriggered = false;
        onReactionStopped?.Invoke(this, true);
    }

}

