using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Main Menu Generation Strategy", menuName = "Generation/MainMenu",
    order = 0)]
public class MainMenuGenerationStrategy : PatchGenerationStrategy
{
    public override void Generate()
    {
        Vector2Int center = patchDimension / 2;
        foreach (var cell in numberCells)
        {
            cell.SetTransparent();
        }
    }
}