using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class SettingsScreenView : MonoBehaviour
{
    [SerializeField] private Button _contactButton;
    [SerializeField] private Button _rateUsButton;
    [SerializeField] private Button _privacyPolicyButton;
    [SerializeField] private Button _termsOfUseButton;
    [SerializeField] private Button _versionButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _achievementsButton;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action AchievementsButtonClicked;
    public event Action MenuButtonClicked;
    public event Action VersionButtonClicked;
    public event Action RateUsClicked;
    public event Action ContactButtonClicked;
    public event Action TermsOfUseClicked;
    public event Action PrivacyClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _contactButton.onClick.AddListener(ProcessCoctactClicked);
        _rateUsButton.onClick.AddListener(ProcessRateUsClicked);
        _privacyPolicyButton.onClick.AddListener(ProcessPrivacyPolicyClicked);
        _termsOfUseButton.onClick.AddListener(ProcessTermsOfUseClicked);
        _versionButton.onClick.AddListener(ProcessVersionClicked);
        _menuButton.onClick.AddListener(ProcessMenuClicked);
        _achievementsButton.onClick.AddListener(ProcessAchievemntsClicked);
    }

    private void OnDisable()
    {
        _contactButton.onClick.RemoveListener(ProcessCoctactClicked);
        _rateUsButton.onClick.RemoveListener(ProcessRateUsClicked);
        _privacyPolicyButton.onClick.RemoveListener(ProcessPrivacyPolicyClicked);
        _termsOfUseButton.onClick.RemoveListener(ProcessTermsOfUseClicked);
        _versionButton.onClick.RemoveListener(ProcessVersionClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _achievementsButton.onClick.RemoveListener(ProcessAchievemntsClicked);
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessCoctactClicked()
    {
        ContactButtonClicked?.Invoke();
    }

    private void ProcessRateUsClicked()
    {
        RateUsClicked?.Invoke();
    }

    private void ProcessPrivacyPolicyClicked()
    {
        PrivacyClicked?.Invoke();
    }

    private void ProcessTermsOfUseClicked()
    {
        TermsOfUseClicked?.Invoke();
    }

    private void ProcessVersionClicked()
    {
        VersionButtonClicked?.Invoke();
    }

    private void ProcessMenuClicked()
    {
        MenuButtonClicked?.Invoke();
    }

    private void ProcessAchievemntsClicked()
    {
        AchievementsButtonClicked?.Invoke();
    }
}
