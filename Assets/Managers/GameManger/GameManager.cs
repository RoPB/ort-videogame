using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private SceneBounds _sceneBounds;

    private GameState _gameState;
    public GameState gameState => _gameState;

    public event EventHandler<GameState> GameStateChanged;

    public float enemiesVelocity = 1.0f;
    private float _enemiesVelocityMultiplier = 1;
    public float enemiesVelocityMultiplier => _enemiesVelocityMultiplier;

    public ScoreManager scoreManager;
    public double currentScore => scoreManager.currentScore;

    public LevelManager levelManager;
    public int currentLevel => levelManager.currentLevel;
    public float levelProgress => levelManager.levelProgress;

    public PlayerManager playerManager;

    public LeaderBoardManager leaderBoardManager;

    public PlayerLifeManager playerLifeManager;

    public PlayerMisionsManager playerMisionsManager;

    //NOT needed anymore for
    //public CollisionManager collisionManager;

    //NOT needed anymore for
    //public EnemyPooler enemyPooler;

    private bool _spawnMissionsEnded;
    private bool _spawningPrincipal;
    private float _principalSpawnerDt;
    private int _spawnerToIndex;
    public List<EnemySpawner> enemySpawners;

    public GameDifficulty initialDifficulty;
    private GameDifficulty _difficulty;

    public string playerWarnMsg { get; set; }
    public string playerWarnMsgDescription { get; set; }

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            this._sceneBounds = new SceneBounds()
            {
                bottomRightCorner = this.SceneBottomRightCorner(),
                topLeftCorner = this.SceneTopLeftCorner()
            };
            Time.timeScale = 0;
            this._gameState = GameState.Init;
            this._difficulty = initialDifficulty;
            SetVolume(0.5f);
            Instance = this;
        }
    }

    public float GetVolume()
    {
        return AudioListener.volume;
    }
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    private GameState? _previusState;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameState == GameState.Playing)
            {
                _previusState = _gameState;
                ChangeGameState(GameState.PlayingInit);
            }
            else if (_previusState != null && _gameState == GameState.PlayingInit
                    || _gameState == GameState.PlayingOptions
                        || _gameState == GameState.PlayingCredits || _gameState == GameState.LeaderBoard)
            {
                ChangeGameState((GameState)_previusState);
                _previusState = null;
            }
        }

        if (!_spawnMissionsEnded)
        {
            if (_spawningPrincipal)
            {
                _principalSpawnerDt += Time.deltaTime;
            }

            if (_principalSpawnerDt > 20)//TODO MODIFICAR ESTE LOOP a 40
            {
                _principalSpawnerDt = 0;
                _spawningPrincipal = false;
                PausePrincipalEnemies();
                playerMisionsManager.TryExecuteMision();
            }
        }
       
    }

    public void StartGame(string playerName)
    {
        _spawnMissionsEnded = false;
        _spawningPrincipal = false;
        _principalSpawnerDt = 0;
        _spawnerToIndex = -1;
        playerWarnMsg = string.Empty;
        playerWarnMsgDescription = string.Empty;
        playerLifeManager.Init();
        levelManager.Init();
        GameObject.FindObjectOfType<Player>().Init(playerLifeManager);
        levelManager.LevelChanged += LevelManager_LevelChanged;
        scoreManager.Init();
        playerManager.Init(playerName);
        _gameState = GameState.Playing;
        playerMisionsManager.Init(_sceneBounds.bottom, _sceneBounds.top,
        _sceneBounds.left, _sceneBounds.right, playerManager.playerHeight, playerManager.playerWidth);
        playerMisionsManager.TryExecuteMision();
        GameStateChanged?.Invoke(this, _gameState);
        Time.timeScale = 1;
    }

    public void InitPrincipalSpawn(bool ended)
    {
        if (!ended)
        {
            _spawningPrincipal = true;

            if(_spawnerToIndex<enemySpawners.Count)
                _spawnerToIndex++;

            for (int i = 0; i <= _spawnerToIndex; i++)
            {
                Debug.Log("YYYYYYYQQQ");
                enemySpawners[i].Init(currentLevel, _difficulty, _sceneBounds.bottom, _sceneBounds.top,
                    _sceneBounds.left, _sceneBounds.right, playerManager.playerHeight, playerManager.playerWidth);
            }
        }
        else
        {
            _spawnMissionsEnded = true;
        }
    }

    private void PausePrincipalEnemies()
    {
        for (int i = 0; i <= _spawnerToIndex; i++)
        {
            enemySpawners[i].PauseSpawn();
        }
    }


    public async Task EndGameAsync()
    {
        ChangeGameState(GameState.End, false);
        scoreManager.Stop();
        levelManager.LevelChanged -= LevelManager_LevelChanged;
        levelManager.Stop();
        playerMisionsManager.Stop();
        foreach (var spawner in enemySpawners)
        {
            spawner.Stop();
        }
        
        //Con esto tampoco
        //await Task.Delay((int) Time.deltaTime * 3000);//Esto para que destruya bien los enemigos
        //Time.timeScale = 0;//Esto al final

    }

    public async void GameEnded(string playerName, string score)
    {
        if (!String.IsNullOrEmpty(playerName))
            await leaderBoardManager.SubmitScoreAsync(playerManager.playerName, (int)currentScore);
        ChangeGameState(GameState.Init);
    }

    public void ChangeGameState(GameState gameState, bool updateTimeScale = true)
    {
        if (updateTimeScale)
            Time.timeScale = gameState == GameState.Playing ? 1 : 0;

        _gameState = gameState;
        GameStateChanged?.Invoke(this, _gameState);
    }

    public void RestartGame()
    {
        _gameState = GameState.Init;
        GameStateChanged?.Invoke(this, _gameState);
    }

    private void LevelManager_LevelChanged(object sender, int level)
    {
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.LevelChanged(level);
        }
    }


    public GameDifficulty GetDifficulty()
    {
        return _difficulty;
    }

    public void SetDifficulty(GameDifficulty difficulty)
    {
        _difficulty = difficulty;
        Debug.Log("DIFFICULTY CHANGED:" + _difficulty);
    }

    //movement is a float between -1,1
    //Returns a value between -1 and 1
    public float ChangePlayerVelocity(float movement)
    {
        _enemiesVelocityMultiplier = Mathf.Clamp(_enemiesVelocityMultiplier + movement, 0.5f, 1.5f);
        return _enemiesVelocityMultiplier * 2 - 2;
    }

    public bool IsLocatedAtTheLeftOfTheScene(Vector3 transform, Vector3 localScale)
    {
        return transform.x + localScale.x / 2 < GameManager.Instance._sceneBounds.topLeftCorner.x;
    }

    public bool IsLocatedAtTheBottomOfTheScene(Vector3 transform, Vector3 localScale)
    {
        return transform.y + localScale.y / 2 < GameManager.Instance._sceneBounds.bottomRightCorner.y;
    }

    public float ClampXInSceneBounds(float newX, float width)
    {
        return Math.Clamp(
                    newX,
                    GameManager.Instance._sceneBounds.left + width / 2,
                    GameManager.Instance._sceneBounds.right - width / 2
                );
    }

    public float ClampYInSceneBounds(float newY, float height)
    {
        return Math.Clamp(
            newY,
            GameManager.Instance._sceneBounds.bottom + height / 2,
            GameManager.Instance._sceneBounds.top - height / 2
        );
    }

    public float GetRandomYInSceneBounds(Vector3 scale)
    {
        var maxY = GameManager.Instance._sceneBounds.top - scale.y / 2;
        var minY = GameManager.Instance._sceneBounds.bottom + scale.y / 2;
        return UnityEngine.Random.Range(minY, maxY);
    }

    public float GetSceneMaxX()
    {
        return this._sceneBounds.right;
    }

    public float GetSceneMaxY()
    {
        return this._sceneBounds.top;
    }

    #region Bounds Auxiliar Methods

    private Vector3 SceneTopLeftCorner()
    {
        var topLeftCornerRef = GameObject.FindGameObjectWithTag("topLeftCorner");
        return topLeftCornerRef.transform.position;
    }

    private Vector3 SceneBottomRightCorner()
    {
        var bottomRightCornerRef = GameObject.FindGameObjectWithTag("bottomRightCorner");
        return bottomRightCornerRef.transform.position;
    }

    #endregion
}

public struct SceneBounds
{
    public float top
    {
        get
        {
            return topLeftCorner.y;
        }
    }

    public float bottom
    {
        get
        {
            return bottomRightCorner.y;
        }
    }

    public float left
    {
        get
        {
            return topLeftCorner.x;
        }
    }

    public float right
    {
        get
        {
            return bottomRightCorner.x;
        }
    }

    public Vector3 topLeftCorner;
    public Vector3 bottomRightCorner;
}

public enum GameState { Init, Options, Credits, LeaderBoard, Playing, PlayingInit, PlayingOptions, PlayingCredits, PlayingPlayerWarnings,  End }

public enum GameDifficulty { Low, Medium, High }