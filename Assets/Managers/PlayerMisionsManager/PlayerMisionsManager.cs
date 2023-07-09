using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static UnityEditor.PlayerSettings;

public class PlayerMisionsManager : MonoBehaviour
{
    public List<EnemySpawner> asteroidsSpawner;
    private int _currentAsteroidsIndex;
    private EnemySpawner _currentAsteroidsSpawner;

    public List<EnemySpawner> ufosSpawner;
    private int _currentUfosSpawnerIndex;
    private EnemySpawner _currentUfoSpawner;

    public List<EnemySpawner> humansEnemiesSpawner;
    private int _currentHumansEnemiesSpawnerIndex;
    private EnemySpawner _currentHumanEnemySpawner;

    public List<EnemySpawner> missileHeadsSpawner;
    private int _currentMissileHeadSpawnerIndex;
    private EnemySpawner _currentMissileHeadSpawner;

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
        _currentAsteroidsIndex = -1;
        _currentUfosSpawnerIndex = -1;
        _currentHumansEnemiesSpawnerIndex = -1;
        _currentMissileHeadSpawnerIndex = -1;
        _yMin = yMin;
        _yMax = yMax;
        _playerHeight = playerHeight;
        _xMin = xMin;
        _xMax = xMax;
        _playerWidth = playerWidth;
    }

    public void Stop()
    {
        foreach(var a in asteroidsSpawner)
        {
            a.Stop();
        }

        foreach (var u in ufosSpawner)
        {
            u.Stop();
        }

        foreach (var h in humansEnemiesSpawner)
        {
            h.Stop();
        }

        foreach (var m in missileHeadsSpawner)
        {
            m.Stop();
        }
    }

    internal void TryExecuteMision()
    {
        ExecuteNextMision();

    }

    private void ExecuteNextMision()
    {
        if (_currentUfosSpawnerIndex < this.ufosSpawner.Count - 1)
        {
            _currentUfosSpawnerIndex++;
            StartCoroutine(UfosAhead());
        }
        else if (_currentAsteroidsIndex < this.asteroidsSpawner.Count - 1)
        {
            _currentAsteroidsIndex++;
            StartCoroutine(AsteroidsAhead());
        }
        else if (_currentHumansEnemiesSpawnerIndex < this.humansEnemiesSpawner.Count - 1)
        {
            _currentHumansEnemiesSpawnerIndex++;
            StartCoroutine(HumanEnemiesAhead());
        }
        else if (_currentMissileHeadSpawnerIndex < this.missileHeadsSpawner.Count - 1)
        {
            _currentMissileHeadSpawnerIndex++;
            StartCoroutine(MissilHeadsAhead());
        }
        else if (ufosSpawner.Count == _currentUfosSpawnerIndex + 1 &&
            asteroidsSpawner.Count == _currentAsteroidsIndex + 1 &&
            humansEnemiesSpawner.Count == _currentHumansEnemiesSpawnerIndex + 1 &&
            missileHeadsSpawner.Count == _currentMissileHeadSpawnerIndex + 1)
        {
            StartCoroutine(InitAll());
        }
    }

    private IEnumerator InitAll()
    {
        GameManager.Instance.playerWarnMsg = "Be careful";
        GameManager.Instance.playerWarnMsgDescription = "Dont know where u go";

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


    #region ufos

    private IEnumerator UfosAhead()
    {
        GameManager.Instance.playerWarnMsg = "UFOs";
        GameManager.Instance.playerWarnMsgDescription = "Many of them ahead";

        _currentUfoSpawner = ufosSpawner[_currentUfosSpawnerIndex];

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        SpawnUfos();
    }

    private void SpawnUfos()
    {
        _currentUfoSpawner.OnSpawnEnded += CurrentUFOsSpawner_OnSpawnEnded;
        _currentUfoSpawner.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(), _yMin, _yMax, _xMin, _xMax, _playerWidth, _playerHeight);
    }

    private void CurrentUFOsSpawner_OnSpawnEnded(object sender, float e)
    {
        _currentUfoSpawner.OnSpawnEnded -= CurrentUFOsSpawner_OnSpawnEnded;

        ExecuteNextMision();
    }

    #endregion

    #region asteroids

    private IEnumerator AsteroidsAhead()
    {
        GameManager.Instance.playerWarnMsg = "Asteroids Ahead";
        GameManager.Instance.playerWarnMsgDescription = "Hold your ship";

        _currentAsteroidsSpawner = asteroidsSpawner[_currentAsteroidsIndex];

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        SpawnAsteroids();
    }

    private void SpawnAsteroids()
    {
        _currentAsteroidsSpawner.OnSpawnEnded += CurrentAsteroidsSpawner_OnSpawnEnded;
        _currentAsteroidsSpawner.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(),_yMin,_yMax,_xMin,_xMax,_playerWidth,_playerHeight);
    }

    private void CurrentAsteroidsSpawner_OnSpawnEnded(object sender, float e)
    {
        _currentAsteroidsSpawner.OnSpawnEnded -= CurrentAsteroidsSpawner_OnSpawnEnded;

        ExecuteNextMision();
    }

    #endregion

    #region humansEnemies

    private IEnumerator HumanEnemiesAhead()
    {
        GameManager.Instance.playerWarnMsg = "Humans";
        GameManager.Instance.playerWarnMsgDescription = "Many of them ahead";

        _currentHumanEnemySpawner = humansEnemiesSpawner[_currentHumansEnemiesSpawnerIndex];

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        SpawnHumanEnemies();
    }

    private void SpawnHumanEnemies()
    {
        _currentHumanEnemySpawner.OnSpawnEnded += CurrentHumanEnemySpawner_OnSpawnEnded;
        _currentHumanEnemySpawner.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(), _yMin, _yMax, _xMin, _xMax, _playerWidth, _playerHeight);
    }

    private void CurrentHumanEnemySpawner_OnSpawnEnded(object sender, float e)
    {
        _currentHumanEnemySpawner.OnSpawnEnded -= CurrentHumanEnemySpawner_OnSpawnEnded;

        ExecuteNextMision();
    }

    #endregion

    #region missilHeads

    private IEnumerator MissilHeadsAhead()
    {
        GameManager.Instance.playerWarnMsg = "Missil Heads";
        GameManager.Instance.playerWarnMsgDescription = "Many of them ahead";

        _currentMissileHeadSpawner = missileHeadsSpawner[_currentMissileHeadSpawnerIndex];

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.ChangeGameState(GameState.Playing);

        SpawnMissileHeads();
    }

    private void SpawnMissileHeads()
    {
        _currentMissileHeadSpawner.OnSpawnEnded += CurrentMissileHeadSpawner_OnSpawnEnded; ;
        _currentMissileHeadSpawner.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(), _yMin, _yMax, _xMin, _xMax, _playerWidth, _playerHeight);
    }

    private void CurrentMissileHeadSpawner_OnSpawnEnded(object sender, float e)
    {
        _currentMissileHeadSpawner.OnSpawnEnded -= CurrentMissileHeadSpawner_OnSpawnEnded;

        ExecuteNextMision();
    }

    #endregion
}

