using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private NumberGridGenerator numberGridGenerator_;
    private Tuple<int, int> position_;

    private void Start()
    {
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();
        numberGridGenerator_.GenerateCells();
        position_ = new Tuple<int, int>(numberGridGenerator_.Width()/2, numberGridGenerator_.Height()/2);
        numberGridGenerator_.GetCell(position_).SetActive();
    }

    public void Move(Vector2Int _vector2Int)
    {
        var currentCell = numberGridGenerator_.GetCell(position_);
        NumberCell targetCell =
            numberGridGenerator_.GetCell(position_.Item1 + _vector2Int.x, position_.Item2 + _vector2Int.y);
        if (targetCell == null)
        {
            XLogger.LogWarning(Category.Movement, $"target cell is null");
            return;
        }

        if (!targetCell.IsActive())
        {
            XLogger.LogWarning(Category.Movement, $"target cell is inactive");
            return;
        }
        var mergeResult = Merge(currentCell, targetCell);
        if (mergeResult > 0)
        {
            currentCell.SetInActive();
            position_ = targetCell.GetPosition();
            targetCell.SetNumber(mergeResult);
            targetCell.SetActive();
            return;
        }
        // invalid merge
        XLogger.LogWarning(Category.Movement, $"invalid merge, game over");
        // TODO: game over
    }

    private int Merge(NumberCell _currentCell, NumberCell _targetCell)
    {
        var currentNumber = _currentCell.GetNumber();
        var targetNumber = _targetCell.GetNumber();
        if (currentNumber > targetNumber)
            return currentNumber - targetNumber;
        if (targetNumber % currentNumber == 0)
            return targetNumber / currentNumber;
        return -1;
    }
}