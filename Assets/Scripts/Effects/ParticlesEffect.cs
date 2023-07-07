using UnityEngine;
using System.Collections;
using static UnityEngine.ParticleSystem;

public class ParticlesEffect : Effect
{
    [SerializeField]
    [Range(1, 10)]
    public int particlesScaleFactor;
    public GameObject particles;
    private bool _playExecuted;

    private void OnEnable()
    {
        _playExecuted = false;
    }


    public override void PlayEffect(Collider2D collider,Collision2D collision)
    {
        if (!_playExecuted)
        {
            Debug.Log("EJECUTA PATICULAS EFFECTS");
            _playExecuted = true;
            for (int i = 0; i < collision.contactCount; i++)
            {
                var contact = collision.GetContact(i);
                var impact = GameObject.Instantiate(particles);
                impact.transform.position = contact.point;
                if (contact.collider != null)
                    impact.transform.localScale = contact.collider.transform.localScale * particlesScaleFactor;
                DestroyEffect(impact);
            }
        }

    }

    private async void DestroyEffect(GameObject gameObject)
    {
        await System.Threading.Tasks.Task.Delay(500);
        GameObject.Destroy(gameObject);
    }

    public override void StopEffect()
    {
        _playExecuted = false;
    }
}

