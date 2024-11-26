using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class LoseScreen : MonoBehaviour
{
    private const string TotalQuestionsText = "/10";
    
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private ColorMatchGame _game;
    [SerializeField] private TMP_Text _questionNumberText;
    
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
        _game.Lose += EnableScreen;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ProcessRestartClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _game.Lose -= EnableScreen;
    }

    public void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
        _questionNumberText.text = _game.CurrentLevel.ToString() + TotalQuestionsText;
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
}
