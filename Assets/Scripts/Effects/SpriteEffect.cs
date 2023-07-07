using UnityEngine;

public class SpriteEffect : Effect
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = gameObject?.GetComponent<SpriteRenderer>();
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        _spriteRenderer.enabled = true;
    }

    public override void StopEffect()
    {
        _spriteRenderer.enabled = false;
    }
}

