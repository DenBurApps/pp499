using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainScreen : MonoBehaviour
{
    private const string ColorMatchSceneName = "ColorMatchScene";
    private const string PuzzleChallengeSceneName = "PuzzleChallengeScene";
    private const string FruitCatcherSceneName = "FruitCatcherScene";
    
    [SerializeField] private MainScreenView _view;
    [SerializeField] private ColorMatchDescrtiptionView _colorMatchDescrtiptionView;
    [SerializeField] private FruitCatcherDescriptionView _fruitCatcherDescriptionView;
    [SerializeField] private PuzzleChallengeDescriptionView _puzzleChallengeDescriptionView;
    [SerializeField] private AchievementsView _achievementsView;
    [SerializeField] private SettingsScreen _settingsScreen;
    
    public event Action OpenColorMatchDescription;
    public event Action OpenPuzzleChallengeDescription;
    public event Action OpenFruitCatcherDescription;
    public event Action AchiementsButtonClicked;
    public event Action SettingButtonClicked;

    private void Start()
    {
        _view.EnableScreen();
        
        _colorMatchDescrtiptionView.SetSceneName(ColorMatchSceneName);
        _fruitCatcherDescriptionView.SetSceneName(FruitCatcherSceneName);
        _puzzleChallengeDescriptionView.SetSceneName(PuzzleChallengeSceneName);
    }

    private void OnEnable()
    {
        _view.QuickPlayClicked += ChoseRandomGame;
        _view.FruitCatcherClicked += ProcessCatcherOpen;
        _view.PuzzleChallengeClicked += ProcessPuzzleChallengeOpen;
        _view.ColorMatchClicked += ProcessColorMatchOpen;
        _view.AchievementsButtonClicked += ProcessAchievemntsClicked;
        _view.SettingButtonClicked += ProcessSettingsClicked;
        
        _colorMatchDescrtiptionView.BackButtonClicked += _view.EnableScreen;
        _fruitCatcherDescriptionView.BackButtonClicked += _view.EnableScreen;
        _puzzleChallengeDescriptionView.BackButtonClicked += _view.EnableScreen;

        _achievementsView.MenuClicked += _view.EnableScreen;
        _settingsScreen.MenuClicked += _view.EnableScreen;
    }

    private void OnDisable()
    {
        _view.QuickPlayClicked -= ChoseRandomGame;
        _view.FruitCatcherClicked -= ProcessCatcherOpen;
        _view.PuzzleChallengeClicked -= ProcessPuzzleChallengeOpen;
        _view.ColorMatchClicked -= ProcessColorMatchOpen;
        
        _colorMatchDescrtiptionView.BackButtonClicked -= _view.EnableScreen;
        _fruitCatcherDescriptionView.BackButtonClicked -= _view.EnableScreen;
        _puzzleChallengeDescriptionView.BackButtonClicked -= _view.EnableScreen;
        
        _achievementsView.MenuClicked -= _view.EnableScreen;
        _settingsScreen.MenuClicked -= _view.EnableScreen;
    }

    private void ChoseRandomGame()
    {
        int randomNumber = Random.Range(0, 3);

        if (randomNumber == 0)
            ProcessColorMatchOpen();
        else if (randomNumber == 1)
            ProcessPuzzleChallengeOpen();
        else if (randomNumber == 2)
            ProcessCatcherOpen();
    }

    private void ProcessColorMatchOpen()
    {
        OpenColorMatchDescription?.Invoke();
        _view.DisableScreen();
    }

    private void ProcessPuzzleChallengeOpen()
    {
        OpenPuzzleChallengeDescription?.Invoke();
        _view.DisableScreen();
    }

    private void ProcessCatcherOpen()
    {
        OpenFruitCatcherDescription?.Invoke();
        _view.DisableScreen();
    }

    private void ProcessSettingsClicked()
    {
        SettingButtonClicked?.Invoke();
        _view.DisableScreen();
    }

    private void ProcessAchievemntsClicked()
    {
        AchiementsButtonClicked?.Invoke();
        _view.DisableScreen();
    }
}