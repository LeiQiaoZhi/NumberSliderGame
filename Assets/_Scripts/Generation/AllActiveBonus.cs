using UnityEngine;

[CreateAssetMenu(fileName = "Bonus All Active", menuName = "LevelBonus/AllActiveBonus", order = 1)]
public class AllActiveBonus : LevelBonus
{
    public override bool CheckCondition(InfiniteGridSystem _gridSystem)
    {
        var allInactive = true;
        Patch patch = _gridSystem.GetPatch(0, 0);
        for (int x = 0; x < _gridSystem.GetPatchDimension().x; x++)
        {
            for (int y = 0; y < _gridSystem.GetPatchDimension().y; y++)
            {
                Cell cell = patch.GetCell(x, y);
                var numberCell = cell.GetComponent<NumberCell>();
                if (numberCell.GetComponent<MenuCell>() != null)
                    continue;
                if (!numberCell.IsActive())
                    allInactive = false;
            }
        }
        XLogger.Log(Category.LevelBonus,$"AllInActiveBonus: {allInactive}");
        return allInactive;
    }
}