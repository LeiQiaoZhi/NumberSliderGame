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
            score_ = 0;
            Instance = this;
        }
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
        OnScoreChange?.Invoke(score_);
    }

    private void MovementScore(PlayerMovement.MergeResult _mergeResult)
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
                throw new ArgumentOutOfRangeException(nameof(_mergeResult));
        }
    }

    private void AddScore(int _score)
    {
        score_ += Mathf.RoundToInt(_score * scoreMultiplier);
        OnScoreChange?.Invoke(score_);
    }
    
    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= MovementScore;
        GameManager.OnGameStart -= OnGameStart;
    }
    
    public void AddPortalScore()
    {
        AddScore(portalScore);
    }

    public void ResetScore()
    {
        score_ = 0;
    }
}