using System;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGridSystem : MonoBehaviour
{
    public float margin = 0.05f;
    public GameObject cellPrefab;
    [Header("Game Events")] public GameEvent cellDimensionChangedEvent;
    public delegate void CellDimensionChanged();
    public static event CellDimensionChanged OnCellDimensionChanged;

    private Vector2Int patchDimension_;
    private Vector2Int screenAreaDimension_;
    private Vector2Int visibleAreaDimension_;

    private Dictionary<Vector2Int, Patch> patches_ = new();
    private Vector2Int currentPatch_ = new(0, 0);

    private Vector2 cellDimension_;

    public void InitDimensions(World _world)
    {
        patchDimension_ = _world.patchDimension;
        screenAreaDimension_ = _world.screenAreaDimension;
        visibleAreaDimension_ = _world.visibleAreaDimension;
    }
    
    public List<Cell> CreatePatch(Vector2Int _patchCoord)
    {
        var patchObject = new GameObject($"Patch {_patchCoord}");
        patchObject.transform.SetParent(transform);
        var patch = patchObject.AddComponent<Patch>();
        patch.Init(patchDimension_.x, patchDimension_.y, cellDimension_, this);

        // set patch's position
        var patchPos = new Vector2(
            _patchCoord.x * patchDimension_.x * cellDimension_.x,
            _patchCoord.y * patchDimension_.y * cellDimension_.y
        );
        patchObject.transform.position = patchPos;
        patches_.Add(_patchCoord, patch);
        return patch.CreateCells();
    }

    public void CalculateCellDimension()
    {
        // find suitable world cell width and height for the grid
        cellDimension_ = GameUtils.CalculateCellDimension(Camera.main, margin, screenAreaDimension_);
        cellDimensionChangedEvent.Raise();
        OnCellDimensionChanged?.Invoke();
    }
    
    public Patch GetPatch(Vector2Int _patchPosition)
    {
        if (!patches_.ContainsKey(_patchPosition))
        {
            XLogger.LogWarning(Category.GridSystem, $"patch {_patchPosition} not found");
            return null;
        }
        return patches_[_patchPosition];
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
        var patchX = _x < 0 ? (_x + 1) / patchDimension_.x - 1 : _x / patchDimension_.x;
        var patchY = _y < 0 ? (_y + 1) / patchDimension_.y - 1 : _y / patchDimension_.y;
        return new Vector2Int(patchX, patchY);
    }

    // overload
    public Vector2Int GridToPatchPosition(Vector2Int _gridPosition)
    {
        return GridToPatchPosition(_gridPosition.x, _gridPosition.y);
    }

    public Vector2Int GridToCellInPatchPosition(int _x, int _y)
    {
        _x %= patchDimension_.x;
        _y %= patchDimension_.y;
        if (_x < 0) _x += patchDimension_.x;
        if (_y < 0) _y += patchDimension_.y;
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


    public Vector2Int GetSreenAreaDimension()
    {
        return screenAreaDimension_;
    }

    public Vector2Int GetPatchDimension()
    {
        return patchDimension_;
    }

    public Vector2Int GetVisibleAreaDimension()
    {
        return visibleAreaDimension_;
    }

    public Patch GetPatch(int _x, int _y)
    {
        return GetPatch(new Vector2Int(_x, _y));
    }
}