using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif

public class SettingsScreen : MonoBehaviour
{
    private string _email = "SVspb2020@icloud.com";
    
    [SerializeField] private SettingsScreenView _view;
    [SerializeField] private MainScreen _mainScreen;
    [SerializeField] private AchievementsView _achievementsView;
    [SerializeField] private PrivacyPolicyView _privacyPolicy;
    [SerializeField] private TermsOfUseView _termsOfUse;
    [SerializeField] private VersionView _versionView;
    
    public event Action MenuClicked;
    public event Action AchievementsClicked;

    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _mainScreen.SettingButtonClicked += _view.Enable;
        _achievementsView.SettingsClicked += _view.Enable;
        _view.RateUsClicked += ProcessRateUsClicked;
        _view.PrivacyClicked += ProcessPrivacyPolicyClicked;
        _view.TermsOfUseClicked += ProcessTermsOfUseClicked;
        _privacyPolicy.BackButtonClicked += _view.Enable;
        _termsOfUse.BackButtonClicked += _view.Enable;
        _versionView.BackButtonClicked += _view.Enable;
        _view.MenuButtonClicked += ProcessMenuOpen;
        _view.AchievementsButtonClicked += ProcessAchievemntsOpen;
        _view.ContactButtonClicked += ProcessContactUsClicked;
        _view.VersionButtonClicked += ProcessVersionClicked;
        
        
    }

    private void OnDisable()
    {
        _mainScreen.SettingButtonClicked -= _view.Enable;
        _achievementsView.SettingsClicked -= _view.Enable;
        _view.RateUsClicked -= ProcessRateUsClicked;
        _view.PrivacyClicked -= ProcessPrivacyPolicyClicked;
        _view.TermsOfUseClicked -= ProcessTermsOfUseClicked;
        _privacyPolicy.BackButtonClicked -= _view.Enable;
        _termsOfUse.BackButtonClicked -= _view.Enable;
        _versionView.BackButtonClicked -= _view.Enable;
        _view.MenuButtonClicked -= ProcessMenuOpen;
        _view.AchievementsButtonClicked -= ProcessAchievemntsOpen;
        _view.ContactButtonClicked -= ProcessContactUsClicked;
        _view.VersionButtonClicked -= ProcessVersionClicked;
    }


    private void ProcessRateUsClicked()
    {
#if UNITY_IOS
        Device.RequestStoreReview();
#endif
    }

    private void ProcessPrivacyPolicyClicked()
    {
        _privacyPolicy.Enable();
        _view.Disable();
    }

    private void ProcessTermsOfUseClicked()
    {
        _termsOfUse.Enable();
        _view.Disable();
    }

    private void ProcessVersionClicked()
    {
        _versionView.Enable();
        _view.Disable();
    }

    private void ProcessContactUsClicked()
    {
        Application.OpenURL("mailto:" + _email + "?subject=Mail to developer");
    }

    private void ProcessAchievemntsOpen()
    {
        AchievementsClicked?.Invoke();
        _view.Disable();
    }

    private void ProcessMenuOpen()
    {
        MenuClicked?.Invoke();
        _view.Disable();
    }
}
