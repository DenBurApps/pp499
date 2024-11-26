using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;

    private ScreenVisabilityHandler _screenVisabilityHandler;
    
    public event Action NewGameButtonClicked;

    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void OnEnable()
    {
        _startGameButton.onClick.AddListener(ProcessNewGameButtonCLicked);
    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveListener(ProcessNewGameButtonCLicked);
    }

    public void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    public void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessNewGameButtonCLicked()
    {
        NewGameButtonClicked?.Invoke();
    }
}