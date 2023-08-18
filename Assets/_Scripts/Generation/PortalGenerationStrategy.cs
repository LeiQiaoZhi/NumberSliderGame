using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Portal Generation Strategy", menuName = "Generation/PortalGenerationStrategy",
    order = 0)]
public class PortalGenerationStrategy : WallGenerationStrategy
{
    // generate clusters of same number, rest are random
    public override void Generate()
    {
        // default -- all random
        foreach (NumberCell cell in numberCells)
        {
            var random = pool[Random.Range(0, pool.Count)];
            cell.SetNumber(random);
        }

        // generate surrounding wall
        Wall();
        
        // generate portal
        Vector2Int center = patchDimension / 2;
        NumberCell centerCell = GetCell(center.x, center.y);
        centerCell.SetPortal(0, colorPreset);
    }
}