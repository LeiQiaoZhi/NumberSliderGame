using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameEvent playerMoveEvent;
    
    private NumberGridGenerator numberGridGenerator_;
    // note this position may exceed or go below dimensions of a patch
    private Tuple<int, int> position_;

    private void Start()
    {
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();
        numberGridGenerator_.StartingGeneration();
        position_ = new Tuple<int, int>(numberGridGenerator_.GetPatchWidth()/2, numberGridGenerator_.GetPatchHeight()/2);
        numberGridGenerator_.GetCell(position_).SetActive();
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
        var mergeResult = Merge(currentCell, targetCell);
        if (mergeResult > 0)
        {
            // valid merge, player moves to target cell
            currentCell.SetInActive();
            position_ = targetPosition;
            targetCell.SetNumber(mergeResult);
            targetCell.SetActive();
            playerMoveEvent.Raise();
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
    
    public Tuple<int, int> GetPlayerPosition()
    {
        return position_;
    }
}