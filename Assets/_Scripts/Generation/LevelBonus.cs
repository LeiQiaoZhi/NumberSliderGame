using UnityEngine;

public abstract class LevelBonus : ScriptableObject
{
    public string description;

    public abstract void CheckCondition(InfiniteGridSystem _gridSystem);
}