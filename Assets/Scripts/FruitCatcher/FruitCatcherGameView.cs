using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class FruitCatcherGameView : MonoBehaviour
{
    private const string SecondsText = " seconds";
    private const string FruitText = " fruit";

    [SerializeField] private Image _timerImage;
    [SerializeField] private Sprite _defaultTimerSprite;
    [SerializeField] private Sprite _changedTimerSprite;
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private TMP_Text _catchedFruitText;
    [SerializeField] private Button _pauseButton;

    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action PauseButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
        _pauseButton.onClick.AddListener(ProcessPauseClicked);
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
        _pauseButton.onClick.RemoveListener(ProcessPauseClicked);
    }

    public void SetFruitCountText(int amount)
    {
        _catchedFruitText.text = amount.ToString() + FruitText;
    }

    public void SetTimerText(float time)
    {
        _timerText.text = time.ToString() + SecondsText;
    }

    public void SetDefaultTimerSprite()
    {
        _timerImage.sprite = _defaultTimerSprite;
    }

    public void SetChangedTimerSprite()
    {
        _timerImage.sprite = _changedTimerSprite;
    }

    public void SetTransperent()
    {
        _screenVisabilityHandler.SetTransperentScreen();
    }

    private void ProcessPauseClicked()
    {
        PauseButtonClicked?.Invoke();
    }
}