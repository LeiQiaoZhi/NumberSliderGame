using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Same Number Diamond GS", menuName = "Generation/SameNumberDiamond",
    order = 0)]
public class SameNumberDiamondGenerationStrategy : PatchGenerationStrategy
{
    [Header("Same Number Generation Strategy")]
    public List<int> sameNumberPool;

    [Header("Color")] public ColorPreset colorPreset;

    // generate clusters of same number, rest are random
    public override void Generate()
    {
        // default -- all random
        foreach (NumberCell cell in numberCells)
        {
            var random = pool[Random.Range(0, pool.Count)];
            cell.SetNumber(random);
        }

        var number = sameNumberPool[Random.Range(0, sameNumberPool.Count)];
        Color color = colorPreset.GetColor(number);
        foreach (NumberCell cell in GetDiamond())
        {
            cell.SetNumber(number);
            cell.SetColor(color);
        }
    }

    private List<NumberCell> GetDiamond()
    {
        var result = new List<NumberCell>();
        Vector2Int center = GetCenterCoord();
        for (var x = 0; x < patchDimension.x; x++)
        {
            if (x < patchDimension.x / 2)
            {
                result.Add(GetCell(x, center.y + x));
                result.Add(GetCell(x, center.y - x));
            }
            else
            {
                result.Add(GetCell(x, 3 * center.y - x));
                result.Add(GetCell(x, - center.y + x));
            }
        }
        return result;
    }
}