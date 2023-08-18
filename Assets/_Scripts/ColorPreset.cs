using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ColorItem
{
    public int number;
    public Color color;
}

[CreateAssetMenu(fileName = "Color Preset", menuName = "Color/ColorPreset", order = 0)]
public class ColorPreset : ScriptableObject
{
    public Color defaultColor;
    [Header("Themes")]
    public ColorTheme.ThemeName defaultTheme;
    public List<ColorTheme> colorThemes;
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
    public Color portalTextColor;
    [Space(10)]
    public Color playerColor;
    public Color playerTextColor;

    public Color GetColor(int _number)
    {
        return GetColor(_number, defaultTheme);
    }
    public Color GetColor(int _number, ColorTheme.ThemeName _theme)
    {
        ColorTheme theme = colorThemes.Find(_x => _x.themeName == _theme);
        ColorItem item = theme.colorItems.Find(_x => _x.number == _number);
        return item?.color ?? defaultColor;
    }

    public Color GetColorViaIndex(int _index)
    {
        return GetColorViaIndex(_index, defaultTheme);
    }

    private Color GetColorViaIndex(int _index, ColorTheme.ThemeName _theme)
    {
        ColorTheme theme = colorThemes.Find(_x => _x.themeName == _theme);
        return theme.colorItems[_index % theme.colorItems.Count].color;
    }
}