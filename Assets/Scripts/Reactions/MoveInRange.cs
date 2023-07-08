using UnityEngine;
using System.Collections;

public class MoveInRange : Reaction
{
    public Rigidbody2D rigidBodyToMoveSide;
    [SerializeField]
    [Range(-1, 1f)]
    public float velocity;
    private Vector2 _lastVelocity;
    private Vector2 _velocityToSet;
    private int _seed;

    public MoveInRange() : base("MoveInRange")
    {

    }

    protected override void OnInitBeforeReaction(Collider2D collider, Collision2D collision)
    {
        _seed = Random.Range(0, 1000);
        _velocityToSet = _lastVelocity = rigidBodyToMoveSide.velocity;
        _velocityToSet += new Vector2(velocity, 0);
    }

    protected override void ExecuteReaction(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        rigidBodyToMoveSide.velocity = new Vector2(_velocityToSet.x+Mathf.PerlinNoise(Time.time, _seed), _velocityToSet.y + Mathf.PerlinNoise(Time.time, _seed));
    }

    protected override void OnReactionStopped()
    {
        rigidBodyToMoveSide.velocity = _lastVelocity;
        onReactionStopped?.Invoke(this, true);
    }
}
