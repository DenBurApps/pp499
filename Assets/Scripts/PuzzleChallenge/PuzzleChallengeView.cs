using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScreenVisabilityHandler))]
public class PuzzleChallengeView : MonoBehaviour
{
   private const string SecondsText = " seconds";
   
   [SerializeField] private Image _timerImage;
   [SerializeField] private Sprite _defaultTimerSprite;
   [SerializeField] private Sprite _changedTimerSprite;
   [SerializeField] private TMP_Text _timeText;
   [SerializeField] private Button _pauseButton;

   [SerializeField] private Image[] _puzzleImages;

   private ScreenVisabilityHandler _screenVisabilityHandler;
   
   public event Action PauseClicked;
   
   private void Awake()
   {
      _screenVisabilityHandler = GetComponent<ScreenVisabilityHandler>();
   }

   private void Start()
   {
      DisableAllPuzleImages();
   }

   private void OnEnable()
   {
      _pauseButton.onClick.AddListener(ProcessPauseClicked);
   }

   private void OnDisable()
   {
      _pauseButton.onClick.RemoveListener(ProcessPauseClicked);
   }
   
   public void SetDefaultTimerSprite()
   {
      _timerImage.sprite = _defaultTimerSprite;
   }

   public void SetChangedTimerSprite()
   {
      _timerImage.sprite = _changedTimerSprite;
   }

   public void Enable()
   {
      _screenVisabilityHandler.EnableScreen();
   }

   public void Disable()
   {
      _screenVisabilityHandler.DisableScreen();
   }

   public void ActivatePuzzleImage(int index)
   {
      _puzzleImages[index].gameObject.SetActive(true);
   }

   public void SetTimerText(float timer)
   {
      _timeText.text = timer.ToString() + SecondsText;
   }

   public void SetTransperent()
   {
      _screenVisabilityHandler.SetTransperentScreen();
   }

   public void DisableAllPuzleImages()
   {
      foreach (var puzzle in _puzzleImages)
      {
         puzzle.gameObject.SetActive(false);
      }
   }

   private void ProcessPauseClicked()
   {
      PauseClicked?.Invoke();
   }
   
} 
