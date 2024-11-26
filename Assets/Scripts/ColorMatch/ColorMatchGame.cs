using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ColorMatchGame : MonoBehaviour
{
    private const int MaxLevel = 10;
    private const int FirstDifficultyActiveBoxes = 2;
    private const int SecondDifficultyActiveBoxes = 4;
    private const int ThirdDifficultyActiveBoxes = 6;
    private const float FirtsDifficultyTimer = 1.5f;
    private const float SecondDifficultyTimer = 1f;

    [SerializeField] private ColorMatchGameView _view;
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private ColorMatchBox[] _colorMatchBoxes;
    [SerializeField] private ColorBoxSpriteProvider _spriteProvider;
    [SerializeField] private ColorBoxStateProvider _stateProvider;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private LoseScreen _loseScreen;
    [SerializeField] private WinScreen _winScreen;

    private float _currentTime;
    private int _currentLevel;
    private int _currentDifficulty;
    private int _correctQuestionsCount;
    private ColorStates _correctColorState;
    private IEnumerator _timerCoroutine;
    private float _currentDifficultyTimer;

    public event Action Victory;
    public event Action Lose;
    public event Action Pause;
    public event Action BackToMenu;

    public int CurrentLevel => _currentLevel;

    private void Start()
    {
        _view.DisableScreen();
    }

    private void OnEnable()
    {
        _mainMenu.StartGameClicked += ProcessGamesStart;
        _view.PauseButtonClicked += ProcessGamePause;
        _pauseMenu.PlayClicked += ContinueGame;
        _pauseMenu.MenuClicked += ProcessBackToMainMenu;
        _loseScreen.RestartClicked += ProcessMenuClicked;
        _loseScreen.MenuClicked += ProcessBackToMainMenu;
        _winScreen.RestartClicked += ProcessMenuClicked;
        _winScreen.MenuClicked += ProcessBackToMainMenu;
    }

    private void OnDisable()
    {
        _mainMenu.StartGameClicked -= ProcessGamesStart;
        _view.PauseButtonClicked -= ProcessGamePause;
        _pauseMenu.PlayClicked -= ContinueGame;
        _pauseMenu.MenuClicked -= ProcessBackToMainMenu;
        _loseScreen.RestartClicked -= ProcessMenuClicked;
        _loseScreen.MenuClicked -= ProcessBackToMainMenu;
        _winScreen.RestartClicked -= ProcessMenuClicked;
        _winScreen.MenuClicked -= ProcessBackToMainMenu;
    }

    private void ProcessGamesStart()
    {
        ResetToDefault();
        _view.EnableScreen();
        StartGame();
    }

    private void ProcessGamePause()
    {
        Pause?.Invoke();
        _view.SetTransperent();

        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
    }

    private void ContinueGame()
    {
        _view.EnableScreen();

        if (_timerCoroutine != null)
            StartCoroutine(_timerCoroutine);
    }

    private void ProcessMenuClicked()
    {
        BackToMenu?.Invoke();
        ResetToDefault();
        _view.DisableScreen();
    }

    private void ProcessBackToMainMenu()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void StartGame()
    {
        if (_currentLevel >= FirstDifficultyActiveBoxes)
        {
            _currentDifficulty = SecondDifficultyActiveBoxes;
        }

        if (_currentLevel >= SecondDifficultyActiveBoxes)
        {
            _currentDifficulty = ThirdDifficultyActiveBoxes;
        }

        if (_currentLevel >= ThirdDifficultyActiveBoxes)
        {
            _currentDifficultyTimer = SecondDifficultyTimer;
        }

        SetColorMatchBoxes(_currentDifficulty);

        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;
            _currentTime = 0;
        }

        _timerCoroutine = StartTimer(_currentDifficultyTimer);
        StartCoroutine(_timerCoroutine);
        _view.SetCurrentQuestionText(_currentLevel.ToString());

        SavePlayCount();
    }

    private void SetColorMatchBoxes(int boxesToOpenCount)
    {
        List<ColorStates> receivedStates = _stateProvider.GetRandomColorsStates(boxesToOpenCount);

        int randomIndex = Random.Range(0, receivedStates.Count);
        _correctColorState = receivedStates[randomIndex];
        _view.SetCurrentColorText(_correctColorState);

        for (int i = 0; i < boxesToOpenCount; i++)
        {
            _colorMatchBoxes[i].Enable();
            _colorMatchBoxes[i].SetCurrentColorState(receivedStates[i]);
            _colorMatchBoxes[i].SetColorSprite(_spriteProvider.GetColorSprite(receivedStates[i]));
            _colorMatchBoxes[i].Clicked += ProcessMatchBoxClick;
        }
    }

    private void ProcessMatchBoxClick(ColorMatchBox colorBox)
    {
        foreach (var box in _colorMatchBoxes)
        {
            box.Clicked -= ProcessMatchBoxClick;
            box.Disable();
        }

        if (colorBox.CurrentColorState == _correctColorState)
        {
            CorrectMatchBoxChosen();
        }
        else
        {
            IncorrectMatchBoxChosen();
        }
    }

    private void CorrectMatchBoxChosen()
    {
        _correctQuestionsCount++;
        _currentLevel++;

        if (_correctQuestionsCount >= MaxLevel)
        {
            StopCoroutine(_timerCoroutine);
            Victory?.Invoke();
            _view.DisableScreen();
        }
        else
        {
            StartGame();
        }
    }

    private void IncorrectMatchBoxChosen()
    {
        Lose?.Invoke();
        ResetToDefault();
        _view.DisableScreen();
    }

    private IEnumerator StartTimer(float maxTime)
    {
        _currentTime = maxTime;

        while (_currentTime >= 0f)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimer(_currentTime);
            yield return null;
        }

        IncorrectMatchBoxChosen();
    }

    private void UpdateTimer(float time)
    {
        time = Mathf.Max(time, 0);

        float seconds = Mathf.FloorToInt(time % 60);
        float miliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        _view.SetTimerValue(seconds, miliseconds);
    }

    private void ResetToDefault()
    {
        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = null;
        _correctColorState = default;
        _currentLevel = 1;
        _correctQuestionsCount = 0;
        _currentDifficulty = FirstDifficultyActiveBoxes;
        _currentTime = 0;
        _currentDifficultyTimer = FirtsDifficultyTimer;

        foreach (var colorBox in _colorMatchBoxes)
        {
            colorBox.Clicked -= ProcessMatchBoxClick;
            colorBox.Disable();
        }

        _view.SetCurrentQuestionText(_currentLevel.ToString());
    }

    private void SavePlayCount()
    {
        if (!PlayerPrefs.HasKey("ColorMatchPlayCount"))
        {
            PlayerPrefs.SetInt("ColorMatchPlayCount", 1);
            PlayerPrefs.Save();
        }
    }
}