using UnityEngine;

[CreateAssetMenu(fileName = "Bonus All Inactive", menuName = "LevelBonus/AllInactiveBonus", order = 1)]
public class AllInactiveBonus : LevelBonus
{
    public override void CheckCondition(InfiniteGridSystem _gridSystem)
    {
        bool allInactive = true;
        Patch patch = _gridSystem.GetPatch(0, 0);
        for (int x = 0; x < _gridSystem.GetPatchDimension().x; x++)
        {
            for (int y = 0; y < _gridSystem.GetPatchDimension().y; y++)
            {
                Cell cell = patch.GetCell(x, y);
                var numberCell = cell.GetComponent<NumberCell>();
                if (numberCell.isPortal)
                    continue;
                if (numberCell.IsActive())
                    allInactive = false;
            }
        }
        XLogger.Log(Category.LevelBonus,$"AllInActiveBonus: {allInactive}");
    }
}