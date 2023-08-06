using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class NumberGridGenerator : MonoBehaviour
{
    [Header("References")] public InfiniteGridSystem infGridSystem;

    private World world_;
    private ColorPreset colorPreset_;

    private Vector2Int currentPatch_ = new(0, 0);
    private List<Vector2Int> portalPositions_ = new();
    private GameStates gameStates_;

    /// generate the first patch
    public void StartingGeneration()
    {
        infGridSystem.CalculateCellDimension();
        GeneratePatch(currentPatch_, world_.startingGenerationStrategy);
    }

    /// test whether new patch needs to be generated
    public void OnPlayerMove(Vector2Int _playerPosition)
    {
        Vector2Int scanHalfDimension = infGridSystem.visibleAreaDimension / 2 + Vector2Int.one * 2;
        // scan in 8 directions
        for (var x = -1; x <= 1; ++x)
        {
            for (var y = -1; y <= 1; ++y)
            {
                var scanPositionGrid = new Vector2Int(_playerPosition.x + x * scanHalfDimension.x,
                    _playerPosition.y + y * scanHalfDimension.y);
                Vector2Int scanPositionPatch = infGridSystem.GridToPatchPosition(scanPositionGrid);
                if (!infGridSystem.IsPatchCreated(scanPositionPatch))
                {
                    PatchGenerationStrategy strategy = world_.GetStrategy(scanPositionPatch, portalPositions_);
                    GeneratePatch(scanPositionPatch, strategy);
                    if (world_.IsPortalStrategy(strategy))
                        portalPositions_.Add(scanPositionPatch);
                }
            }
        }
    }

    private void GeneratePatch(Vector2Int _patchPosition, PatchGenerationStrategy _patchGenerationStrategy)
    {
        currentPatch_ = _patchPosition;
        var cells = infGridSystem.CreatePatch(_patchPosition);
        var numberCells = new List<NumberCell>();
        foreach (Cell cell in cells)
        {
            var numberCell = cell.AddComponent<NumberCell>();
            numberCell.Init(cell, colorPreset_);
            numberCells.Add(numberCell);
        }

        // apply patch generation strategy
        _patchGenerationStrategy.Init(numberCells, infGridSystem.patchDimension);
        _patchGenerationStrategy.Generate();
    }

    public NumberCell GetCell(int _x, int _y)
    {
        Vector2Int patchPosition = infGridSystem.GridToPatchPosition(_x, _y);
        Vector2Int cellPosition = infGridSystem.GridToCellInPatchPosition(_x, _y);

        Cell cell = infGridSystem.GetCell(patchPosition, cellPosition);
        return cell == null ? null : cell.GetComponent<NumberCell>();
    }

    // overload
    public NumberCell GetCell(Vector2Int _position)
    {
        return GetCell(_position.x, _position.y);
    }

    public int GetPatchWidth()
    {
        return infGridSystem.patchDimension.x;
    }

    public int GetPatchHeight()
    {
        return infGridSystem.patchDimension.y;
    }


    public void Init(ColorPreset _colorPreset, World _world, GameStates _gameStates)
    {
        colorPreset_ = _colorPreset;
        world_ = _world;
        gameStates_ = _gameStates;
    }
}