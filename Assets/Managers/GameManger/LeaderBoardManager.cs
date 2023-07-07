using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dan.Main;
using TMPro;
using System.Threading.Tasks;

public class LeaderBoardManager : MonoBehaviour
{
    private const string OldLeaderboardPublicKey = "fca0e5a15240f0341538061fd6129ce2a1f9918031ee0c8fbb84ad5bb397421d";
    private const string OldLeaderboardSecret = "1621928aad8305387fe2b9c451a2a62831ab548336fe30158049df396731446516f63d49c5b5e6a57491dfb80864d2278bb6748e12908032c160f920f290351cfdd003a3077dcf196b6880cafeb6fb191fa0f450678cb062a9fda9e8498fde74ad2439b580c4317fa9592cd84275cf489e6dc5218b393251f3e7a027ef40c2b5";
    private const string LeaderboardPublicKey = "d002462091419e624016564c4ae2fb44c6184b64d71373974b2c40ec65dd909f";
    private const string LeaderboardSecret = "ab1227635ef06c88cbc44e4ce19876234e036dee2c401a58d9050a0e0a835c6b0ef09c139422e5c332c667b12105755c608ea3de83f549586f027c3f6140772ef162dbfa28862abe49b5acff2c2ee528047d866dff3932fb615f78cadc64092aacb4cd329d6887e2fa02bd15a18d96e88dc6b39d543aa5d91fe1c7911951f7a3";

    public Task<bool> SubmitScoreAsync(string name, int score)
    {
        Debug.Log("SubmitScoreAsync");
        var task = new TaskCompletionSource<bool>();
        LeaderboardCreator.UploadNewEntry(LeaderboardPublicKey, name, score, (success) =>
        {
            Debug.Log("SubmitScoreAsync success: " + success);
            task.SetResult(success);
        });
        return task.Task;
    }

    public Task<Dan.Models.Entry[]> GetLeaderboardAsync(int take = 10)
    {
        Debug.Log("GetScoresAsync");
        var query = new Dan.Models.LeaderboardSearchQuery()
        {
            Take = take,
        };
        var task = new TaskCompletionSource<Dan.Models.Entry[]>();
        LeaderboardCreator.GetLeaderboard(LeaderboardPublicKey, query, (leaderboard) =>
        {
            task.SetResult(leaderboard);
        });

        return task.Task;
    }
}
