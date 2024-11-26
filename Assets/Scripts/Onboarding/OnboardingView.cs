using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class OnboardingView : MonoBehaviour
{
    [SerializeField] private Button _continueButton;

    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action ContinueClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _continueButton.onClick.AddListener(ProcessButtonClick);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveListener(ProcessButtonClick);
    }

    public void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    private void ProcessButtonClick()
    {
        ContinueClicked?.Invoke();
    }
}
