using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectile : MonoBehaviour
{
    [Range(1, 10)]
    public int damage = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        var takesDamage = other.gameObject.GetComponent<TakesDamage>();
        if (takesDamage != null)
        {
            Debug.Log("Proyectile collided with " + other.gameObject.name);
            takesDamage.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
