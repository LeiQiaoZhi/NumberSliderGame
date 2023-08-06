using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Theme", menuName = "Color/ColorTheme", order = 0)]
public class ColorTheme : ScriptableObject
{
    public enum ThemeName
    {
        Debug,
        SereneSkies,
        PastelRetreat,
        MorningHush,
        CoastalBreeze
    }
    public ThemeName themeName;
    public List<ColorItem> colorItems;
}