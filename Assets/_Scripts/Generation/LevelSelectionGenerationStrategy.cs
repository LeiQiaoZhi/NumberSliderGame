using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Level Selection Generation Strategy", menuName = "Generation/Level Selection",
    order = 0)]
public class LevelSelectionGenerationStrategy : PatchGenerationStrategy
{
    public List<MenuItem> menuItems;

    public override void Generate()
    {
        foreach (var cell in numberCells)
        {
            cell.SetTextColor(new Color(0,0,0,0));
            cell.isStatic = true;
        }

        foreach (MenuItem item in menuItems)
        {
            NumberCell cell = GetCell(item.relativePosition.x, item.relativePosition.y);
            var menuCell = cell.AddComponent<MenuCell>();
            menuCell.SetUp(item.config, cell);
        }
    }
}