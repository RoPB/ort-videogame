using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnInvisible;
    public bool showInvisiblePath = false;
    [SerializeField]
    [Range(2f, 10f)]
    public float spawnFrequence;
    private bool _initialized = false;
    private int _currentLevel = 0;
    private float _dtSum = 0;
    private const float _dynamicYPositionMovementOffset = 0.05f;
    private const float _dynamicXPositionMovementOffset = 0.02f;

    //Right to left
    private float _yMin = 0;
    private float _yMax = 0;
    private float _dynamicYPosition = 0;
    private bool _dynamicYPositionCheckAgainstMin;
    private float _restrictedHeight = 0;
    private float _restrictedYMax => _dynamicYPosition - (_restrictedHeight / 2);
    private float _restrictedYMin => _dynamicYPosition + (_restrictedHeight / 2);

    //Top to bottom
    private float _xMin = 0;
    private float _xMax = 0;
    private float _dynamicXPosition = 0;
    private bool _dynamicXPositionCheckAgainstMin;
    private float _restrictedWidth = 0;
    private float _restrictedXMax => _dynamicXPosition - (_restrictedWidth / 2);
    private float _restrictedXMin => _dynamicXPosition + (_restrictedWidth / 2);

    public List<EnemyPooler> _enemyPoolers;


    public void Init(int currentLevel, float yMin, float yMax, float xMin, float xMax, float playerHeight, float playerWidth)
    {
        _initialized = true;
        _currentLevel = currentLevel;

        _yMin = yMin;
        _yMax = yMax;
        _dynamicYPosition = yMin;
        _dynamicYPositionCheckAgainstMin = true;
        _restrictedHeight = playerHeight;

        _xMin = xMin;
        _xMax = xMax;
        _dynamicXPosition = xMin;
        _dynamicXPositionCheckAgainstMin = true;
        _restrictedWidth = playerWidth;
    }

    public void Stop()
    {
        _initialized = false;
        foreach(var pooler in _enemyPoolers)
        {
            pooler.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (_initialized)
        {
            _dtSum += Time.deltaTime;
            if (_dtSum > GetSpawnFrequency())
            {
                _dtSum = 0;
                SpawnEnemy();
                MoveDynamicYPosition();
                MoveDynamicXPosition();
            }
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

    private void MoveDynamicXPosition()
    {

        //This logic will make _dynamicXPosition moves from left to right and from right to left
        if (_dynamicXPositionCheckAgainstMin)
        {
            if (_dynamicXPosition - GetDynamicXPositionMovementOffset() < _xMin)
                _dynamicXPositionCheckAgainstMin = false;

            if (_dynamicXPositionCheckAgainstMin)
                _dynamicXPosition -= GetDynamicXPositionMovementOffset();
        }
        if (!_dynamicXPositionCheckAgainstMin)
        {
            if (_dynamicXPosition + GetDynamicXPositionMovementOffset() > _xMax)
                _dynamicXPositionCheckAgainstMin = true;

            if (!_dynamicXPositionCheckAgainstMin)
                _dynamicXPosition += GetDynamicXPositionMovementOffset();
        }

    }

    public void LevelChanged(int level)
    {
        _currentLevel = level;
    }

    private float GetSpawnFrequency()
    {
        //return Mathf.Max(1.5f/_currentLevel,0.2f);//TODO VOLVER ESTO
        return spawnFrequence;
    }

    private float GetDynamicYPositionMovementOffset()
    {
        return Mathf.Max(_currentLevel * _dynamicYPositionMovementOffset, 0.3f);
    }

    private float GetDynamicXPositionMovementOffset()
    {
        return Mathf.Max(_currentLevel * _dynamicXPositionMovementOffset, 0.3f);
    }

    public void SpawnEnemy()
    {
        //var finalScale = Mathf.Min(Mathf.Log10(_currentLevel + 2.5f) + (1 / (_currentLevel + 2.5f)) - 0.7f + Random.Range(-0.05f, 0.05f),0.35f);
        var randomPosition = GetRandomPositionFromTop(new Vector3(0.1f, 0.1f, 0));
        var finalScaleValue = spawnInvisible ? 0f : 0.1f;
        var scale = new Vector3(finalScaleValue, finalScaleValue, 0);

        _enemyPoolers?.First()?.SpawnPooledEnemy(scale, randomPosition);
    }

    private bool _spawnAboveDynamicYPosition = false;
    private bool _spawnRightDynamicXPosition = false;

    private Vector3 GetRandomPositionFromRight(Vector3 scale)
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

    private Vector3 GetRandomPositionFromTop(Vector3 scale)
    {
        var maxY = GameManager.Instance.GetSceneMaxY() + scale.y / 2;
        var randomX = 0f;

        var canSpawnRight = _xMax - _restrictedXMax > scale.x;
        var canSpawnLeft = _restrictedXMin - _xMin > scale.x;

        var spawnRight= canSpawnRight && canSpawnLeft ? _spawnRightDynamicXPosition : canSpawnRight ? true : false;

        _spawnRightDynamicXPosition = !_spawnRightDynamicXPosition;

        if (spawnRight)
        {
            var maxX = _xMax - scale.x / 2;
            var minX = _restrictedXMax + scale.x / 2;
            randomX = UnityEngine.Random.Range(minX, maxX);
            //Debug.Log("SPAWN RIGHT");
        }
        else
        {
            var maxX = _restrictedXMin - scale.x / 2;
            var minX = _xMin + scale.x / 2;
            randomX = UnityEngine.Random.Range(minX, maxX);
            //Debug.Log("SPAWN LEFT");
        }


        return new Vector3(randomX,maxY, 0);
    }

    public void OnDrawGizmos()
    {
        if (showInvisiblePath)
        {
            Gizmos.DrawSphere(new Vector3(0, _dynamicYPosition), _restrictedHeight);
            Gizmos.DrawSphere(new Vector3(_dynamicXPosition, 0), _restrictedWidth);
        }
    }
}
