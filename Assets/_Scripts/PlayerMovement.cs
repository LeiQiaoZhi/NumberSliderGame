using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [Header("Event")] 
    [SerializeField] private GameEvent gameOverEvent;
    [SerializeField] private GameEvent enterPortalEvent;
    
    // event for player movement
    public delegate void PlayerMove(NumberCell _targetCell);
    public static event PlayerMove OnPlayerMove;

    private NumberGridGenerator numberGridGenerator_;
    private GameStates gameStates_;

    // note this position may exceed or go below dimensions of a patch
    // because it uses grid coordinates, not patch coordinates
    private Vector2Int position_;

    public void OnGameStart(NumberGridGenerator _numberGridGenerator, GameStates _gameStates)
    {
        numberGridGenerator_ = _numberGridGenerator;
        gameStates_ = _gameStates;

        numberGridGenerator_.StartingGeneration();
        position_ = new Vector2Int(numberGridGenerator_.GetPatchWidth() / 2,
            numberGridGenerator_.GetPatchHeight() / 2);
        numberGridGenerator_.GetCell(position_).SetPlayerCell();
        numberGridGenerator_.GetCell(position_).SetNumber(1);
        numberGridGenerator_.OnPlayerMove(position_);
    }

    public void Move(Vector2Int _vector2Int)
    {
        if (!gameStates_.IsPlaying())
            return;

        NumberCell currentCell = numberGridGenerator_.GetCell(position_);
        var targetPosition = new Vector2Int(position_.x + _vector2Int.x, position_.y + _vector2Int.y);
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

        // invalid merge, game over
        XLogger.LogWarning(Category.Movement, $"invalid merge, game over");
        gameStates_.state = GameStates.GameState.Over;
        gameOverEvent.Raise();
        gameObject.SetActive(false);
    }

    private bool Merge(NumberCell _currentCell, NumberCell _targetCell, Vector2Int _targetPosition)
    {
        var mergeResult = GetMergeResult(_currentCell, _targetCell);
        if (mergeResult > 0)
        {
            // valid merge, player moves to target cell
            _currentCell.SetVisited();
            position_ = _targetPosition;
            _targetCell.SetNumber(mergeResult);
            _targetCell.SetPlayerCell();
            // test whether new patch needs to be generated
            numberGridGenerator_.OnPlayerMove(position_);
            OnPlayerMove?.Invoke(_targetCell);
            // test whether the cell is a portal
            if (_targetCell.isPortal)
            {
                XLogger.Log(Category.Movement, $"Player enters portal {_targetPosition}");
                enterPortalEvent.Raise();
                gameObject.SetActive(false);
            }

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

    public Vector2Int GetPlayerPosition()
    {
        return position_;
    }

    public Vector3 GetPlayerPositionWorld()
    {
        return numberGridGenerator_.infGridSystem.GridToWorldPosition(position_.x, position_.y);
    }
}