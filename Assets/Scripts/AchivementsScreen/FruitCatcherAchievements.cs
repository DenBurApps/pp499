public class FruitCatcherAchievements : AchievementPlane
{
    private const string WinCountKey = "FruitCatcherWinCount";
    private const string PlayerCountKey = "FruitCatcherPlayCount";
    
    private void Start()
    {
        VerifyAchivementsStatus(WinCountKey, PlayerCountKey);
        SetPoints(WinCountKey, PlayerCountKey);
    }
}
