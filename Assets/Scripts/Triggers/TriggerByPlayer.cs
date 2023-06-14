using UnityEngine;

public class TriggerByPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("TRIGGERED BY PLAYER");
        }
    }
}
