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

    private InfiniteGridSystem gridSystem_;

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
        Vector2 cellDimension = useCustomGridDimension
            ? GameUtils.CalculateCellDimension(Camera.main, gridSystem_.margin, customGridDimension)
            : gridSystem_.GetCellDimension();

        XLogger.Log("cellDimension: " + cellDimension);
        transform.localScale = new Vector3(cellDimension.x * size.x, cellDimension.y * size.y, 1);
        Vector3 refPos = GetRefPointWorldPos(refPointType);
        XLogger.Log("botLeft: " + refPos);
        transform.position = refPos + new Vector3(cellDimension.x * gridPos.x, cellDimension.y * gridPos.y, 1);

        ApplyYConstraint(cellDimension, refPos);
    }

    private void ApplyYConstraint(Vector2 _cellDimension, Vector2 _refPos)
    {
        transform.position = yConstraint switch
        {
            YConstraint.AboveGrid
                => new Vector3(transform.position.x,
                    _refPos.y + gridSystem_.GetCellDimension().y * gridSystem_.GetPatchDimension().y +
                    _cellDimension.y * yOffset, transform.position.z),
            YConstraint.BelowGrid
                => new Vector3(transform.position.x, _refPos.y - _cellDimension.y * yOffset,
                    transform.position.z),
            _ =>
                transform.position
        };
    }

    private Vector2 GetRefPointWorldPos(RefPoint _refPoint)
    {
        if (_refPoint == RefPoint.BottomLeft)
            return gridSystem_.GridToWorldPosition(0, 0) - (Vector3)gridSystem_.GetCellDimension() / 2.0f;
        else
            return gridSystem_.GridToWorldPosition(gridSystem_.GetPatchDimension().x - 1, 0) +
                   new Vector3(gridSystem_.GetCellDimension().x / 2.0f, -gridSystem_.GetCellDimension().y / 2.0f, 0);
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