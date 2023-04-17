using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class ScorePanel: BasePanel
{
	public TextMeshProUGUI scoreLabel;
	public TextMeshProUGUI scoreValue;

    // Use this for initialization
    private void Start()
	{
        AttachGameState(GameState.Playing);
        scoreLabel.text = "Score";
    }

    private void Update()
    {
        scoreValue.text = "" + Mathf.Round(GameManager.Instance.currentScore);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

}

