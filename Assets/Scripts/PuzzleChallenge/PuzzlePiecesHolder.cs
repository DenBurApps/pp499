using UnityEngine;

public class PuzzlePiecesHolder : MonoBehaviour
{
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private PuzzlePiece[] _puzzlePieces;

    public void OnEnable()
    {
        foreach (var piece in _puzzlePieces)
        {
            piece.gameObject.SetActive(true);
            piece.SetParentCanvas(_parentCanvas);
            piece.Placed += () => piece.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        foreach (var piece in _puzzlePieces)
        {
            piece.gameObject.SetActive(false);
            piece.Placed -= () => piece.gameObject.SetActive(false);
        }
    }
}
