using System;
using UnityEngine;

public class ScoreManger : MonoBehaviour
{
    public float scoreMultiplier = 1f;
    public int portalScore = 100;

    public delegate void ScoreChange(int _score);

    public static event ScoreChange OnScoreChange;
    private static ScoreManger Instance { get; set; }

    private int score_ = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
        {
            ResetScore();
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnMovementScore;
        PlayerMovement.OnGameOver += OnGameOver;
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameRestart += ResetScore;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnMovementScore;
        PlayerMovement.OnGameOver -= OnGameOver;
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameRestart -= ResetScore;
    }

    private void OnGameOver(Vector2Int _direction)
    {
        XLogger.Log(Category.Score, $"Game Over, score: {score_}");
        ResetScore();
    }

    private void ResetScore()
    {
        score_ = 0;
        OnScoreChange?.Invoke(score_);
    }

    private void OnGameStart()
    {
        OnScoreChange?.Invoke(score_);
    }

    private void OnMovementScore(PlayerMovement.MergeResult _mergeResult)
    {
        switch (_mergeResult.type)
        {
            case PlayerMovement.MergeType.Fail:
                break;
            case PlayerMovement.MergeType.Minus:
                AddScore(_mergeResult.other);
                break;
            case PlayerMovement.MergeType.Divide:
                AddScore(_mergeResult.result);
                break;
            case PlayerMovement.MergeType.PlusOne:
                AddScore(1);
                break;
            default:
                break;
        }
    }

    private void AddScore(int _score)
    {
        if (_score == 0) return;
        score_ += Mathf.RoundToInt(_score * scoreMultiplier);
        OnScoreChange?.Invoke(score_);
    }


    public void AddPortalScore()
    {
        AddScore(portalScore);
    }
}