using System.Linq;
using TMPro;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [Header("UI")] public GameObject levelEditorPanel;
    public TMP_InputField levelInput;
    public TMP_InputField dimensionInputX;
    public TMP_InputField dimensionInputY;
    public TMP_InputField portalInput;
    [Header("Affects")] public PredefinedGenerationStrategy predefinedGenerationStrategy;
    public World fixedWorld;

    private void Start()
    {
        levelEditorPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += ExtractLevelStr;
        GameManager.OnGameStart += ExtractLevelDimension;
        GameManager.OnGameStart += ExtractPortalNumber;
    }

    private void ExtractPortalNumber()
    {
        portalInput.text = predefinedGenerationStrategy.portalNumber.ToString();
    }

    private void ExtractLevelDimension()
    {
        dimensionInputX.text = fixedWorld.patchDimension.x.ToString();
        dimensionInputY.text = fixedWorld.patchDimension.y.ToString();
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= ExtractLevelStr;
        GameManager.OnGameStart -= ExtractLevelDimension;
        GameManager.OnGameStart -= ExtractPortalNumber;
    }

    private void ExtractLevelStr()
    {
        var level = predefinedGenerationStrategy.ParseLevelStr(predefinedGenerationStrategy.levelStr);
        levelInput.text = FormatLevel(level);
    }

    private string FormatLevel(int[,] _level)
    {
        var longestCharLength = _level.Cast<int>().Max(_number => _number.ToString().Length);
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
        var level = levelInput.text;
        predefinedGenerationStrategy.levelStr = level;
        SetDimension();
        SetPortalNumber();
        SceneLoader.Instance.ReloadScene();
    }

    private void SetPortalNumber()
    {
        predefinedGenerationStrategy.portalNumber = int.Parse(portalInput.text);
    }
    
    private void SetDimension()
    {
        var x = int.Parse(dimensionInputX.text);
        var y = int.Parse(dimensionInputY.text);
        fixedWorld.patchDimension = new Vector2Int(x, y);
        fixedWorld.screenAreaDimension = new Vector2Int(x, y+4);
        fixedWorld.visibleAreaDimension = new Vector2Int(x, y);
        fixedWorld.visibleAreaDimensionOuter = fixedWorld.visibleAreaDimension;
    }
}