using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderBoardPanel : BasePanel
{
    private EventHandler<GameState> gameStateChanged;

    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    private void Start()
    {
        AttachGameState(GameState.End);
    }

    private void OnDestroy()
    {
        DettachGameState();
    }

    void OnEnable()
    {
        names.ForEach(n => n.text = "");
        scores.ForEach(s => s.text = "");
        names[0].text = "Loading...";
        names[0].color = Color.white;
        GameManager.Instance.leaderBoardManager.GetScoresAsync(names, scores);
    }

    public void PlayAgain()
    {
        GameManager.Instance.RestartGame();
    }
}

