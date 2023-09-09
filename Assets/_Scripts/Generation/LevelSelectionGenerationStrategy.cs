using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Level Selection Generation Strategy", menuName = "Generation/Level Selection",
    order = 0)]
public class LevelSelectionGenerationStrategy : PatchGenerationStrategy
{
    public List<MenuItem> menuItems;
    public List<MenuItem> levelItems;

    public override void Generate()
    {
        foreach (var cell in numberCells)
        {
            cell.SetTextColor(new Color(0, 0, 0, 0));
            cell.isStatic = true;
        }

        // other items such as exit cell
        foreach (MenuItem item in menuItems)
        {
            NumberCell cell = GetCell(item.relativePosition.x, item.relativePosition.y);
            var menuCell = cell.AddComponent<MenuCell>();
            menuCell.SetUp(item.config, cell);
        }

        var level = LevelSaveHandler.LoadLevel();

        // level items like L1, L2, etc.
        for (int i = 0; i < levelItems.Count; i++)
        {
            MenuItem item = levelItems[i];
            NumberCell cell = GetCell(item.relativePosition.x, item.relativePosition.y);
            var menuCell = cell.AddComponent<MenuCell>();
            var active = i + 1 <= level; // if level is 1, then L1 is active, L2 is inactive, etc.
            menuCell.SetUp(item.config, cell).SetActive(active);
            if (!active)
            {
                cell.SetColor(colorPreset.inactiveColor);
            }
        }
    }
}