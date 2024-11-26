using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Collider2D))]
public class Basket : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    
    private Vector2 _previousTouchPosition;
    private RectTransform _rectTransform;
    
    public event Action<InteractableObject> FruitCatched;
    public event Action BombCatched;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _previousTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 currentTouchPosition = touch.position;
                Vector2 moveDelta = currentTouchPosition - _previousTouchPosition;

                Vector3 newPosition = _rectTransform.anchoredPosition;
                newPosition.x += moveDelta.x * _speed;

                newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);

                _rectTransform.anchoredPosition = newPosition;

                _previousTouchPosition = currentTouchPosition;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractable interactable))
        {
            if (interactable is Bomb)
            {
                BombCatched?.Invoke();
            }
            else if(interactable is Fruit)
            {
                FruitCatched?.Invoke((InteractableObject)interactable);
            }
        }
    }
}
