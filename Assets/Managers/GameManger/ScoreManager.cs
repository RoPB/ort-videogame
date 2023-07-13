using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private bool _scoreInitiated = false;

    private float _currentScore = 0;
    public double currentScore => Math.Floor(_currentScore);

    private void Update()
    {
        if (_scoreInitiated)
            SetScore();
    }

    private void SetScore()
    {
        _currentScore += Time.deltaTime * 2;
    }

    public void Init()
    {
        _currentScore = 0;
        _scoreInitiated = true;
    }

    public void Stop()
    {
        _currentScore = 0;
        _scoreInitiated = false;
    }

    public void AddToScore(int value)
    {
        if(_scoreInitiated)
            _currentScore += value;
    }

  

}

