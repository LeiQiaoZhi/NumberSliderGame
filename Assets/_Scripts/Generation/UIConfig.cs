using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "UI Config", menuName = "UIConfig", order = 1)]
public class UIConfig : ScriptableObject
{
    public bool pause;
    public bool editor;
    public bool score;
    public bool health;
    public bool gameTitle;
    [FormerlySerializedAs("bonusPanel")] public bool bonusButton;
    [Space(10)]
    public bool visibilityMaskFollowCamera = true;
}