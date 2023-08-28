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
        foreach (NumberCell cell in numberCells)
        {
            cell.SetTextColor(new Color(0,0,0,0));
            // cell.SetColor(new Color(1,1,1,1));
            cell.isStatic = true;
        }

        foreach (MenuItem item in menuItems)
        {
            NumberCell cell = GetCell(center.x + item.relativePosition.x, center.y + item.relativePosition.y);
            var menuCell = cell.AddComponent<MenuCell>();
            menuCell.SetUp(item.config, cell);
        }
    }
}