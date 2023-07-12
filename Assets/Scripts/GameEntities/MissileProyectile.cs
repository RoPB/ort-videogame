using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProyectile : Proyectile
{
    public GameObject explosionPrefab;
    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody2D.velocity = new Vector2(Mathf.PingPong(Time.time * 4, 1) - 0.5f, _rigidbody2D.velocity.y);
    }
}
