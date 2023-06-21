using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterAnimator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    [Range(0, 0.5f)]
    float flameFlicker = 0.1f;

    [SerializeField]
    [Range(0, 1)]
    float flameStrengthChange = 0.5f;

    [SerializeField]
    [Range(0, 0.5f)]
    float flameOpacityChange = 0.25f;



    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ToggleFlameIntensity();
        UpdateFlameLength();
    }

    private void ToggleFlameIntensity()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 1 - flameOpacityChange + Mathf.PingPong(Time.time, flameOpacityChange));
    }

    private void UpdateFlameLength()
    {
        var y = Input.GetAxis("Vertical");
        float scaleTransform;
        if (y > 0)
        {
            scaleTransform = Mathf.Lerp(1f, 1 + flameStrengthChange, y);
        }
        else
        {
            scaleTransform = Mathf.Lerp(1f, 1 - flameStrengthChange, -y);
        }
        transform.localScale = new Vector3(1, scaleTransform + Mathf.PingPong(Time.time, flameFlicker), 0);
    }
}
