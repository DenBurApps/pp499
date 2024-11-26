using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MainMenuView _view;
    [SerializeField] private ColorMatchGame _game;

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
