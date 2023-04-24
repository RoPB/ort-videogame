using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public bool showInvisiblePath = false;
    private int _currentLevel = 0;
    private float _dtSum = 0;
    private const float _dynamicYPositionMovementOffset = 0.05f;
    private float _yMin = 0;
    private float _yMax = 0;
    private float _dynamicYPosition = 0;
    private bool _dynamicYPositionCheckAgainstMin;
    private float _restrictedHeight = 0;
    private float _restrictedYMax => _dynamicYPosition - (_restrictedHeight / 2);
    private float _restrictedYMin => _dynamicYPosition + (_restrictedHeight / 2);
    

    public void Init(int currentLevel, float yMin, float yMax, float playerHeight)
    {
        _currentLevel = currentLevel;

        _yMin = yMin;
        _yMax = yMax;
        _dynamicYPosition = yMax;
        _dynamicYPositionCheckAgainstMin = true;
        _restrictedHeight = playerHeight;
    }

    private void FixedUpdate()
    {
        _dtSum += Time.deltaTime;
        if (_dtSum > GetSpawnFrequency())
        {
            _dtSum = 0;
            SpawnEnemy();
            MoveDynamicYPosition();
        }

    }

    private void MoveDynamicYPosition()
    {

        //This logic will make _dynamicYPosition moves from top to bottom and from bottom to top
        if (_dynamicYPositionCheckAgainstMin)
        {
            if (_dynamicYPosition - GetDynamicYPositionMovementOffset() < _yMin)
                _dynamicYPositionCheckAgainstMin = false;

            if (_dynamicYPositionCheckAgainstMin)
                _dynamicYPosition -= GetDynamicYPositionMovementOffset();
        }
        if (!_dynamicYPositionCheckAgainstMin)
        {
            if (_dynamicYPosition + GetDynamicYPositionMovementOffset() > _yMax)
                _dynamicYPositionCheckAgainstMin = true;

            if (!_dynamicYPositionCheckAgainstMin)
                _dynamicYPosition += GetDynamicYPositionMovementOffset();
        }
    }

    public void LevelChanged(int level)
    {
        _currentLevel = level;
    }

    private float GetSpawnFrequency()
    {
        return Mathf.Max(1.5f/_currentLevel,0.2f);
    }

    private float GetDynamicYPositionMovementOffset()
    {
        return Mathf.Max(_currentLevel * _dynamicYPositionMovementOffset, 0.3f);
    }

    public void SpawnEnemy()
    {
        var finalScale = Mathf.Min(Mathf.Log10(_currentLevel + 2.5f) + (1 / (_currentLevel + 2.5f)) - 0.7f + Random.Range(-0.05f, 0.05f),0.35f);
        var scale = new Vector3(finalScale, finalScale, 0);

        GameManager.Instance.enemyPooler.SpawnPooledEnemy(scale, GetRandomPosition(scale));
    }


    private bool _spawnAboveDynamicYPosition = false;

    private Vector3 GetRandomPosition(Vector3 scale)
    {
        var maxX = GameManager.Instance.GetSceneMaxX() + scale.x / 2;
        var randomY = 0f;

        var canSpawnAbove = _yMax - _restrictedYMax > scale.y;
        var canSpawnBelow = _restrictedYMin - _yMin > scale.y;

        var spawnAbove = canSpawnAbove && canSpawnBelow ? _spawnAboveDynamicYPosition : canSpawnAbove ? true : false;

        _spawnAboveDynamicYPosition = !_spawnAboveDynamicYPosition;

        if (spawnAbove)
        {
            var maxY = _yMax - scale.y / 2;
            var minY = _restrictedYMax + scale.y / 2;
            randomY = UnityEngine.Random.Range(minY, maxY);
            //Debug.Log("SPAWN ABOVE");
        }
        else
        {
            var maxY = _restrictedYMin - scale.y / 2;
            var minY = _yMin + scale.y / 2;
            randomY = UnityEngine.Random.Range(minY, maxY);
            //Debug.Log("SPAWN BELOW");
        }


        return new Vector3(maxX, randomY, 0);
    }

    public void OnDrawGizmos()
    {
        if (showInvisiblePath)
        {
            Gizmos.DrawSphere(new Vector3(0, _dynamicYPosition), _restrictedHeight);
        }
    }
}
