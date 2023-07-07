using UnityEngine;

public class SoundEffect : Effect
{
    public bool loop;

    public AudioSource audioSource;
    private bool _playExecuted;

    private void OnEnable()
    {
        audioSource.loop = loop;
    }

    public override void PlayEffect(Collider2D collider, Collision2D collision)
    {
        if (!_playExecuted&&!audioSource.isPlaying)
        {
            audioSource.enabled = true;
            _playExecuted = true;
            audioSource.Play();
            //Debug.Log("PLAYAUDIO");
        }
        
    }

    public override void StopEffect()
    {
        _playExecuted = false;
        audioSource.enabled = false;
        audioSource.Stop();
        //Debug.Log("StopEffect AUDIO");
    }

}
