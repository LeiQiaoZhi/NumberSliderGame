using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// ReSharper disable Unity.InefficientPropertyAccess

public class GridWorldPosSetter : MonoBehaviour
{
    public RefPoint refPointType;
    [Space(10)] public YConstraint yConstraint;
    public float yOffset;
    [Space(10)] public Vector2 gridPos; // of center, relative to player position
    public Vector2 size; // relative to cell size
    [Space(10)] public bool useCustomGridDimension;
    public Vector2Int customGridDimension;

    public void SetPosition(InfiniteGridSystem _gridSystem)
    {
        Vector2 cellDimension = useCustomGridDimension
            ? GameUtils.CalculateCellDimension(Camera.main, _gridSystem.margin, customGridDimension)
            : _gridSystem.GetCellDimension();

        XLogger.Log("cellDimension: " + cellDimension);
        transform.localScale = new Vector3(cellDimension.x * size.x, cellDimension.y * size.y, 1);
        Vector3 refPos = GetRefPointWorldPos(refPointType, _gridSystem);
        XLogger.Log($"{refPointType}:  {refPos}");
        transform.position = refPos + new Vector3(cellDimension.x * gridPos.x, cellDimension.y * gridPos.y, 1);

        ApplyYConstraint(cellDimension, refPos, _gridSystem);
    }

    private void ApplyYConstraint(Vector2 _cellDimension, Vector2 _refPos, InfiniteGridSystem _gridSystem)
    {
        transform.position = yConstraint switch
        {
            YConstraint.AboveGrid
                => new Vector3(transform.position.x,
                    _refPos.y + _gridSystem.GetCellDimension().y * _gridSystem.GetVisibleAreaDimension().y +
                    _cellDimension.y * yOffset, transform.position.z),
            YConstraint.BelowGrid
                => new Vector3(transform.position.x, _refPos.y - _cellDimension.y * yOffset,
                    transform.position.z),
            _ =>
                transform.position
        };
    }

    private Vector2 GetRefPointWorldPos(RefPoint _refPoint, InfiniteGridSystem _gridSystem)
    {
        Vector2 visibleToPatchHalfDimension =
            (Vector2)(_gridSystem.GetPatchDimension() - _gridSystem.GetVisibleAreaDimension()) / 2.0f;
        if (_refPoint == RefPoint.BottomLeft)
            return _gridSystem.GridToWorldPosition((int)visibleToPatchHalfDimension.x,
                       (int)visibleToPatchHalfDimension.y)
                   - (Vector3)_gridSystem.GetCellDimension() / 2.0f;
        else
            return _gridSystem.GridToWorldPosition(_gridSystem.GetPatchDimension().x - 1 - (int) visibleToPatchHalfDimension.x,
                       (int)visibleToPatchHalfDimension.y)
                   + new Vector3(_gridSystem.GetCellDimension().x / 2.0f, -_gridSystem.GetCellDimension().y / 2.0f, 0);
    }
}

public enum YConstraint
{
    None,
    AboveGrid,
    BelowGrid,
}

public enum RefPoint
{
    BottomLeft,
    BottomRight,
}