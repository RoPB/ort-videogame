using UnityEngine;

public class SpriteEffect : Effect
{
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = gameObject?.GetComponent<SpriteRenderer>();
    }

    public override void PlayEffect()
    {
        _spriteRenderer.enabled = true;

    }

    public override void StopEffect()
    {
        _spriteRenderer.enabled = false;
    }
}

