using UnityEngine;

public class TriggerByPlayer : MonoBehaviour
{
    public IReaction reaction;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            reaction.React(collision);
        }
    }
}
