using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.SceneManagement;


// SINGLETON
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public List<int> levelsUnlockedAtTheStart;

    private void Awake()
    {
#if DEBUG
        foreach (var t in levelsUnlockedAtTheStart)
        {
            UnlockLevel(t);
        }
#else
        // don't unlock any levels
#endif

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetLevels()
    {
        for (int i = 1; i <= 3; i++)
        {
            LockLevel(i);
        }
    }

    public void LockLevel(int _i)
    {
        XLogger.Log(Category.Level,$"Level {_i} is locked");
        PlayerPrefs.SetInt($"level{_i}", 0);
    }

    public void UnlockLevel(int _i)
    {
        XLogger.Log(Category.Level,$"Level {_i} is unlocked");
        PlayerPrefs.SetInt($"level{_i}", 1);
    }

    public bool IsLevelUnlocked(int _i)
    {
        int unlocked = PlayerPrefs.GetInt($"level{_i}", 0);
        XLogger.Log(Category.Level,$"level {_i} is unlocked: {unlocked}");
        return unlocked == 1;
    }
}