using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostprocessLayer : Effect
{
    public PostProcessVolume volume;
    private Vignette _vignette;
    private float _dtTime;

    private void OnEnable()
    {
        _dtTime = 0;
        volume.profile.TryGetSettings<Vignette>(out _vignette);
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision, ExecutionData executionData)
    {
        _dtTime += Time.deltaTime;
        if (_vignette)
        {
            var currentIntensity = Mathf.Lerp(0.55f, 0f, Mathf.Clamp01((float)executionData.elapsed / executionData.to));
            // Debug.Log(currentIntensity);
            _vignette.intensity.Override(currentIntensity);
        }
    }

    public override void StopEffect()
    {
        _dtTime = 0;
        if (_vignette)
        {
            _vignette.intensity.Override(0f);
        }
    }
}

