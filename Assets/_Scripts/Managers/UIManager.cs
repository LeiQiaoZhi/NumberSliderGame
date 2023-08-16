using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Game State")] public GameStates gameStates;

    private void Awake()
    {
        SetEnableGameOverScreen(false);
        pauseScreen.SetActive(false);
    }

    private void OnEnable()
    {
        ScoreManger.OnScoreChange += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoreManger.OnScoreChange -= UpdateScoreText;
    }

    private void UpdateScoreText(int _score)
    {
        scoreText.TweenNumber(_score, 0.4f).Play();
    }

    public void SetEnableGameOverScreen(bool _enable)
    {
        gameOverScreen.SetActive(_enable);
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        pauseButton.SetActive(false);
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        if (gameStates.state == GameStates.GameState.Over)
            return;
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        GameManager.Instance.ResumeGame();
    }
}