using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class ColorMatchGameView : MonoBehaviour
{
    private const string SecondsText = " seconds";
    private const string TotalQuestionsText = "/10";
    
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _currentQuestionText;
    [SerializeField] private TMP_Text _currentColorText;
    [SerializeField] private TMP_Text _timerCount;

    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action PauseButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        DisableScreen();
    }

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(ProcessPauseButtonClicked);
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(ProcessPauseButtonClicked);
    }

    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void SetTransperent()
    {
        _screenVisabilityHandler.SetTransperentScreen();
    }
    
    public void SetTimerValue(float seconds, float miliseconds)
    {
        _timerCount.text = string.Format("{0,00} : {1,00}", seconds, miliseconds) + SecondsText;
    }

    public void SetCurrentQuestionText(string text)
    {
        _currentQuestionText.text = text + TotalQuestionsText;
    }
    
    public void SetCurrentColorText(ColorStates state)
    {
        switch (state)
        {
            case ColorStates.Blue:
                _currentColorText.text = "Blue";
                break;

            case ColorStates.Green:
                _currentColorText.text = "Green";
                break;

            case ColorStates.Orange:
                _currentColorText.text = "Orange";
                break;

            case ColorStates.Purple:
                _currentColorText.text = "Purple";
                break;

            case ColorStates.Red:
                _currentColorText.text = "Red";
                break;
            
            case ColorStates.Yellow:
                _currentColorText.text = "Yellow";
                break;
        }
    }

    private void ProcessPauseButtonClicked()
    {
        PauseButtonClicked?.Invoke();
    }
}