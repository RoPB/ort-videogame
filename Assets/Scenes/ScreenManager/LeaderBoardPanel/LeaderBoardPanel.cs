using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    async void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            names.ForEach(n => n.text = "");
            scores.ForEach(s => s.text = "");
            names[0].text = "Loading...";
            names[0].color = Color.white;
            var leaderboard = await GameManager.Instance.leaderBoardManager.GetLeaderboardAsync(Math.Min(names.Count, scores.Count));
            for (int i = 0; i < leaderboard.Length; i++)
            {
                var entry = leaderboard[i];
                names[i].text = entry.Username;
                scores[i].text = entry.Score.ToString();
                if (entry.IsMine())
                {
                    names[i].color = Color.yellow;
                    scores[i].color = Color.yellow;
                }
                else
                {
                    names[i].color = Color.white;
                    scores[i].color = Color.white;
                }
            }
        }
        
    }

    public void PlayAgain()
    {
        GameManager.Instance.RestartGame();
    }
}

