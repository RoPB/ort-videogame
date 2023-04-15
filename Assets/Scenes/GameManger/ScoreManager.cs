using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private bool _scoreInitiated = false;
    private int _scoreRefreshSeconds = 1;
    private float _dtSum = 0;
    private float _velocityAvgPerEvaluation = 1;
    private long _currentScore = 0;
    public long currentScore => _currentScore;

    private void Update()
    {
        if(_scoreInitiated)
            SetScore();
    }

    public void Init()
    {
        Reset();
        _scoreInitiated = true;
    }

    public void Stop()
    {
        SetScore(true);
        _scoreInitiated = false;
    }

    private void Reset()
    {
        _dtSum = 0;
        ResetVelocityAvgPerEvaluation();
        _currentScore = 0;
    }

    private void ResetVelocityAvgPerEvaluation()
    {
        _velocityAvgPerEvaluation = 1;
    }

    private void SetScore(bool forceCalculate = false)
    {
        _dtSum += Time.deltaTime;

        if (_dtSum > _scoreRefreshSeconds || forceCalculate)
        {
            _currentScore += (long)(_dtSum * _velocityAvgPerEvaluation);
            _dtSum = 0;
            ResetVelocityAvgPerEvaluation();
        }
        else
        {
            //TODO ver esta referencia circular a GameManager
            _velocityAvgPerEvaluation *= 1;//GameManager.Instance.enemiesVelocityMultiplier;// Mathf.Clamp(GameManager.Instance.enemiesVelocityMultiplier, 0.1f, 10f);
        }
    }

}

