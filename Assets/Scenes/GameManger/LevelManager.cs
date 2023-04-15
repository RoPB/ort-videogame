using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    [Range(10, 30)]
    public int increaseLevelDt = 10;

    private bool _levelInitiated = false;
    private float _dtSum = 0;
    private int _currentLevel = 1;
    public int currentLevel => _currentLevel;

    // Update is called once per frame
    void Update()
	{
        if (_levelInitiated)
            checkLevel();
    }

    public void init()
    {
        resetLevel();
        _levelInitiated = true;
    }

    private void resetLevel()
    {
        _dtSum = 0;
    }

    private void checkLevel()
    {
        _dtSum += Time.deltaTime;

        if (_dtSum > increaseLevelDt)
        {
            _currentLevel++;
        }
    }
}

