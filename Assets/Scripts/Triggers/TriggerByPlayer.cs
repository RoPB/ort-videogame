using System.Collections.Generic;
using UnityEngine;

public class TriggerByPlayer : MonoBehaviour
{
    public List<Reaction> reactions;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            foreach(var reaction in reactions)
                reaction.React(collision);
        }
    }
}
