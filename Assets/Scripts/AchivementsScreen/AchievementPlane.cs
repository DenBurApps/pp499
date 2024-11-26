using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPlane : MonoBehaviour
{
    private const string PointsText = " points";
    private const int BronzeCountToActivate = 10;
    private const int GoldCountToActivate = 20;
    
    [SerializeField] private Color _achievedColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Image _silver;
    [SerializeField] private Image _bronze;
    [SerializeField] private Image _gold;
    [SerializeField] private TMP_Text _pointsText;

    protected void VerifyAchivementsStatus(string winCountKey, string playCountKey)
    {
        if (PlayerPrefs.HasKey(playCountKey))
        {
            _silver.color = _achievedColor;
        }
        else
        {
            _silver.color = _defaultColor;
        }

        if (PlayerPrefs.HasKey(winCountKey))
        {
            int winCount = PlayerPrefs.GetInt(winCountKey);

            if (winCount >= BronzeCountToActivate)
                _bronze.color = _achievedColor;
            else
                _bronze.color = _defaultColor;

            if (winCount >= GoldCountToActivate)
                _gold.color = _achievedColor;
            else
                _gold.color = _defaultColor;
        }
        else
        {
            _bronze.color = _defaultColor;
            _gold.color = _defaultColor;
        }
    }

    protected void SetPoints(string winCountKey, string playCountKey)
    {
        int totalPoints = 0;

        if (PlayerPrefs.HasKey(playCountKey))
        {
            totalPoints += PlayerPrefs.GetInt(playCountKey);
        }
        
        if (PlayerPrefs.HasKey(winCountKey))
        {
            totalPoints += PlayerPrefs.GetInt(winCountKey);
        }
        
        _pointsText.text = totalPoints.ToString() + PointsText;
    }
}
