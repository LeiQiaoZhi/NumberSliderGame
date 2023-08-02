using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject levelEndScreen;
    [SerializeField] GameObject pauseScreen;

    private void Awake()
    {
        SetEnableGameOverScreen(false);
        SetEnableLevelEndScreen(false);
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
            XLogger.Log(Category.Achievement,$"Achivement {achievementManager.achievementNames[_i]} is already unlocked");
            return;
        }
        achievementManager.UnlockAchievement(_i);
        MessageManager.instance.DisplayMessage($"Achievement Unlock: {achievementManager.achievementNames[_i].ToUpper()}");
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
  }
