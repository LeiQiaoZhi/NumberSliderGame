using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NumberGridGenerator : MonoBehaviour
{
    [FormerlySerializedAs("gridSystem")] public InfiniteGridSystem infGridSystem;
    private Tuple<int, int> currentPatch_ = new(0, 0);

    public void StartingGeneration()
    {
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
        foreach (var cell in cells)
        {
            var numberCell = cell.AddComponent<NumberCell>();
            numberCell.Init(cell);
            numberCell.SetNumber(Random.Range(1, 41));
        }
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