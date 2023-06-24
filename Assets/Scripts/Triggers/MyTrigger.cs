using System;
using System.Collections.Generic;
using UnityEngine;

public class MyTrigger : MonoBehaviour
{
	private string _tag;

	public MyTrigger(string tag)
	{
		_tag = tag;
    }

    public List<Reaction> reactions;
    public ReactionSequencer reactionSequencer;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals(_tag))
        {
            reactionSequencer?.StartReactionSequence(collider);

            if (reactions != null)
                foreach (var reaction in reactions)
                {
                    reaction.React(collider);
                }
        }
    }
}

