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

    public void Init(PlayerLifeManager playerLifeManager)
    {
        _playerLifeManager = playerLifeManager;
        BroadcastMessage("InitGunGroup", null, SendMessageOptions.DontRequireReceiver);
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
