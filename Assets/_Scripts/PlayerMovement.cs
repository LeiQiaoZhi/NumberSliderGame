using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Color activeColor;
    public Color activeTextColor;
    public GameEvent playerMoveEvent;

    private NumberGridGenerator numberGridGenerator_;

    // note this position may exceed or go below dimensions of a patch
    // because it uses grid coordinates, not patch coordinates
    private Tuple<int, int> position_;

    private void Start()
    {
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();
        numberGridGenerator_.StartingGeneration();
        position_ = new Tuple<int, int>(numberGridGenerator_.GetPatchWidth() / 2,
            numberGridGenerator_.GetPatchHeight() / 2);
        SetActiveCell(numberGridGenerator_.GetCell(position_));
        numberGridGenerator_.GetCell(position_).SetNumber(1);
    }

    private void SetActiveCell(NumberCell _activeCell)
    {
        _activeCell.SetColor(activeColor);
        _activeCell.SetTextColor(activeTextColor);
    }

    public void Move(Vector2Int _vector2Int)
    {
        NumberCell currentCell = numberGridGenerator_.GetCell(position_);
        var targetPosition = new Tuple<int, int>(position_.Item1 + _vector2Int.x, position_.Item2 + _vector2Int.y);
        NumberCell targetCell = numberGridGenerator_.GetCell(targetPosition);
        if (targetCell == null)
        {
            XLogger.LogWarning(Category.Movement, $"target cell {targetPosition} is null");
            return;
        }

        if (!targetCell.IsActive())
        {
            XLogger.LogWarning(Category.Movement, $"target cell {targetPosition} is inactive");
            return;
        }

        // Merge
        if (Merge(currentCell, targetCell, targetPosition))
            return;

        // invalid merge
        XLogger.LogWarning(Category.Movement, $"invalid merge, game over");
        // TODO: game over
    }

    private bool Merge(NumberCell _currentCell, NumberCell _targetCell, Tuple<int, int> _targetPosition)
    {
        var mergeResult = GetMergeResult(_currentCell, _targetCell);
        if (mergeResult > 0)
        {
            // valid merge, player moves to target cell
            _currentCell.SetVisited();
            position_ = _targetPosition;
            _targetCell.SetNumber(mergeResult);
            SetActiveCell(_targetCell);
            playerMoveEvent.Raise();
            return true;
        }

        return false;
    }

    private int GetMergeResult(NumberCell _currentCell, NumberCell _targetCell)
    {
        var currentNumber = _currentCell.GetNumber();
        var targetNumber = _targetCell.GetNumber();
        if (_targetCell.IsVisited())
            return currentNumber + 1;
        if (currentNumber > targetNumber)
            return currentNumber - targetNumber;
        if (targetNumber % currentNumber == 0)
            return targetNumber / currentNumber;
        return -1;
    }

    public Tuple<int, int> GetPlayerPosition()
    {
        return position_;
    }

    public Vector3 GetPlayerPositionWorld()
    {
        return numberGridGenerator_.infGridSystem.GridToWorldPosition(position_.Item1, position_.Item2);
    }

    private void UpdateVisibility()
    {
        var visibleHalfX = numberGridGenerator_.infGridSystem.visibleAreaDimension.x / 2;
        var visibleHalfY = numberGridGenerator_.infGridSystem.visibleAreaDimension.y / 2;
        int peripheral = 2;
        for (int x = -visibleHalfX - peripheral; x <= visibleHalfY + peripheral; x++)
        {
            for (int y = -visibleHalfY - peripheral; y <= visibleHalfY + peripheral; y++)
            {
                NumberCell cell = numberGridGenerator_.GetCell(position_.Item1 + x, position_.Item2 + y);
                if (cell == null) continue;
                if (Math.Abs(x) > visibleHalfX || Math.Abs(y) > visibleHalfY )
                    cell.SetTransparency(0.2f);
                else if (Math.Abs(x) > visibleHalfX - 1|| Math.Abs(y) > visibleHalfY-1)
                    cell.SetTransparency(0.9f);
                else
                    cell.SetTransparency(1.0f);
            }
        }
    }
}