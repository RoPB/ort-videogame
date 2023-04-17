using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;

public class LeaderBoardManager : MonoBehaviour
{
    private const string LeaderboardPublicKey = "fca0e5a15240f0341538061fd6129ce2a1f9918031ee0c8fbb84ad5bb397421d";
    private const string LeaderboardSecret = "1621928aad8305387fe2b9c451a2a62831ab548336fe30158049df396731446516f63d49c5b5e6a57491dfb80864d2278bb6748e12908032c160f920f290351cfdd003a3077dcf196b6880cafeb6fb191fa0f450678cb062a9fda9e8498fde74ad2439b580c4317fa9592cd84275cf489e6dc5218b393251f3e7a027ef40c2b5";

    public List<LeaderboardEntry> leaderboardEntries = new List<LeaderboardEntry>();

    public void SubmitScoreAsync(string name, int score)
    {
        Debug.Log("SubmitScoreAsync");
        LeaderboardCreator.UploadNewEntry(LeaderboardPublicKey, name, score, (success) =>
        {
            Debug.Log("SubmitScoreAsync success: " + success);
        });
    }

    public void GetScoresAsync()
    {
        Debug.Log("GetScoresAsync");
        var query = new Dan.Models.LeaderboardSearchQuery()
        {
            Take = 10,
        };
        LeaderboardCreator.GetLeaderboard(LeaderboardPublicKey, query, (leaderboard) =>
        {
            foreach (var entry in leaderboard)
            {
                Debug.Log(entry.Username + " " + entry.Score);
                var e = new LeaderboardEntry()
                {
                    name = entry.Username,
                    score = entry.Score,
                };
                leaderboardEntries.Add(e);
            }
        });
    }
}

public struct LeaderboardEntry
{
    public string name;
    public int score;
}