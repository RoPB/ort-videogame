﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using static UnityEditor.PlayerSettings;

public class PlayerMisionsManager : MonoBehaviour
{
    public List<PlayerMisions> playerMisionsGrouped;
    private int _playerMisionsGroupedIndex;
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

    public DropsPickup dropsPickup;
    private DropsPickup _dropPickup;

    private void OnDestroy()
    {
        GameObject.Destroy(_dropPickup);
    }

    public void Init(float yMin, float yMax, float xMin, float xMax, float playerHeight, float playerWidth)
    {
        if (_dropPickup == null)
            _dropPickup = GameObject.Instantiate<DropsPickup>(dropsPickup);

        _playerMisionsGroupedIndex = -1;
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
        foreach(var pmg in playerMisionsGrouped)
        {
            pmg.StopPlayerMisions();
        }

    }

    private int _counterNoNextPlayerMissions;
    internal void TryExecuteMision()
    {
        _counterNoNextPlayerMissions = 0;
        _currentMisionIndex++;
        ExecuteNextMision();
    }


    private void ExecuteNextMision()
    {
        if (_playerMisionsGroupedIndex < playerMisionsGrouped.Count - 1)
        {
            if (_currentMision != null)
                _currentMision.enemies.PauseSpawn();

            _playerMisionsGroupedIndex++;
            _currentMision = playerMisionsGrouped[_playerMisionsGroupedIndex].GetByIndex(_currentMisionIndex);
            if (_currentMision != null)
            {
                _counterNoNextPlayerMissions = 0;
                StartCoroutine(ExecuteMision());

                //Drop weapon when second mision
                if(_playerMisionsGroupedIndex != 0 && _playerMisionsGroupedIndex == 2 )
                    _dropPickup.DropExtraWeaponPickup();
            }
            else
            {
                _counterNoNextPlayerMissions++;
                ExecuteNextMision();
            }

        }
        else if (_playerMisionsGroupedIndex >= playerMisionsGrouped.Count - 1)
        {
            EndExecution();
        }

    }

    private void EndExecution()
    {
        var ended = _counterNoNextPlayerMissions >= playerMisionsGrouped.Count;
        _playerMisionsGroupedIndex = -1;
        _currentMision = null;
        StartCoroutine(InitPrincipalSpawn(ended));
    }

    private IEnumerator ExecuteMision()
    {
        try
        {
            GameManager.Instance.playerWarnMsg = _currentMision.msg;
            GameManager.Instance.playerWarnMsgDescription = _currentMision.description;

            GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);
        }
        catch (Exception ex) { }

        yield return new WaitForSeconds(3f);

        try
        {
            if(GameManager.Instance.ResumeGame())
                SpawnMisionEnemies();
        }
        catch (Exception ex) { }

    }

    private void SpawnMisionEnemies()
    {
        try
        {
            _currentMision.enemies.OnSpawnEnded += CurrentMision_OnSpawnEnded;
            _currentMision.enemies.Init(GameManager.Instance.currentLevel, GameManager.Instance.GetDifficulty(), _yMin, _yMax, _xMin, _xMax, _playerWidth, _playerHeight);
        }
        catch (Exception ex) { }

    }

    private void CurrentMision_OnSpawnEnded(object sender, float e)
    {
        try
        {
            _currentMision.enemies.OnSpawnEnded -= CurrentMision_OnSpawnEnded;
        }
        catch (Exception ex) { }//not sure why get an error here

        ExecuteNextMision();
    }

    private IEnumerator InitPrincipalSpawn(bool ended)
    {
        if (ended)
        {
            GameManager.Instance.playerWarnMsg = "";
            GameManager.Instance.playerWarnMsgDescription = "No more unkown enemies in this galaxy kill them all";
        }
        else
        {
            GameManager.Instance.playerWarnMsg = "Be careful";
            GameManager.Instance.playerWarnMsgDescription = "All them will appear togheter to save the galaxy";
        }

        GameManager.Instance.ChangeGameState(GameState.PlayingPlayerWarnings, false);

        yield return new WaitForSeconds(3f);

        if(GameManager.Instance.ResumeGame())
            GameManager.Instance.InitPrincipalSpawn(ended);
    }

}

