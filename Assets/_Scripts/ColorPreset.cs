using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorItem
{
    public int number;
    public Color color;
}

[CreateAssetMenu(fileName = "Color Preset", menuName = "ColorPreset", order = 0)]
public class ColorPreset : ScriptableObject
{
    public List<ColorItem> colorItems;
    public Color defaultColor;
    [Header("Wall")]
    public Color wallColor;
    public Color wallTextColor;
    [Header("Default Cell")] public Color activeColor;
    public Color activeTextColor;
    [Space(10)]
    public Color visitedColor;
    public Color visitedTextColor;
    [Space(10)]
    public Color inactiveColor;
    [Space(10)]
    public Color portalColor;
    [Space(10)]
    public Color playerColor;
    public Color playerTextColor;

    public Color GetColor(int _number)
    {
        foreach (var item in colorItems)
        {
            if (item.number == _number)
                return item.color;
        }

        return defaultColor;
    }

    public Color GetWallColor()
    {
        return wallColor;
    }
    public Color GetWallTextColor()
    {
        return wallTextColor;
    }
}