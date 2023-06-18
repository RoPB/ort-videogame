using UnityEngine;

public class SoundEffect : Effect
{
    public bool loop;

    private AudioSource _audioSource;
    private bool _playExecuted;

    void Awake()
    {
        _audioSource = gameObject?.GetComponent<AudioSource>();
        _audioSource.loop = loop;
        _audioSource.enabled = true;
    }

    public override void PlayEffect()
    {
        Debug.Log("CALLINGAUDIOPLAYEFFECT");
        if (!_playExecuted&&!_audioSource.isPlaying)
        {
            _playExecuted = true;
            _audioSource.Play();
            Debug.Log("PLAYAUDIO");
        }
        
    }

    public override void StopEffect()
    {
        _playExecuted = false;
        //_audioSource.enabled = false;//NO DESCOMENTAR ESTO
        Debug.Log("StopEffect AUDIO");
    }

}
