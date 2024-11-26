using System;
using UnityEngine;

public class FruitCatcherMainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuView _view;
    [SerializeField] private FruitCatcherGame _game;

    public event Action StartGameClicked;

    private void Start()
    {
        _view.Enable();
    }

    private void OnEnable()
    {
        _view.NewGameButtonClicked += StartGame;
        _game.BackToMenu += _view.Enable;
    }

    private void OnDisable()
    {
        _view.NewGameButtonClicked -= StartGame;
        _game.BackToMenu -= _view.Enable;
    }

    private void StartGame()
    {
        StartGameClicked?.Invoke();
        _view.Disable();
    }
}