using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class PuzzlePiece : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Image _image;
    private CanvasGroup _canvasGroup;
    private Canvas _parentCanvas;
    private RectTransform _rectTransform;
    private Vector2 _beforeDragPosition;

    public event Action Placed; 

    public Image Image => _image;
    
    private void OnEnable()
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
        
        if (_rectTransform == null)
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = _beforeDragPosition;
        }
        
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _beforeDragPosition = _rectTransform.anchoredPosition;
        _canvasGroup.blocksRaycasts = false;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        
        ReturnToPreviousPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_parentCanvas != null)
        {
            _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
        }
    }
    
    public void ReturnToPreviousPosition()
    {
        _rectTransform.anchoredPosition = _beforeDragPosition;
    }
    
    public void SetParentCanvas(Canvas canvas)
    {
        _parentCanvas = canvas;
    }

    public void Place()
    {
        _rectTransform.anchoredPosition = _beforeDragPosition;
        Placed?.Invoke();
    }
}
