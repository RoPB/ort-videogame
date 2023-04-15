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


    public event EventHandler<int> LevelChanged;

    // Update is called once per frame
    void Update()
	{
        if (_levelInitiated)
            Evaluate();
    }

    public void Init()
    {
        Reset();
        _levelInitiated = true;
    }

    public void Stop()
    {
        _levelInitiated = false;
    }

    private void Reset()
    {
        _dtSum = 0;
    }

    private void Evaluate()
    {
        _dtSum += Time.deltaTime;

        if (_dtSum > increaseLevelDt)
        {
            _dtSum = 0;
            _currentLevel++;
            LevelChanged?.Invoke(this, _currentLevel);
            Debug.Log("LEVEL:" + _currentLevel);
        }
    }
}

