using UnityEngine;

public class SoundEffect : Effect
{
    public bool loop;

    private AudioSource _audioSource;
    private bool _playExecuted;

    void Start()
    {
        _audioSource = gameObject?.GetComponent<AudioSource>();
    }

    public override void PlayEffect()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.enabled = true;
            _audioSource.Play();
            Debug.Log("PLAYAUDIO");
        }
        
    }

    public override void StopEffect()
    {
        _audioSource.enabled = false;
    }

}
