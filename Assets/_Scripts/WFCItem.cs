using UnityEngine;



[CreateAssetMenu(fileName = "WFC Item Basic", menuName = "WFCItem/Basic")]
[System.Serializable]
public class WFCItem : ScriptableObject
{
    public Sprite image;
    [Header("Rules")]
    public Rule topRule;
    public Rule leftRule;
    public Rule rightRule;
    public Rule downRule;
}