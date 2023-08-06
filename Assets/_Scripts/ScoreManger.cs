using System;
using UnityEngine;

public class ScoreManger : MonoBehaviour
{
    public delegate void ScoreChange(int _score);
    public static event ScoreChange OnScoreChange;
    private static ScoreManger Instance { get; set; }

    private int score_ = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += MovementScore;
        GameManager.OnGameStart += OnGameStart;
    }

    private void OnGameStart()
    {
        score_ = 0;
        OnScoreChange?.Invoke(score_);
    }

    private void MovementScore(NumberCell _targetCell)
    {
        AddScore(_targetCell.GetNumber());
    }

    public void AddScore(int _score)
    {
        score_ += _score;
        OnScoreChange?.Invoke(score_);
    }

    public void ResetScore()
    {
        score_ = 0;
    }
}