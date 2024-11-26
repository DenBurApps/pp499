using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Onboarding : MonoBehaviour
{
    [SerializeField] private OnboardingView _firstScreenView;
    [SerializeField] private OnboardingView _secondScreenView;
    [SerializeField] private OnboardingView _thirdScreenView;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("OnboardingComplete"))
            SceneManager.LoadScene("MainScene");
    }

    private void Start()
    {
        _firstScreenView.EnableScreen();
        _secondScreenView.DisableScreen();
        _secondScreenView.DisableScreen();
    }

    private void OnEnable()
    {
        _firstScreenView.ContinueClicked += ProcessFirstScreenButtonClick;
        _secondScreenView.ContinueClicked += ProcessSecondScreenButtonClick;
        _thirdScreenView.ContinueClicked += ProcessThirdScreenButtonClick;
    }

    private void OnDisable()
    {
        _firstScreenView.ContinueClicked -= ProcessFirstScreenButtonClick;
        _secondScreenView.ContinueClicked -= ProcessSecondScreenButtonClick;
        _thirdScreenView.ContinueClicked -= ProcessThirdScreenButtonClick;
    }

    private void ProcessFirstScreenButtonClick()
    {
        _firstScreenView.DisableScreen();
        _secondScreenView.EnableScreen();
        _thirdScreenView.DisableScreen();
    }

    private void ProcessSecondScreenButtonClick()
    {
        _firstScreenView.DisableScreen();
        _secondScreenView.DisableScreen();
        _thirdScreenView.EnableScreen();
    }

    private void ProcessThirdScreenButtonClick()
    {
        if (!PlayerPrefs.HasKey("OnboardingComplete"))
            PlayerPrefs.SetInt("OnboardingComplete", 1);
        
        SceneManager.LoadScene("MainScene");
    }
}