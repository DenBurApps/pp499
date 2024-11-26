using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PuzzleSlot : MonoBehaviour, IDropHandler
{
    private Image _image;
    private bool _isActive;

    public event Action<PuzzleSlot> PlacedRight;
    
    private void OnEnable()
    {
        _isActive = false;
        
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            VerifyPuzzlePiece(eventData.pointerDrag.GetComponent<PuzzlePiece>());
        }
    }

    public void SetImageColor(Color color)
    {
        _image.color = color;
    }

    private void VerifyPuzzlePiece(PuzzlePiece piece)
    {
        if(_isActive)
            return;
        
        if (piece.Image.sprite == _image.sprite)
        {
           piece.Place();
           PlacedRight?.Invoke(this);
           _isActive = true;
           _image.raycastTarget = false;
        }
    }
}
