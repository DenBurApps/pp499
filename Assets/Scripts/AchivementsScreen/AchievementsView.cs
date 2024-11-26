using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class AchievementsView : MonoBehaviour
{
    [SerializeField] private Button _mainScreenButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private MainScreen _mainScreen;
    [SerializeField] private SettingsScreen _settingsScreen;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;
    
    public event Action SettingsClicked;
    public event Action MenuClicked;

    private void Start()
    {
        DisableScreen();
    }

    private void OnEnable()
    {
        _mainScreen.AchiementsButtonClicked += EnableScreen;
        _settingsScreen.AchievementsClicked += EnableScreen;

        _mainScreenButton.onClick.AddListener(ProcessMainMenuClicked);
        _settingsButton.onClick.AddListener(ProcessSettingClicked);
    }

    private void OnDisable()
    {
        _mainScreen.AchiementsButtonClicked -= EnableScreen;
        _settingsScreen.AchievementsClicked -= EnableScreen;
        
        _mainScreenButton.onClick.RemoveListener(ProcessMainMenuClicked);
        _settingsButton.onClick.RemoveListener(ProcessSettingClicked);
    }

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessSettingClicked()
    {
        SettingsClicked?.Invoke();
        DisableScreen();
    }

    private void ProcessMainMenuClicked()
    {
        MenuClicked?.Invoke();
        DisableScreen();
    }
}
