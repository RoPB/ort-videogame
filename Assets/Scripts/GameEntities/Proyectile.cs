using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [Range(1, 10)]
    public int damage;

    private void OnCollisionEnter2D(Collision2D other)
    {
        var takesDamage = other.gameObject.GetComponent<TakesDamage>();
        if (takesDamage != null)
        {
            Debug.Log("Proyectile collided with " + other.gameObject.name);
            takesDamage.TakeDamage(other, damage);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ExternalBounds")
        {
            Destroy(this.gameObject);
        }
    }
}
