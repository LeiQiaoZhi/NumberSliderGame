using UnityEngine;

[CreateAssetMenu(fileName = "Menu Cell Config", menuName = "UI/MenuCellConfig",
    order = 0)]
public class MenuCellConfig : ScriptableObject
{
    public string text;
    public Color color;
    public GameObject overlayPrefab;
    public GameEvent visitEvent;
    public Progression progression;
}