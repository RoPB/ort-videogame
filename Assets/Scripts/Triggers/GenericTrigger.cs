using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericTrigger : MonoBehaviour
{
    public List<Reaction> reactions;
    public ReactionSequencer reactionSequencer;

    protected void Execute(Collider2D collider)
    {
        reactionSequencer?.StartReactionSequence(collider,null);

        if (reactions != null)
            foreach (var reaction in reactions)
            {
                reaction.React(collider,null);
            }
    }
}

