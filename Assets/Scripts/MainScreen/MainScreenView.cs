using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class MainScreenView : MonoBehaviour
{
    [SerializeField] private Button _quickPlayButton;
    [SerializeField] private Button _colorMatchButton;
    [SerializeField] private Button _fruitCatcherButton;
    [SerializeField] private Button _puzzleChallengeButton;
    [SerializeField] private Button _achiementsButton;
    [SerializeField] private Button _settingsButton;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action QuickPlayClicked;
    public event Action ColorMatchClicked;
    public event Action FruitCatcherClicked;
    public event Action PuzzleChallengeClicked;
    public event Action AchievementsButtonClicked;
    public event Action SettingButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }
    
    private void OnEnable()
    {
       _quickPlayButton.onClick.AddListener(ProcessQuickPlayClicked);
       _colorMatchButton.onClick.AddListener(ProcessColorMatchClicked);
       _fruitCatcherButton.onClick.AddListener(ProcessFruitCatcherClicked);
       _puzzleChallengeButton.onClick.AddListener(ProcessPuzzleChallengeClicked);
       _achiementsButton.onClick.AddListener(ProcessAchievemntButtonClicked);
       _settingsButton.onClick.AddListener(ProcessSettingsButtonClicked);
    }

    private void OnDisable()
    {
        _quickPlayButton.onClick.RemoveListener(ProcessQuickPlayClicked);
        _colorMatchButton.onClick.RemoveListener(ProcessColorMatchClicked);
        _fruitCatcherButton.onClick.RemoveListener(ProcessFruitCatcherClicked);
        _puzzleChallengeButton.onClick.RemoveListener(ProcessPuzzleChallengeClicked);
        _achiementsButton.onClick.RemoveListener(ProcessAchievemntButtonClicked);
        _settingsButton.onClick.RemoveListener(ProcessSettingsButtonClicked);
    }

    public void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    private void ProcessQuickPlayClicked()
    {
        QuickPlayClicked?.Invoke();
    }

    private void ProcessColorMatchClicked()
    {
        ColorMatchClicked?.Invoke();
    }

    private void ProcessFruitCatcherClicked()
    {
        FruitCatcherClicked?.Invoke();
    }

    private void ProcessPuzzleChallengeClicked()
    {
        PuzzleChallengeClicked?.Invoke();
    }

    private void ProcessSettingsButtonClicked()
    {
        SettingButtonClicked?.Invoke();
    }

    private void ProcessAchievemntButtonClicked()
    {
        AchievementsButtonClicked?.Invoke();
    }
}
