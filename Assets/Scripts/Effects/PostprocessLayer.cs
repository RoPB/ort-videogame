using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostprocessLayer : Effect
{
    public PostProcessVolume volume;
    private Vignette _vignette;

    public void OnEnable()
    {
        volume.profile.TryGetSettings<Vignette>(out _vignette);
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        if (_vignette)
        {
            volume.enabled = true;
            Debug.Log("VA A PONE VIGNNETE");
            _vignette.enabled.Override(true);
        }
    }

    public override void StopEffect()
    {
        if (_vignette)
        {
            volume.enabled = false;
            _vignette.enabled.Override(false);
        }
    }
}

