using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NumberGridGenerator : MonoBehaviour
{
    [Header("Generation")] public PatchGenerationStrategy patchGenerationStrategy;
    [Header("References")] public InfiniteGridSystem infGridSystem;
    private Tuple<int, int> currentPatch_ = new(0, 0);

    public void StartingGeneration()
    {
        infGridSystem.CalculateCellDimension();
        GeneratePatch(currentPatch_);
        // test
        GeneratePatch(new Tuple<int, int>(0, 1));
        GeneratePatch(new Tuple<int, int>(1, 0));
        GeneratePatch(new Tuple<int, int>(1, 1));
        GeneratePatch(new Tuple<int, int>(-1, 0));
        GeneratePatch(new Tuple<int, int>(-1, -1));
        GeneratePatch(new Tuple<int, int>(0, -1));
        GeneratePatch(new Tuple<int, int>(1, -1));
        GeneratePatch(new Tuple<int, int>(-1, 1));
    }

    private void GeneratePatch(Tuple<int, int> _patchPosition)
    {
        currentPatch_ = _patchPosition;
        var cells = infGridSystem.CreatePatch(_patchPosition);
        var numberCells = new List<NumberCell>();
        foreach (Cell cell in cells)
        {
            var numberCell = cell.AddComponent<NumberCell>();
            numberCell.Init(cell);
            numberCells.Add(numberCell);
        }
        // apply patch generation strategy
        patchGenerationStrategy.GetData(numberCells, infGridSystem.patchDimension);
        patchGenerationStrategy.Generate();
    }

    public NumberCell GetCell(int _x, int _y)
    {
        var patchPosition = infGridSystem.GridToPatchPosition(_x, _y);
        var cellPosition = infGridSystem.GridToCellInPatchPosition(_x, _y);

        Cell cell = infGridSystem.GetCell(patchPosition, cellPosition);
        return cell == null ? null : cell.GetComponent<NumberCell>();
    }


    public NumberCell GetCell(Tuple<int, int> _position)
    {
        return GetCell(_position.Item1, _position.Item2);
    }

    public int GetPatchWidth()
    {
        return infGridSystem.patchDimension.x;
    }

    public int GetPatchHeight()
    {
        return infGridSystem.patchDimension.y;
    }
}