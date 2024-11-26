public class ColorMatchAchievements : AchievementPlane
{
    private const string WinCountKey = "ColorMatchWinCount";
    private const string PlayerCountKey = "ColorMatchPlayCount";
    
    private void Start()
    {
        VerifyAchivementsStatus(WinCountKey, PlayerCountKey);
        SetPoints(WinCountKey, PlayerCountKey);
    }
}
