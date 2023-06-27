﻿using UnityEngine;

public class SoundEffect : Effect
{
    public bool loop;

    private AudioSource _audioSource;
    private bool _playExecuted;

    void Awake()
    {
        _audioSource = gameObject?.GetComponent<AudioSource>();
        _audioSource.loop = loop;

    }

    public override void PlayEffect()
    {
        if (!_playExecuted&&!_audioSource.isPlaying)
        {
            _audioSource.enabled = true;
            _playExecuted = true;
            _audioSource.Play();
            //Debug.Log("PLAYAUDIO");
        }
        
    }

    public override void StopEffect()
    {
        _playExecuted = false;
        _audioSource.enabled = false;
        _audioSource.Stop();
        //Debug.Log("StopEffect AUDIO");
    }

}
