using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBoxStateProvider : MonoBehaviour
{
    private readonly ColorStates[] _allColorStates = (ColorStates[])System.Enum.GetValues(typeof(ColorStates));

    public List<ColorStates> GetRandomColorsStates(int count)
    {
        List<ColorStates> selectedColorStates = new List<ColorStates>();
        
        while (selectedColorStates.Count < count)
        {
            ColorStates colorState = _allColorStates[Random.Range(0, _allColorStates.Length)];
            
            if (!selectedColorStates.Contains(colorState))
            {
                selectedColorStates.Add(colorState);
            }
        }
        
        ShuffleList(selectedColorStates);

        return selectedColorStates;
    }
    
    private void ShuffleList(List<ColorStates> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ColorStates temp = list[i];
            int randomIndex = Random.Range(0, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        } 
    }
}

public enum ColorStates
{
    Purple,
    Blue,
    Orange,
    Yellow,
    Red,
    Green
}
