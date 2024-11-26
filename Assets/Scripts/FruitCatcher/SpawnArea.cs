using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpawnArea : MonoBehaviour
{
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _yPosition;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public Vector2 GetRandomXPositionToSpawn()
    {
        float randomX = Random.Range(_minX, _maxX);
        
        return new Vector2(randomX, _yPosition);
    }
}
