using UnityEngine;

[CreateAssetMenu(fileName = "Menu Cell Config", menuName = "UI/MenuCellConfig",
    order = 0)]
public class MenuCellConfig : ScriptableObject
{
    public string text;
    public Color textColor;
    [Space(10)]
    public Color color;
    public GameObject overlayPrefab;
    [Space(10)]
    public GameEvent visitEvent;
    public Progression progression;
}