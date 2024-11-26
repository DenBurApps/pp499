using System;
using UnityEngine;

public class PuzzleToSolvePieceHolder : MonoBehaviour
{
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _unselectedColor;
    [SerializeField] private PuzzleSlot[] _puzzleSlots;

    public event Action OnePiecePlaced;
    
    private void Start()
    {
        foreach (var slot in _puzzleSlots)
        {
            slot.gameObject.SetActive(true);
            slot.SetImageColor(_unselectedColor);
            slot.PlacedRight += ProcessPieceSelectedCorrectly;
        }
    }

    private void OnDisable()
    {
        foreach (var slot in _puzzleSlots)
        {
            slot.SetImageColor(_unselectedColor);
            slot.PlacedRight -= ProcessPieceSelectedCorrectly;
            slot.gameObject.SetActive(false);
        }
    }

    private void ProcessPieceSelectedCorrectly(PuzzleSlot puzzleSlot)
    {
        puzzleSlot.SetImageColor(_selectedColor);
        OnePiecePlaced?.Invoke();
    }
}
