using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static UnityEditor.PlayerSettings;

public class PlayerMisionsManager : MonoBehaviour
{
    public List<PlayerMision> playerMisions;

    private int _currentMisionIndex;
    private PlayerMision _currentMision;

    //Right to left
    private float _yMin = 0;
    private float _yMax = 0;
    private float _playerHeight = 0;

    //Top to bottom
    private float _xMin = 0;
    private float _xMax = 0;
    private float _playerWidth = 0;

    public void Init(float yMin, float yMax, float xMin, float xMax, float playerHeight, float playerWidth)
    {
        _currentMisionIndex = -1;
        _currentMision = null;

        _yMin = yMin;
        _yMax = yMax;
        _playerHeight = playerHeight;
        _xMin = xMin;
        _xMax = xMax;
        _playerWidth = playerWidth;
    }

    public void Stop()
    {
        foreach(var pm in playerMisions)
        {
            pm.enemies.Stop();
        }
    }

    internal void TryExecuteMision()
    {
        ExecuteNextMision();

    }

    private void ExecuteNextMision()
    {
        if(_currentMisionIndex < playerMisions.Count - 1)
        {
            if(_currentMision!=null)
                _currentMision.enemies.PauseSpawn();

            _currentMisionIndex++;
            _currentMision = playerMisions[_currentMisionIndex];
            StartCoroutine(ExecuteMision());
        }
        else
        {
            StartCoroutine(InitAll());
        }
        
    }

    private IEnumerator ExecuteMision()
    {
        GameManager.Instance.playerWarnMsg = _currentMision.msg;
        GameManager.Instance.playerWarnMsgDescription = _currentMision.description;

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        SpawnMisionEnemies();
    }

    private void SpawnMisionEnemies()
    {
        _currentMision.enemies.OnSpawnEnded += CurrentMision_OnSpawnEnded;
        _currentMision.enemies.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(), _yMin, _yMax, _xMin, _xMax, _playerWidth, _playerHeight);
    }

    private void CurrentMision_OnSpawnEnded(object sender, float e)
    {
        _currentMision.enemies.OnSpawnEnded -= CurrentMision_OnSpawnEnded;

        ExecuteNextMision();
    }

    private IEnumerator InitAll()
    {
        GameManager.Instance.playerWarnMsg = "Be careful";
        GameManager.Instance.playerWarnMsgDescription = "All them will appear togheter to save the galaxy";

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        GameManager.Instance.InitAll();
    }

    private void PauseSpawn(List<EnemySpawner> currentEnemySpawners)
    {
        foreach (var enemySpawner in currentEnemySpawners)
        {
            enemySpawner.PauseSpawn();
        }
    }

}

