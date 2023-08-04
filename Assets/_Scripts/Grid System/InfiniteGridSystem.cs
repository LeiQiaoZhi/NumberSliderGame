using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGridSystem : MonoBehaviour
{
    public float margin = 0.05f;
    public GameObject cellPrefab;
    [Header("Dimensions")]
    public Vector2Int patchDimension = new(7, 7);
    public Vector2Int visibleAreaDimension = new(5, 5);
    [Header("Game Events")]
    public GameEvent cellDimensionChangedEvent;

    private Dictionary<Vector2Int, Patch> patches_ = new();
    private Vector2Int currentPatch_ = new(0, 0);

    private Vector2 cellDimension_;

    public List<Cell> CreatePatch(Vector2Int _patchCoord)
    {
        var patchObject = new GameObject($"Patch {_patchCoord}");
        patchObject.transform.SetParent(transform);
        var patch = patchObject.AddComponent<Patch>();
        patch.Init(patchDimension.x, patchDimension.y, cellDimension_, this);

        // set patch's position
        var patchPos = new Vector2(
            _patchCoord.x * patchDimension.x * cellDimension_.x,
            _patchCoord.y * patchDimension.y * cellDimension_.y
        );
        patchObject.transform.position = patchPos;
        patches_.Add(_patchCoord, patch);
        return patch.CreateCells();
    }

    public void CalculateCellDimension()
    {
        // find suitable world cell width and height for the grid
        Vector3 botLeft = Camera.main.ViewportToWorldPoint(Vector2.zero) * (1 - 2 * margin);
        Vector3 topRight = Camera.main.ViewportToWorldPoint(Vector2.one) * (1 - 2 * margin);
        XLogger.Log(Category.GridSystem, $"bot left in world coord: {botLeft}");
        XLogger.Log(Category.GridSystem, $"top right in world coord: {topRight}");
        var cellWidth = (topRight - botLeft).x / visibleAreaDimension.x;
        var cellHeight = (topRight - botLeft).y / visibleAreaDimension.y;
        cellDimension_ = Vector2.one * Mathf.Min(cellWidth, cellHeight);
        XLogger.Log(Category.GridSystem, $"cell dimension : {cellDimension_}");
        cellDimensionChangedEvent.Raise();
    }

    public Cell GetCell(Vector2Int _patchPosition, int _x, int _y)
    {
        if (!patches_.ContainsKey(_patchPosition))
        {
            XLogger.LogWarning(Category.GridSystem, $"patch {_patchPosition} not found");
            return null;
        }
        return patches_[_patchPosition].GetCell(_x, _y);
    }
    public Vector2Int GridToPatchPosition(int _x, int _y)
    {
        var patchX = _x < 0 ? (_x + 1) / patchDimension.x - 1 : _x / patchDimension.x;
        var patchY = _y < 0 ? (_y + 1) / patchDimension.y - 1 : _y / patchDimension.y;
        return new Vector2Int(patchX, patchY);
    }
    // overload
    public Vector2Int GridToPatchPosition(Vector2Int _gridPosition)
    {
        return GridToPatchPosition(_gridPosition.x, _gridPosition.y);
    }
    public Vector2Int GridToCellInPatchPosition(int _x, int _y)
    {
        _x %= patchDimension.x;
        _y %= patchDimension.y;
        if (_x < 0) _x += patchDimension.x;
        if (_y < 0) _y += patchDimension.y;
        return new Vector2Int(_x, _y);
    }
    public Vector3 GridToWorldPosition(int _x, int _y)
    {
        Vector2Int patchPosition = GridToPatchPosition(_x, _y);
        Vector2Int cellPosition = GridToCellInPatchPosition(_x, _y);
        Cell cell = GetCell(patchPosition, cellPosition);
        return cell.transform.position;
    }
    
    // overload
    public Cell GetCell(Vector2Int _patchPosition, Vector2Int _cellPosition)
    {
        return GetCell(_patchPosition, _cellPosition.x, _cellPosition.y);
    }

    public Vector2 GetCellDimension()
    {
        return cellDimension_;
    }

    public bool IsPatchCreated(Vector2Int _patchPosition)
    {
        return patches_.ContainsKey(_patchPosition);
    }
}