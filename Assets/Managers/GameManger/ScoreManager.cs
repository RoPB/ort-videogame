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
        if(_scoreInitiated)
            SetScore();
    }

    public void Init()
    {
        _currentScore = 0;
        _scoreInitiated = true;
    }

    public void Stop()
    {
        SetScore();
        _scoreInitiated = false;
    }

    private void SetScore()
    {
        _currentScore += Time.deltaTime * GameManager.Instance.enemiesVelocityMultiplier * 10;
    }

}

