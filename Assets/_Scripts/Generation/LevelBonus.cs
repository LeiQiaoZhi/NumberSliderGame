using UnityEngine;

public abstract class LevelBonus : ScriptableObject
{
    [TextArea(5, 10)]
    public string description;

    public abstract void CheckCondition(InfiniteGridSystem _gridSystem);
}