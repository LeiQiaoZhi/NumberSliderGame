using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGridSystem : MonoBehaviour
{
    public float margin = 0.05f;
    public GameObject cellPrefab;
    public Vector2Int patchDimension = new(8, 8);
    public Vector2Int visibleAreaDimension = new(5, 5);

    private Dictionary<Tuple<int, int>, Patch> patches_ = new();
    private Tuple<int, int> currentPatch_ = new(0, 0);

    private Vector2 cellDimension_;

    private void Awake()
    {
        CalculateCellDimension();
    }

    public List<Cell> CreatePatch(Tuple<int, int> _patchCoord)
    {
        var patchObject = new GameObject($"Patch {_patchCoord}");
        patchObject.transform.SetParent(transform);
        var patch = patchObject.AddComponent<Patch>();
        patch.Init(patchDimension.x, patchDimension.y, cellDimension_, this);

        // set patch's position
        var patchPos = new Vector2(
            _patchCoord.Item1 * patchDimension.x * cellDimension_.x,
            _patchCoord.Item2 * patchDimension.y * cellDimension_.y
        );
        patchObject.transform.position = patchPos;
        patches_.Add(_patchCoord, patch);
        return patch.CreateCells();
    }

    private void CalculateCellDimension()
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
    }

    public Cell GetCell(Tuple<int, int> _patchPosition, int _x, int _y)
    {
        if (!patches_.ContainsKey(_patchPosition))
        {
            XLogger.LogWarning(Category.GridSystem, $"patch {_patchPosition} not found");
            return null;
        }

        return patches_[_patchPosition].GetCell(_x, _y);
    }
    public Tuple<int, int> GridToPatchPosition(int _x, int _y)
    {
        var patchX = _x < 0 ? (_x + 1) / patchDimension.x - 1 : _x / patchDimension.x;
        var patchY = _y < 0 ? (_y + 1) / patchDimension.y - 1 : _y / patchDimension.y;
        return new Tuple<int, int>(patchX, patchY);
    }
    public Tuple<int, int> GridToCellInPatchPosition(int _x, int _y)
    {
        _x %= patchDimension.x;
        _y %= patchDimension.y;
        if (_x < 0) _x += patchDimension.x;
        if (_y < 0) _y += patchDimension.y;
        return new Tuple<int, int>(_x, _y);
    }
    public Vector3 GridToWorldPosition(int _x, int _y)
    {
        var patchPosition = GridToPatchPosition(_x, _y);
        var cellPosition = GridToCellInPatchPosition(_x, _y);
        var cell = GetCell(patchPosition, cellPosition);
        return cell.transform.position;
    }
    public Cell GetCell(Tuple<int, int> _patchPosition, Tuple<int, int> _cellPosition)
    {
        return GetCell(_patchPosition, _cellPosition.Item1, _cellPosition.Item2);
    }
}