using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private bool _spawnerInitiated = false;
    private int _currentLevel = 0;
    private float _dtSum = 0;

    private void Update()
    {
        if (_spawnerInitiated)
        {
            _dtSum += Time.deltaTime;
            if (_dtSum > GetSpawnFrequency())
            {
                _dtSum = 0;
                SpawnEnemy();
            }
        }
    }

    public void LevelChanged(int level)
    {
        _currentLevel = level;
    }

    public void Init(int currentLevel)
    {
        _currentLevel = currentLevel;
        _spawnerInitiated = true;
    }

    public void Stop()
    {
        _spawnerInitiated = false;
    }

    private float GetSpawnFrequency()
    {
        return 1f/_currentLevel;
    }

    public void SpawnEnemy()
    {
        var enemy = GameManager.Instance.enemyPooler.GetPooledEnemy();
        GameManager.Instance.enemyPooler.SpawnPooledEnemy(enemy,GetRandomPosition(enemy.transform.localScale));
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        float randomY = GameManager.Instance.GetRandomYInSceneBounds(scale);
        float maxX = GameManager.Instance.GetSceneMaxX();
        return new Vector3(maxX, randomY, 0);
    }
}
