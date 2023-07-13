using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //NOT needed anymore
    //public CollisionController collisionController;
    //public EnemyMovementController enemyMovementController;

    public int score;
    public ReactionSequencer damageReceivedReactionSequencer;
    public ReactionSequencer killedReactionSequencer;

    [SerializeField]
    [Range(1, 5)]
    public int lifes = 1;
    private int _damageReceived = 0;

    public void OnEnable()
    {
        _scoreIncreased = false;
        _damageReceived = 0;
        if (this.gameObject.GetComponent<TakesDamageEnemy>() == null)
            this.gameObject.AddComponent<TakesDamageEnemy>();
    }

    private EnemyPooler _enemyPooler;
    private bool _scoreIncreased;

    public void SetOriginPool(EnemyPooler enemyPooler)
    {
        this._enemyPooler = enemyPooler;
    }

    public void ReturnToOriginPool()
    {
        this._enemyPooler.ReturnToPool(this.gameObject);
    }

    public void Collided()
    {
        this._enemyPooler.ReturnToPool(this.gameObject);
    }

    public void TookDamage(Collision2D collision, int damage)
    {
        _damageReceived += damage;
        if (lifes - _damageReceived <= 0)
        {
            this.gameObject.layer = LayerMask.NameToLayer("EnemiesKilled");

            if (!_scoreIncreased)
            {
                _scoreIncreased = true;
                GameManager.Instance.scoreManager.AddToScore(score);
            }

            killedReactionSequencer.ReactionSequenceEnded += KilledReactionSequencer_ReactionSequenceEnded;
            killedReactionSequencer.StartReactionSequence(null, collision);
            
        }
        else
        {
            damageReceivedReactionSequencer.StartReactionSequence(null, collision);
        }
    }

    private void KilledReactionSequencer_ReactionSequenceEnded(object sender, bool e)
    {
        killedReactionSequencer.ReactionSequenceEnded -= KilledReactionSequencer_ReactionSequenceEnded;
        ReturnToOriginPool();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("ExternalBounds"))
        {
            this.ReturnToOriginPool();
        }

    }

}
