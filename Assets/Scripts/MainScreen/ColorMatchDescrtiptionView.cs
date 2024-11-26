using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatchDescrtiptionView : GameDescriptionView
{
    [SerializeField] private MainScreen _mainScreen;

    private void OnEnable()
    {
        _mainScreen.OpenColorMatchDescription += EnableScreen;
        _startGameButton.onClick.AddListener(OpenGame);
        _backButton.onClick.AddListener(ProcessBackButtonClicked);
    }

    private void OnDisable()
    {
        _mainScreen.OpenColorMatchDescription -= EnableScreen;
        _startGameButton.onClick.RemoveListener(OpenGame);
        _backButton.onClick.RemoveListener(ProcessBackButtonClicked);
    }
}
