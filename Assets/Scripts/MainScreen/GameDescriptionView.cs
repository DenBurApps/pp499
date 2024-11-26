using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class GameDescriptionView : MonoBehaviour
{
    [SerializeField] protected Button _backButton;
    [SerializeField] protected Button _startGameButton;

    private string _sceneToOpenName;
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action BackButtonClicked;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        DisableScreen();
    }

    protected void EnableScreen()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    private void DisableScreen()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    public void SetSceneName(string sceneName)
    {
        _sceneToOpenName = sceneName;
    }

    protected void OpenGame()
    {
        SceneManager.LoadScene(_sceneToOpenName);
    }

    protected void ProcessBackButtonClicked()
    {
        BackButtonClicked?.Invoke();
        DisableScreen();
    }
}
