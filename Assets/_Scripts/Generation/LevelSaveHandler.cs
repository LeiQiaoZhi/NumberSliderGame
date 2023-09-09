using UnityEngine;

public class LevelSaveHandler
{
    private const string LevelKey = "Level";

    /// 0 index
    public static void SaveLevel(int _level)
    {
        var currentLevel = LoadLevel();
        if (_level <= currentLevel)
        {
            XLogger.Log(Category.Level, $"Level {_level} is already unlocked");
            return;
        }
        XLogger.Log(Category.Level, $"Saving level to {_level}");
        ForceSetLevel(_level);
    }
    
    public static void ForceSetLevel(int _level)
    {
        PlayerPrefs.SetInt(LevelKey, _level);
    }

    public static int LoadLevel()
    {
        var level = PlayerPrefs.GetInt(LevelKey, 1);
        XLogger.Log(Category.Level, $"Loaded level {level}");
        return level;
    }
}