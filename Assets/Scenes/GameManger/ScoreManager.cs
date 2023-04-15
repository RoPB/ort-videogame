using UnityEngine;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
    private long initialTime = 0;
    private long lastTimeScoreEvaluated = 0;
    private float velocityAvgPerEvaluation = 0;
    private long currentScore = 0;

    private void Update()
    {
        if (initialTime != 0)
            setScore();
    }

    public long getCurrentScore()
    {
        return currentScore;
    }

    public void startScore()
    {
        initialTime = DateTime.Now.Ticks;
        velocityAvgPerEvaluation = 1;
        currentScore = 0;
    }

    public void stopScore()
    {
        setScore(true);
    }

    public void resetScore()
    {
        initialTime = 0;
        velocityAvgPerEvaluation = 0;
        currentScore = 0;
    }

    private void setScore(bool forceCalculate = false)
    {
        if (lastTimeScoreEvaluated == 0)
            lastTimeScoreEvaluated = initialTime;

        var timeToEvaluate = DateTime.Now.Ticks;
        var dt = timeToEvaluate - lastTimeScoreEvaluated;

        if (dt > 5000 || forceCalculate)
        {
            //currentScore += (long)(dt * velocityAvgPerEvaluation) / 100000;
            currentScore += (long)(dt * GameManager.Instance.enemiesVelocityMultiplier) / 100000;
            lastTimeScoreEvaluated = timeToEvaluate;
            velocityAvgPerEvaluation = 1;
        }
        else
        {
            //TODO ver esta referencia circular a GameManager
            //velocityAvgPerEvaluation *= Mathf.Clamp(GameManager.Instance.enemiesVelocityMultiplier, 0.1f, 1);
        }
    }

}

