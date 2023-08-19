using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

[System.Serializable]
public class MenuItem
{
    public MenuCellConfig config;
    public Vector2Int relativePosition;
}

[CreateAssetMenu(fileName = "Main Menu Generation Strategy", menuName = "Generation/MainMenu",
    order = 0)]
public class MainMenuGenerationStrategy : PatchGenerationStrategy
{
    public List<MenuItem> menuItems;

    public override void Generate()
    {
        Vector2Int center = patchDimension / 2;
        GetCell(center.x, center.y).SetVisited();
        var cornerCells = new List<NumberCell>
        {
            GetCell(center.x - 1, center.y - 1),
            GetCell(center.x - 1, center.y + 1),
            GetCell(center.x + 1, center.y - 1),
            GetCell(center.x + 1, center.y + 1)
        };
        foreach (var cell in cornerCells)
        {
            cell.SetInActive();
        }

        foreach (MenuItem item in menuItems)
        {
            NumberCell cell = GetCell(center.x + item.relativePosition.x, center.y + item.relativePosition.y);
            var menuCell = cell.AddComponent<MenuCell>();
            menuCell.SetUp(item.config, cell);
        }
    }
}