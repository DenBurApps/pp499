using UnityEngine;

public class ColorBoxSpriteProvider : MonoBehaviour
{
    [SerializeField] private Sprite _purple;
    [SerializeField] private Sprite _blue;
    [SerializeField] private Sprite _orange;
    [SerializeField] private Sprite _yellow;
    [SerializeField] private Sprite _red;
    [SerializeField] private Sprite _green;

    public Sprite GetColorSprite(ColorStates colorState)
    {
        switch (colorState)
        {
            case ColorStates.Purple:
                return _purple;

            case ColorStates.Blue:
                return _blue;
            
            case ColorStates.Orange:
                return _orange;
            
            case ColorStates.Yellow:
                return _yellow;
            
            case ColorStates.Red:
                return _red;
            
            case ColorStates.Green:
                return _green;
        }

        return null;
    }
}
