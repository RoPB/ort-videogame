using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private bool _scoreInitiated = false;
    private int _scoreRefreshSeconds = 10;
    private float _dtSum = 0;
    private float _velocityAvgPerEvaluation = 0;
    private long _currentScore = 0;
    public long currentScore => _currentScore;

    private void Update()
    {
        if(_scoreInitiated)
            setScore();
    }

    public void init()
    {
        reset();
        _scoreInitiated = true;
    }

    public void stop()
    {
        setScore(true);
        _scoreInitiated = false;
    }

    private void reset()
    {
        _dtSum = 0;
        _velocityAvgPerEvaluation = 0;
        _currentScore = 0;
    }

    private void setScore(bool forceCalculate = false)
    {
        _dtSum += Time.deltaTime;

        if (_dtSum > _scoreRefreshSeconds || forceCalculate)
        {
            _currentScore += (long)(_dtSum * _velocityAvgPerEvaluation);
            Debug.Log(_dtSum + "INGRESA" +_velocityAvgPerEvaluation);
            _dtSum = 0;
            _velocityAvgPerEvaluation = 1;
        }
        else
        {
            //TODO ver esta referencia circular a GameManager
            _velocityAvgPerEvaluation *= 1;// Mathf.Clamp(GameManager.Instance.enemiesVelocityMultiplier, 0.1f, 10);
        }
    }

}

