using System.Collections.Generic;
using UnityEngine;

public class TriggerByPlayer : MonoBehaviour
{
    public List<Reaction> reactions;
    public ReactionSequencer reactionSequencer;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            reactionSequencer?.StartReactionSequence(collider);

            if(reactions!=null)
                foreach (var reaction in reactions)
                {
                    reaction.React(collider);
                }
        }
    }
}
