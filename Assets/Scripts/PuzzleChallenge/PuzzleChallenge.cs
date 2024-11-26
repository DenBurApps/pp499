using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleChallengeGame : MonoBehaviour
{
    private const int MaximumCorrectPuzzlesAmount = 12;
    private const float MaxTimer = 60f;
    
    [SerializeField] private PuzzleChallengeView _view;
    [SerializeField] private PuzzleToSolvePieceHolder[] _puzzles;
    [SerializeField] private PuzzleChallengeSelection _selectionScreen;
    [SerializeField] private PuzzlePiecesHolder[] _puzzlePiecesHolders;
    [SerializeField] private PuzzleChallengeGameMenu _gameMenu;
    [SerializeField] private PuzzleChallengeWinScreen _winScreen;
    [SerializeField] private PuzzleChallengeLoseScreen _loseScreen;

    private int _correctPuzzlesPlaced;
    private float _currentTime;
    private IEnumerator _timerCoroutine;
    private int _currentChosenIndex;

    public event Action GameWon;
    public event Action GameLost;
    public event Action GamePaused;
    public event Action BackToMenu;
    
    private void Start()
    {
        _view.Disable();
        ReturnToDefault();
    }

    private void OnEnable()
    {
        _selectionScreen.FirstPuzzleSelected += () => ActivatePuzzle(0);
        _selectionScreen.SecondPuzzleSelected += () => ActivatePuzzle(1);
        _selectionScreen.ThirdPuzzleSelected += () => ActivatePuzzle(2);
        _selectionScreen.FourthPuzzleSelected += () => ActivatePuzzle(3);

        _view.PauseClicked += ProcessPause;

        _gameMenu.PlayClicked += ProcessContinue;
        _gameMenu.MenuClicked += ProcessReturnToMainScene;

        _winScreen.MenuClicked += ProcessReturnToMainScene;
        _winScreen.RestartClicked += ProcessReturnToMenu;

        _loseScreen.MenuClicked += ProcessReturnToMainScene;
        _loseScreen.RestartClicked += ProcessReturnToMenu;
    }

    private void OnDisable()
    {
        _selectionScreen.FirstPuzzleSelected -= () => ActivatePuzzle(0);
        _selectionScreen.SecondPuzzleSelected -= () => ActivatePuzzle(1);
        _selectionScreen.ThirdPuzzleSelected -= () => ActivatePuzzle(2);
        _selectionScreen.FourthPuzzleSelected -= () => ActivatePuzzle(3);
        
        _view.PauseClicked -= ProcessPause;

        _gameMenu.PlayClicked -= ProcessContinue;
        _gameMenu.MenuClicked -= ProcessReturnToMenu;

        _winScreen.MenuClicked -= ProcessReturnToMenu;
        _winScreen.RestartClicked -= ProcessRestart;

        _loseScreen.MenuClicked -= ProcessReturnToMenu;
        _loseScreen.RestartClicked -= ProcessRestart;
    }

    private void ActivatePuzzle(int index)
    {
        _view.Enable();

        _currentChosenIndex = index;
        
        if(_puzzles[index] == null || _puzzlePiecesHolders[index] == null)
            return;
        
        _puzzles[index].gameObject.SetActive(true);
        _puzzles[index].OnePiecePlaced += UpdateTotalPuzzlesPlaced;
        _puzzlePiecesHolders[index].gameObject.SetActive(true);
        _view.ActivatePuzzleImage(index);

        if (_timerCoroutine == null)
        {
            _timerCoroutine = StartTimer(MaxTimer);
            StartCoroutine(_timerCoroutine);
        }

        SavePlayCount();
    }
    
    private IEnumerator StartTimer(float maxTime)
    {
        _currentTime = maxTime;

        while (_currentTime >= 0f)
        {
            _currentTime -= Time.deltaTime;
            UpdateTimer(_currentTime);
            
            if(_currentTime <= 4f)
                _view.SetChangedTimerSprite();
            
            yield return null;
        }

        ProcessGameEnd();
    }

    private void UpdateTimer(float time)
    {
        time = Mathf.Max(time, 0);

        float seconds = Mathf.FloorToInt(time % 60);

        _view.SetTimerText(seconds);
    }

    private void ReturnToDefault()
    {
        _correctPuzzlesPlaced = 0;
        
        foreach (var puzzle in _puzzles)
        {
            puzzle.OnePiecePlaced -= UpdateTotalPuzzlesPlaced;
            puzzle.gameObject.SetActive(false);
        }
        
        foreach (var puzzle in _puzzlePiecesHolders)
        {
            puzzle.gameObject.SetActive(false);
        }
        
        _view.SetDefaultTimerSprite();
        
        if(_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = null;
    }

    private void ProcessRestart()
    {
        _view.Enable();
        ActivatePuzzle(_currentChosenIndex);
    }

    private void ProcessGameEnd()
    {
        GameLost?.Invoke();
        _view.Disable();
        ReturnToDefault();
    }

    private void ProcessPause()
    {
        GamePaused?.Invoke();
        _view.SetTransperent();
        
        if(_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
    }

    private void ProcessContinue()
    {
        _view.Enable();
        
        if(_timerCoroutine != null)
            StartCoroutine(_timerCoroutine);
    }

    private void ProcessReturnToMenu()
    {
        _view.Disable();
        ReturnToDefault();
        BackToMenu?.Invoke();
    }

    private void ProcessReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    private void UpdateTotalPuzzlesPlaced()
    {
        _correctPuzzlesPlaced++;

        if (_correctPuzzlesPlaced >= MaximumCorrectPuzzlesAmount)
        {
            GameWon?.Invoke();
            _view.Disable();
            ReturnToDefault();
        }
    }
    
    private void SavePlayCount()
    {
        if (!PlayerPrefs.HasKey("PuzzleChallengePlayCount"))
        {
            PlayerPrefs.SetInt("PuzzleChallengePlayCount", 1);
            PlayerPrefs.Save();
        }
    }
}
