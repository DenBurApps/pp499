using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class WinScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private ColorMatchGame _game;
    
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
        _game.Victory += EnableScreen;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ProcessRestartClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _game.Victory -= EnableScreen;
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
        if (PlayerPrefs.HasKey("ColorMatchWinCount"))
        {
            int currentCount = PlayerPrefs.GetInt("ColorMatchWinCount");
            PlayerPrefs.SetInt("ColorMatchWinCount", currentCount++);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("ColorMatchWinCount", 1);
            PlayerPrefs.Save();
        }
    }
    
}
