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
    protected void Wall()
    {
        // set wall cells
        foreach (NumberCell cell in GetEdgeCells(wallHalfDimension, doorHalfDimension))
        {
            var number = wallNumberPool[Random.Range(0, wallNumberPool.Count)];
            cell.SetNumber(number);
            cell.SetColor(colorPreset.wallColor);
            cell.SetTextColor(colorPreset.wallTextColor);
        }

        // set door cells
        foreach (NumberCell cell in GetDoorCells(wallHalfDimension, doorHalfDimension))
        {
            var number = doorNumberPool[Random.Range(0, doorNumberPool.Count)];
            cell.SetNumber(number);
        }
    }

}