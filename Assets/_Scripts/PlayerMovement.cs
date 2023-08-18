using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Event")] [SerializeField] private GameEvent gameOverEvent;
    [SerializeField] private GameEvent enterPortalEvent;
    private PlayerHealth playerHealth_;

    public enum MergeType
    {
        Fail,
        Minus,
        Divide,
        PlusOne,
        Start
    }

    public class MergeResult
    {
        public MergeType type;
        public int result;
        public int original;
        public int other;
        public Transform targetTransform;
    }

    public delegate void PlayerInvalidMove(Vector2Int _direction);

    public static event PlayerInvalidMove OnPlayerInvalidMove;

    public delegate void PlayerMove(MergeResult _targetCell);

    public static event PlayerMove OnPlayerMove;

    public delegate void GameOver(Vector2Int _direction);

    public static event GameOver OnGameOver;


    private NumberGridGenerator numberGridGenerator_;
    private GameStates gameStates_;

    // note this position may exceed or go below dimensions of a patch
    // because it uses grid coordinates, not patch coordinates
    private Vector2Int position_;

    public void OnGenerationStart(NumberGridGenerator _numberGridGenerator, GameStates _gameStates)
    {
        playerHealth_ = GetComponent<PlayerHealth>();
        numberGridGenerator_ = _numberGridGenerator;
        gameStates_ = _gameStates;

        // the center patch
        numberGridGenerator_.StartingGeneration();
        // set player cell
        position_ = new Vector2Int(numberGridGenerator_.GetPatchWidth() / 2,
            numberGridGenerator_.GetPatchHeight() / 2);
        numberGridGenerator_.GetCell(position_).SetNumber(1);
        numberGridGenerator_.GetCell(position_).SetPlayerCell();
        OnPlayerMove?.Invoke(new MergeResult
        {
            type = MergeType.Start,
            result = 1,
            original = 0,
            other = 0,
            targetTransform = numberGridGenerator_.GetCell(position_).transform
        });
        // generate surrounding patches
        numberGridGenerator_.OnPlayerMove(position_);
    }

    public void Move(Vector2Int _direction)
    {
        if (!gameStates_.IsPlaying())
            return;

        NumberCell currentCell = numberGridGenerator_.GetCell(position_);
        var targetPosition = new Vector2Int(position_.x + _direction.x, position_.y + _direction.y);
        NumberCell targetCell = numberGridGenerator_.GetCell(targetPosition);
        if (targetCell == null)
        {
            // border of a fixed patch
            XLogger.LogWarning(Category.Movement, $"target cell {targetPosition} is null");
            OnPlayerInvalidMove?.Invoke(_direction);
            return;
        }

        if (!targetCell.IsActive())
        {
            OnPlayerInvalidMove?.Invoke(_direction);
            return;
        }

        // Merge
        if (Merge(currentCell, targetCell, targetPosition))
            return;

        // can't merge, reduce health
        playerHealth_.ChangeHealth(-1);
        if (playerHealth_.GetHealth() > 0)
        {
            OnPlayerInvalidMove?.Invoke(_direction);
            if (!targetCell.isPortal)
                targetCell.SetVisited();
        }
        else // Game Over
        {
            XLogger.LogWarning(Category.Movement, $"invalid merge, game over");
            gameStates_.state = GameStates.GameState.Over;
            gameOverEvent.Raise();
            OnGameOver?.Invoke(_direction);
            gameObject.SetActive(false);
        }
    }

    private bool Merge(NumberCell _currentCell, NumberCell _targetCell, Vector2Int _targetPosition)
    {
        MergeResult mergeResult = GetMergeResult(_currentCell, _targetCell);
        if (mergeResult.type == MergeType.Fail) return false;
        // valid merge, player moves to target cell
        _currentCell.SetVisited();
        position_ = _targetPosition;
        _targetCell.SetNumber(mergeResult.result);
        _targetCell.SetPlayerCell();
        // test whether new patch needs to be generated
        numberGridGenerator_.OnPlayerMove(position_);
        mergeResult.targetTransform = _targetCell.transform;
        OnPlayerMove?.Invoke(mergeResult);
        // test whether the cell is a portal
        if (_targetCell.isPortal)
        {
            XLogger.Log(Category.Movement, $"Player enters portal {_targetPosition}");
            enterPortalEvent.Raise();
            gameStates_.state = GameStates.GameState.Pause;
        }

        return true;
    }

    private MergeResult GetMergeResult(NumberCell _currentCell, NumberCell _targetCell)
    {
        var mergeResult = new MergeResult();
        var currentNumber = _currentCell.GetNumber();
        mergeResult.original = currentNumber;
        var targetNumber = _targetCell.GetNumber();
        mergeResult.other = targetNumber;
        if (_targetCell.IsVisited())
        {
            mergeResult.result = currentNumber + 1;
            mergeResult.type = MergeType.PlusOne;
        }
        else if (currentNumber > targetNumber)
        {
            mergeResult.result = currentNumber - targetNumber;
            mergeResult.type = MergeType.Minus;
        }
        else if (targetNumber % currentNumber == 0)
        {
            mergeResult.result = targetNumber / currentNumber;
            mergeResult.type = MergeType.Divide;
        }
        else
            mergeResult.type = MergeType.Fail;

        return mergeResult;
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