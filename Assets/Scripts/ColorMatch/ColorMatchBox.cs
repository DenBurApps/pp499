using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class ColorMatchBox : MonoBehaviour
{
    private Button _button;
    private Image _image;
    [SerializeField] private Sprite _currentColorSprite;
    [SerializeField] ColorStates _currentColorState;

    public event Action<ColorMatchBox> Clicked;

    public ColorStates CurrentColorState => _currentColorState;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ProcessClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ProcessClicked);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetColorSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void SetCurrentColorState(ColorStates state)
    {
        _currentColorState = state;
    }

    private void ProcessClicked()
    {
        _button.onClick.RemoveListener(ProcessClicked);
        Clicked?.Invoke(this);
    }
}