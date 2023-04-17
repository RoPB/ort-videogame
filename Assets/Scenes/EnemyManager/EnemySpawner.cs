using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    private int _currentLevel = 0;
    private float _dtSum = 0;

    public void Init(int currentLevel)
    {
        _currentLevel = currentLevel;
    }

    private void FixedUpdate()
    {
        _dtSum += Time.deltaTime;
        if (_dtSum > GetSpawnFrequency())
        {
            _dtSum = 0;
            SpawnEnemy();
        }
    }

    public void LevelChanged(int level)
    {
        _currentLevel = level;
    }

    private float GetSpawnFrequency()
    {
        return Mathf.Max(1.5f/_currentLevel,0.25f);
    }

    public void SpawnEnemy()
    {
        var finalScale = Mathf.Min(Mathf.Log10(_currentLevel + 2.5f) + (1 / (_currentLevel + 2.5f)) - 0.7f + Random.Range(-0.05f, 0.05f),0.35f);
        var scale = new Vector3(finalScale, finalScale, 0);

        GameManager.Instance.enemyPooler.SpawnPooledEnemy(scale, GetRandomPosition(scale));
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        float randomY = GameManager.Instance.GetRandomYInSceneBounds(scale);
        float maxX = GameManager.Instance.GetSceneMaxX();
        return new Vector3(maxX+ scale.x/2, randomY, 0);
    }

}
