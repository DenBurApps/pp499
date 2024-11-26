using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private ColorMatchGame _game;
    
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
        _game.Pause += Enable;
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(ProcessPlayClicked);
        _menuButton.onClick.RemoveListener(ProcessMenuClicked);
        _game.Pause -= Enable;
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
