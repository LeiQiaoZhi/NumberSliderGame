using UnityEngine;

public abstract class LevelBonus : ScriptableObject
{
    [TextArea(5, 10)]
    public string description;

    public abstract bool CheckCondition(InfiniteGridSystem _gridSystem);
}