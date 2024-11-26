using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class FruitCatcherWinScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private FruitCatcherGame _game;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;
    
    public event Action RestartClicked;
    public event Action MenuClicked;
    
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
        _restartButton.onClick.AddListener(ProcessRestartClicked);
        _menuButton.onClick.AddListener(ProcessMenuClicked);
        _game.GameWon += EnableScreen;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ProcessRestartClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _game.GameWon -= EnableScreen;
    }
    
    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
        SaveWinCount();
    }

    public void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessRestartClicked()
    {
        RestartClicked?.Invoke();
        DisableScreen();
    }

    private void ProcessMenuClicked()
    {
        MenuClicked?.Invoke();
        DisableScreen();
    }
    
    private void SaveWinCount()
    {
        if (PlayerPrefs.HasKey("FruitCatcherWinCount"))
        {
            int currentCount = PlayerPrefs.GetInt("FruitCatcherWinCount");
            PlayerPrefs.SetInt("FruitCatcherWinCount", currentCount++);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("FruitCatcherWinCount", 1);
            PlayerPrefs.Save();
        }
    }
}
