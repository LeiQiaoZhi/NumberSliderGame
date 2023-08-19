using UnityEngine;

[CreateAssetMenu(fileName = "UI Config", menuName = "UIConfig", order = 1)]
public class UIConfig : ScriptableObject
{
    public bool pause;
    public bool editor;
    public bool score;
    public bool health;
}