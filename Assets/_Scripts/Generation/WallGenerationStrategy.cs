using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Wall Generation Strategy", menuName = "Generation/WallGenerationStrategy",
    order = 0)]
public class WallGenerationStrategy : PatchGenerationStrategy
{
    [Header("Wall Generation Strategy")] public List<int> wallNumberPool;
    public List<int> doorNumberPool;

    [Header("Wall Size")] [Tooltip("0 means 1 cell, 1 means 3 cells, etc.")]
    public Vector2Int doorHalfDimension;

    public Vector2Int wallHalfDimension;
    public bool wallOnPatchEdges = false;
    [Header("Color")] public ColorPreset colorPreset;

    public override void Init(List<NumberCell> _numberCells, Vector2Int _patchDimension)
    {
        base.Init(_numberCells, _patchDimension);
        if (wallOnPatchEdges)
            wallHalfDimension = patchDimension / 2;
    }

    // generate clusters of same number, rest are random
    public override void Generate()
    {
        // default -- all random
        foreach (NumberCell cell in numberCells)
        {
            var random = pool[Random.Range(0, pool.Count)];
            cell.SetNumber(random);
        }

        Wall();
    }

    // generate wall on the edges of the patch
    private void Wall()
    {
        // set wall cells
        foreach (NumberCell cell in GetEdgeCells())
        {
            var number = wallNumberPool[Random.Range(0, wallNumberPool.Count)];
            cell.SetNumber(number);
            Color wallColor = colorPreset.GetWallColor();
            Color wallTextColor = colorPreset.GetWallTextColor();
            cell.SetColor(wallColor);
            cell.SetTextColor(wallTextColor);
        }

        // set door cells
        foreach (NumberCell cell in GetDoorCells())
        {
            var number = doorNumberPool[Random.Range(0, doorNumberPool.Count)];
            cell.SetNumber(number);
        }
    }

    private List<NumberCell> GetEdgeCells()
    {
        var center = new Vector2Int(patchDimension.x / 2, patchDimension.y / 2);
        if (wallHalfDimension.x > center.x || wallHalfDimension.y > center.y)
        {
            XLogger.LogError(Category.Generation, "Wall half dimension is too big");
            return null;
        }

        var edgeCells = new List<NumberCell>();
        // top and bottom
        for (int x = -wallHalfDimension.x; x <= wallHalfDimension.x; x++)
        {
            if (Math.Abs(x) <= doorHalfDimension.x) // skip door
                continue;
            edgeCells.Add(GetCell(center.x + x, center.y - wallHalfDimension.y));
            edgeCells.Add(GetCell(center.x + x, center.y + wallHalfDimension.y));
        }

        // left and right
        for (int y = -wallHalfDimension.y + 1; y < wallHalfDimension.y; y++)
        {
            if (Math.Abs(y) <= doorHalfDimension.y) // skip door
                continue;
            edgeCells.Add(GetCell(center.x - wallHalfDimension.x, center.y + y));
            edgeCells.Add(GetCell(center.x + wallHalfDimension.x, center.y + y));
        }

        return edgeCells;
    }

    private List<NumberCell> GetDoorCells()
    {
        var center = new Vector2Int(patchDimension.x / 2, patchDimension.y / 2);
        if (wallHalfDimension.x > center.x || wallHalfDimension.y > center.y)
        {
            XLogger.LogError(Category.Generation, "Wall half dimension is too big");
            return null;
        }

        var doorCells = new List<NumberCell>();
        // top and bottom
        for (int x = -doorHalfDimension.x; x <= doorHalfDimension.x; x++)
        {
            if (Math.Abs(x) <= doorHalfDimension.x)
            {
                doorCells.Add(GetCell(center.x + x, center.y - wallHalfDimension.y));
                doorCells.Add(GetCell(center.x + x, center.y + wallHalfDimension.y));
            }
        }
        // left and right
        for (int y = -wallHalfDimension.y + 1; y < wallHalfDimension.y; y++)
        {
            if (Math.Abs(y) <= doorHalfDimension.y)
            {
                doorCells.Add(GetCell(center.x - wallHalfDimension.x, center.y + y));
                doorCells.Add(GetCell(center.x + wallHalfDimension.x, center.y + y));
            }
        }
        return doorCells;
    }
}