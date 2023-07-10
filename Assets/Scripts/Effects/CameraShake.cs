using UnityEngine;
using System.Collections;

public class CameraShake : Effect
{
    public Camera effectCamera;
    [SerializeField]
    [Range(0.1f,1f)]
    public float magnitud;
    private Vector3 _originalPosition;

    public void OnEnable()
    {
        _originalPosition = effectCamera.transform.position;
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision, ExecutionData executionData = null)
    {
        Shake(executionData.elapsed, executionData.to, magnitud);
    }

    private void Shake(float elapsed, float duration, float magnitude)
    {
        Vector3 orignalPosition = effectCamera.transform.position;

        if (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * (magnitude/100);
            float y = Random.Range(-1f, 1f) * (magnitude /100);

            effectCamera.transform.position = new Vector3(x, y, -10f);
        }
    }

    public override void StopEffect()
    {
        effectCamera.transform.position = _originalPosition;
    }


}

