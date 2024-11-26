using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class PuzzleChallengeSelection : MonoBehaviour
{
    [SerializeField] private Button _firstPuzzle;
    [SerializeField] private Button _secondPuzzle;
    [SerializeField] private Button _thirdPuzzle;
    [SerializeField] private Button _fourthPuzzle;
    [SerializeField] private PuzzleChallengeGame _game;
    
    private ScreenVisabilityHandler _screenVisabilityHandler;

    public event Action FirstPuzzleSelected;
    public event Action SecondPuzzleSelected;
    public event Action ThirdPuzzleSelected;
    public event Action FourthPuzzleSelected;
    
    private void Awake()
    {
        _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
    }

    private void Start()
    {
        Enable();
    }

    private void OnEnable()
    {
        _firstPuzzle.onClick.AddListener(ProcessFirstPuzzleOpened);
        _secondPuzzle.onClick.AddListener(ProcessSecondPuzzleSelected);
        _thirdPuzzle.onClick.AddListener(ProcessThirdPuzzleSelected);
        _fourthPuzzle.onClick.AddListener(ProcessFourthPuzzleSelected);

        _game.BackToMenu += Enable;
    }

    private void OnDisable()
    {
        _firstPuzzle.onClick.RemoveListener(ProcessFirstPuzzleOpened);
        _secondPuzzle.onClick.RemoveListener(ProcessSecondPuzzleSelected);
        _thirdPuzzle.onClick.RemoveListener(ProcessThirdPuzzleSelected);
        _fourthPuzzle.onClick.RemoveListener(ProcessFourthPuzzleSelected);
        
        _game.BackToMenu -= Enable;
    }

    private void Enable()
    {
        _screenVisabilityHandler.EnableScreen();
    }

    private void Disable()
    {
        _screenVisabilityHandler.DisableScreen();
    }

    private void ProcessFirstPuzzleOpened()
    {
        FirstPuzzleSelected?.Invoke();
        Disable();
    }

    private void ProcessSecondPuzzleSelected()
    {
        SecondPuzzleSelected?.Invoke();
        Disable();
    }

    private void ProcessThirdPuzzleSelected()
    {
        ThirdPuzzleSelected?.Invoke();
        Disable();
    }

    private void ProcessFourthPuzzleSelected()
    {
        FourthPuzzleSelected?.Invoke();
        Disable();
    }
}
