using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] TextMeshProUGUI scoreText;
    [Header("Game State")]
    public GameStates gameStates;

    private void Awake()
    {
        SetEnableGameOverScreen(false);
        SetEnableLevelEndScreen(false);
        Resume();
    }

    private void OnEnable()
    {
        ScoreManger.OnScoreChange += UpdateScoreText;
    }

    private void UpdateScoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }

    public void SetEnableGameOverScreen(bool _enable)
    {
        gameOverScreen.SetActive(_enable);
    }

    public void SetEnableLevelEndScreen(bool _enable)
    {
        levelEndScreen.SetActive(_enable);
    }

    public void DisplayAchievementUnlockMessage(int _i)
    {
        AchievementManager achievementManager = AchievementManager.instance;
        if (achievementManager.IsAchievementUnlocked(_i))
        {
            XLogger.Log(Category.Achievement,
                $"Achivement {achievementManager.achievementNames[_i]} is already unlocked");
            return;
        }

        achievementManager.UnlockAchievement(_i);
        MessageManager.instance.DisplayMessage(
            $"Achievement Unlock: {achievementManager.achievementNames[_i].ToUpper()}");
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        gameStates.state = GameStates.GameState.Pause;
    }

    public void Resume()
    {
        if (gameStates.state == GameStates.GameState.Over)
            return;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        gameStates.state = GameStates.GameState.Playing;
    }
}