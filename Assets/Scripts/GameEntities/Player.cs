using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerHeight => this.transform.localScale.x;

    public float playerWidth => this.transform.localScale.y;

    public ReactionSequencer damageReceivedReactionSequencer;
    public ReactionSequencer killedReactionSequencer;

    private PlayerLifeManager _playerLifeManager;

    private Color _originalColor;

    private void Start()
    {
        _originalColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
    }

    public void Init(PlayerLifeManager playerLifeManager)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().color = _originalColor;
        _playerLifeManager = playerLifeManager;
    }

    public void Collided()
    {
        // var currentColor = gameObject.GetComponentInChildren<SpriteRenderer>().color;
        // var alpha = (float)playerLifes.currentLifes / (float)playerLifes.maxLifes;
        // var newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
        // if (alpha == 0)
        //     newColor = new Color(255, 0, 0);
        // gameObject.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }


    public void TookDamage(Collision2D collision, int damage)
    {
        var damageReceived = _playerLifeManager.lifes - damage;
        if (_playerLifeManager.lifes - damageReceived <= 0)
        {
            killedReactionSequencer.ReactionSequenceEnded += KilledReactionSequencer_ReactionSequenceEnded;
            killedReactionSequencer.StartReactionSequence(null, collision);

        }
        else
        {
            damageReceivedReactionSequencer.StartReactionSequence(null, collision);
        }

        _playerLifeManager.PlayerLostLife(damage);
        if (_playerLifeManager.lifes <= 0)
            _ = GameManager.Instance.EndGameAsync();
    }

    private void KilledReactionSequencer_ReactionSequenceEnded(object sender, bool e)
    {
        
    }

}
