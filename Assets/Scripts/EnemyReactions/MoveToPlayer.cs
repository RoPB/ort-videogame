using UnityEngine;
using System.Collections;

public class MoveToPlayer : IReaction
{
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }


    public override void React(Collider2D collision)
    {
        Debug.Log("COLISIONA");
        //_rigidbody.velocity = new Vector2(0, 0);
        //_rigidbody.bodyType = RigidbodyType2D.Static;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        //_rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.velocity = Vector2.up * 1;
        //_rigidbody.AddForce(Vector2.up * 1000f, ForceMode2D.Impulse);
        //_rigidbody.AddForce(Vector2.up*10f, ForceMode2D.Impulse);
    }
}

