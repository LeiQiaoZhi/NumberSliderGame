using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public GameObject levelEditorPanel;
    public TMP_InputField inputField;
    public PredefinedGenerationStrategy predefinedGenerationStrategy;

    private void Start()
    {
        levelEditorPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += ExtractLevelStr;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= ExtractLevelStr;
    }

    private void ExtractLevelStr()
    {
        var level = predefinedGenerationStrategy.ParseLevelStr(predefinedGenerationStrategy.levelStr);
        inputField.text = FormatLevel(level);
    }
    private string FormatLevel(int[,] _level)
    {
        var longestCharLength = _level.Cast<int>().Max(_number => _number.ToString().Length);
        XLogger.Log("longestCharLength: " + longestCharLength);
        var levelStr = "";
        for (int y = _level.GetLength(1) - 1; y >= 0; y--)
        {
            for (int x = 0; x < _level.GetLength(0); x++)
            {
                var numberStr = _level[x, y].ToString();
                var padding = new string(' ', longestCharLength - numberStr.Length + 2);
                levelStr += numberStr + padding;
            }
            levelStr += "\n";
        }
        return levelStr;
    }

    public void ShowLevelEditor()
    {
        levelEditorPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }
    
    public void HideLevelEditor()
    {
        levelEditorPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void SetLevel()
    {
        var level = inputField.text;
        predefinedGenerationStrategy.levelStr = level;
        SceneLoader.Instance.ReloadScene();
    }
    
}
