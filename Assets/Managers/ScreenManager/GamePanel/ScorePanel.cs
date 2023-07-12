using UnityEngine;
using System.Collections;
using TMPro;
using System;
using System.Collections.Generic;

public class ScorePanel: BasePanel
{
	public TextMeshProUGUI scoreLabel;
	public TextMeshProUGUI scoreValue;

    // Use this for initialization
    private void Start()
	{
        AttachGameState(GameState.Playing, new List<GameState>() { GameState.PlayingPlayerWarnings });
        scoreLabel.text = "Score";
    }

    private void Update()
    {
        scoreValue.text = "" + GameManager.Instance.currentScore;
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

}

