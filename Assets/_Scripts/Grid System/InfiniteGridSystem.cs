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
}