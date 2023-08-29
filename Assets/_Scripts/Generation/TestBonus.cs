using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonus Test", menuName = "LevelBonus/TestBonus", order = 1)]
public class TestBonus : LevelBonus
{
    public override bool CheckCondition(InfiniteGridSystem _infiniteGridSystem)
    {
        XLogger.Log(Category.LevelBonus,"Checking TestBonus");
        return false;
    }
}