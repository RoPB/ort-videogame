using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        return 1.5f/_currentLevel;
    }

    public void SpawnEnemy()
    {
        var randomScale = Mathf.Min(Random.Range(0.1f * _currentLevel, 0.2f * _currentLevel), 0.6f);
        var scale = new Vector3(randomScale, randomScale, 0);
        GameManager.Instance.enemyPooler.SpawnPooledEnemy(scale, GetRandomPosition(scale));
    }

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        float randomY = GameManager.Instance.GetRandomYInSceneBounds(scale);
        float maxX = GameManager.Instance.GetSceneMaxX();
        return new Vector3(maxX, randomY, 0);
    }

}
