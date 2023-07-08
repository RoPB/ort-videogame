using UnityEngine;
using System.Collections;

public class MoveToSide : Reaction
{
    public Rigidbody2D rigidBodyToMoveSide;
    [SerializeField]
    [Range(-1, 1f)]
    public float velocity;
    public bool randomize;
    private Vector2 _lastVelocity;
    private Vector2 _velocityToSet;

    public MoveToSide() : base("MoveToSide")
    {

    }

    protected override void OnInitBeforeReaction(Collider2D collider, Collision2D collision)
    {
        _velocityToSet = _lastVelocity = rigidBodyToMoveSide.velocity;
        _velocityToSet += new Vector2(velocity, 0);
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        if (!randomize)
            rigidBodyToMoveSide.velocity = _velocityToSet;
        else
            rigidBodyToMoveSide.velocity = _velocityToSet * Random.Range(0, 1);
    }

    protected override void OnReactionStopped()
    {
        rigidBodyToMoveSide.velocity = _lastVelocity;
        onReactionStopped?.Invoke(this, true);
    }
}
