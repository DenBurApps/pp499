using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class FruitCatcherGameMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private FruitCatcherGame _game;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action PlayClicked;
    public event Action MenuClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        Disable();
    }

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(ProcessPlayClicked);
        _menuButton.onClick.AddListener(ProcessMenuClicked);
        _game.GamePaused += Enable;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ProcessPlayClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _game.GamePaused -= Enable;
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessMenuClicked()
    {
        MenuClicked?.Invoke();
        Disable();
    }

    private void ProcessPlayClicked()
    {
        PlayClicked?.Invoke();
        Disable();
    }
}
