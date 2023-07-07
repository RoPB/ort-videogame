using UnityEngine;

public class SpriteEffect : Effect
{
    public SpriteRenderer spriteRenderer;


    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        spriteRenderer.enabled = true;
    }

    public override void StopEffect()
    {
        spriteRenderer.enabled = false;
    }
}

