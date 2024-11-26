public class PuzzleChallengeAchievements : AchievementPlane
{
    private const string WinCountKey = "PuzzleChallengeWinCount";
    private const string PlayerCountKey = "PuzzleChallengePlayCount";

    private void Start()
    {
        VerifyAchivementsStatus(WinCountKey, PlayerCountKey);
        SetPoints(WinCountKey, PlayerCountKey);
    }
}