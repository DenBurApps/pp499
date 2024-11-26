using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitCatcherGame : MonoBehaviour
{
    private const float MaxTimerValue = 30f;
    private const int SpawnInterval = 1;
    
    [SerializeField] private FruitCatcherGameView _view;
    [SerializeField] private FruitCatcherMainMenu _mainMenu;
    [SerializeField] private InteractableObjectSpawner _spawner;
    [SerializeField] private Basket _basket;
    [SerializeField] private FruitCatcherGameMenu _gameMenu;
    [SerializeField] private FruitCatcherLoseScreen _loseScreen;
    [SerializeField] private FruitCatcherWinScreen _winScreen;

    private int _fruitCount;
    private float _currentTime;
    private IEnumerator _timerCoroutine;
    private IEnumerator _spawnCoroutine;

    public event Action GamePaused;
    public event Action GameLost;
    public event Action GameWon;
    public event Action BackToMenu;
    public event Action<InteractableObject> ObjectReceived;
    
    private void Start()
    {
        _view.Disable();
    }

    private void OnEnable()
    {
        _mainMenu.StartGameClicked += StartNewGame;

        _view.PauseButtonClicked += ProcessPause;
        
        _basket.BombCatched += ProcessGameLost;
        _basket.FruitCatched += ProcessFruitCatched;

        _gameMenu.PlayClicked += ProcessContinue;
        _gameMenu.MenuClicked += ProcessGoToMainScene;

        _loseScreen.RestartClicked += ProcessGoToMainMenu;
        _loseScreen.MenuClicked += ProcessGoToMainScene;

        _winScreen.RestartClicked += ProcessGoToMainMenu;
        _winScreen.MenuClicked += ProcessGoToMainScene;
    }

    private void OnDisable()
    {
        _mainMenu.StartGameClicked -= StartNewGame;
        
        _view.PauseButtonClicked -= ProcessPause;
        
        _basket.BombCatched -= ProcessGameLost;
        _basket.FruitCatched -= ProcessFruitCatched;
        
        _gameMenu.PlayClicked -= ProcessContinue;
        _gameMenu.MenuClicked -= ProcessGoToMainScene;
        
        _loseScreen.RestartClicked -= ProcessGoToMainMenu;
        _loseScreen.MenuClicked -= ProcessGoToMainScene;
        
        _winScreen.RestartClicked -= ProcessGoToMainMenu;
        _winScreen.MenuClicked -= ProcessGoToMainScene;
    }

    private void StartNewGame()
    {
        _view.Enable();
        ResetAllValues();

        if (_timerCoroutine == null)
        {
            _timerCoroutine = StartTimer(MaxTimerValue);
            StartCoroutine(_timerCoroutine);
        }

        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartSpawning();
            StartCoroutine(_spawnCoroutine);
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
    
    private IEnumerator StartSpawning()
    {
        WaitForSeconds interval = new WaitForSeconds(SpawnInterval);

        while (true)
        {
            _spawner.Spawn();

            yield return interval;
        }
    }

    private void UpdateTimer(float time)
    {
        time = Mathf.Max(time, 0);

        float seconds = Mathf.FloorToInt(time % 60);

        _view.SetTimerText(seconds);
    }

    private void ProcessPause()
    {
        if(_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        
        if(_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);
        
        _view.SetTransperent();
        _spawner.ReturnAllObjectsToPool();
        GamePaused?.Invoke();
    }

    private void ProcessContinue()
    {
        if(_timerCoroutine != null)
            StartCoroutine(_timerCoroutine);
        
        if(_spawnCoroutine != null)
            StartCoroutine(_spawnCoroutine);

        _view.Enable();
    }

    private void ProcessGoToMainMenu()
    {
        ResetAllValues();
        _spawner.ReturnAllObjectsToPool();
        BackToMenu?.Invoke();
        _view.Disable();
    }

    private void ProcessGoToMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void ProcessGameRestart()
    {
        ResetAllValues();
        _spawner.ReturnAllObjectsToPool();
        StartNewGame();
        _view.Enable();
    }

    private void ProcessFruitCatched(InteractableObject @object)
    {
        if (@object is Bomb)
        {
            ProcessGameLost();
            return;
        }
        
        _fruitCount++;
        ObjectReceived?.Invoke(@object);
        _view.SetFruitCountText(_fruitCount);
    }

    private void ProcessGameEnd()
    {
        GameWon?.Invoke();
        _view.Disable();
    }

    private void ProcessGameLost()
    {
        GameLost?.Invoke();
        _view.Disable();
    }

    private void ResetAllValues()
    {
        _currentTime = 0;
        _fruitCount = 0;
        
        if(_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
        
        if(_spawnCoroutine != null)
            StopCoroutine(_spawnCoroutine);

        _timerCoroutine = null;
        _spawnCoroutine = null;
        _view.SetDefaultTimerSprite();
        _view.SetFruitCountText(_fruitCount);
    }
    
    private void SavePlayCount()
    {
        if (!PlayerPrefs.HasKey("FruitCatcherPlayCount"))
        {
            PlayerPrefs.SetInt("FruitCatcherPlayCount", 1);
            PlayerPrefs.Save();
        }
    }
}
