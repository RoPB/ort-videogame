using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float seed;

    public PickupType pickupType = PickupType.Health;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        seed = Random.Range(0, 1000);
    }

    void Update()
    {
        rb2d.velocity = new Vector2(Mathf.PerlinNoise(Time.time, seed) - 0.5f, -0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ExternalBounds")
        {
            Destroy(this.gameObject);
        }
    }
}

public enum PickupType
{
    Health,
    Shield,
    ExtraWeapon,
    AttackSpeed,

    MissileWeapon,
}