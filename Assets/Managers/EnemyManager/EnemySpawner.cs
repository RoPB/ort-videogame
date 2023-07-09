using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Drawing;

public class EnemySpawner : MonoBehaviour
{
    public bool spawnFromRigth;

    public bool spawnInvisible;
    public bool showInvisiblePath = false;
    [SerializeField]
    [Range(0.1f, 60f)]
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

    [SerializeField]
    [Range(0f, 1f)]
    public float rangeToSpawnReduceFactor;
    public List<EnemyPooler> _enemyPoolers;
    private float _enemyChangeRate;
    private float _spawnFrequenceRate;
    private bool _hasSpawned;
    private bool _spawnPaused;

    [Range(0f, 1f)]
    public float spawnUntilFixedTimeFactor;
    private bool _spawnEnded;
    private float _spawnedForFixedDTExecution;
    public event EventHandler<float> OnSpawnEnded;

    private float _spawnLifeCounter;

    public void Init(int currentLevel, GameDifficulty difficulty, float yMin, float yMax, float xMin, float xMax, float playerHeight, float playerWidth)
    {
        _spawnLifeCounter = 0;
        _spawnedForFixedDTExecution = 0;
        _spawnEnded = false;
        _spawnPaused = false;
        _hasSpawned = false;
        _enemyChangeRate = difficulty == GameDifficulty.Low ? 0.1f : difficulty == GameDifficulty.Medium ? 0.15f : 0.2f;
        _spawnFrequenceRate = difficulty == GameDifficulty.Low ? 1.4f : difficulty == GameDifficulty.Medium ? 1f : 0.8f;
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
            CheckSpawnEnded();

            if(!_spawnPaused)
                _spawnLifeCounter += Time.deltaTime;

            _spawnedForFixedDTExecution += Time.deltaTime;
            _dtSum += Time.deltaTime;

            if (!_spawnPaused && (!_hasSpawned || _dtSum > GetSpawnFrequency()))
            {
                _hasSpawned = true;
                _dtSum = 0;
                SpawnEnemy();
                MoveDynamicYPosition();
                MoveDynamicXPosition();
            }
        }
    }

    private void CheckSpawnEnded()
    {
        if(spawnUntilFixedTimeFactor > 0 && _spawnedForFixedDTExecution * spawnUntilFixedTimeFactor >= 12)
        {
            PauseSpawn();
            OnSpawnEnded?.Invoke(this, _spawnedForFixedDTExecution);
            _spawnedForFixedDTExecution = 0;
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

    public void PauseSpawn()
    {
        _spawnPaused = true;
    }

    public void ResumeSpawn()
    {
        _spawnPaused = false;
    }

    private float GetSpawnFrequency()
    {
        if(spawnUntilFixedTimeFactor==0)
            return Mathf.Max(spawnFrequence * _spawnFrequenceRate / (_spawnLifeCounter/12),2f);
        else//ingore level, this is a spawn for a fixed time
            return spawnFrequence * _spawnFrequenceRate;
    }

    private float GetDynamicYPositionMovementOffset()
    {
        return Mathf.Max((_spawnLifeCounter / 12) * _dynamicYPositionMovementOffset, 0.3f);
    }

    private float GetDynamicXPositionMovementOffset()
    {
        return Mathf.Max((_spawnLifeCounter / 12) * _dynamicXPositionMovementOffset, 0.3f);
    }

    public void SpawnEnemy()
    {

        var indexToSpawn = 0;

        if (spawnUntilFixedTimeFactor == 0) {

            //var n = _enemyChangeRate * (_spawnLifeCounter / 12);
            //indexToSpawn = Mathf.FloorToInt((_enemyPoolers.Count - 1) * Mathf.Clamp(n, 0f, 1f));
            indexToSpawn = UnityEngine.Random.Range(0, _enemyPoolers.Count);

        }
        else//ignore level and randomize between all items
        {
            indexToSpawn = UnityEngine.Random.Range(0, _enemyPoolers.Count);
        }


        var size = _enemyPoolers[indexToSpawn].GetEnemySize();
        var randomPosition = Vector3.zero;

        if (spawnFromRigth)
        {
            randomPosition = GetRandomPositionFromRight(new Vector3(size, size, 0));
            
        }
        else
        {
            randomPosition = GetRandomPositionFromTop(new Vector3(size, size, 0));
        }

        var finalScaleValue = spawnInvisible ? 0f : size;
        var scale = new Vector3(finalScaleValue, finalScaleValue, 0);

        _enemyPoolers[indexToSpawn].SpawnPooledEnemy(scale, randomPosition);

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

        randomY = rangeToSpawnReduceFactor == 0 ? randomY : randomY * rangeToSpawnReduceFactor;

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

        randomX = rangeToSpawnReduceFactor==0 ? randomX : randomX * rangeToSpawnReduceFactor;

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
