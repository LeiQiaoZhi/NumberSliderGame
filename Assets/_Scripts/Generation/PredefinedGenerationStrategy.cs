using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Predefined Generation Strategy", menuName = "Generation/PredefinedGenerationStrategy",
    order = 0)]
public class PredefinedGenerationStrategy : PatchGenerationStrategy
{
    [Tooltip("1 index")] public int levelNumber;
    [Space(10)] public int portalNumber;
    [TextArea(10, 10)] public string levelStr;
    [Space(10)] public int playerStartHealth = 1;
    [Space(10)] public MenuCellConfig portalCellConfig;

    public override void Generate()
    {
        FindObjectOfType<PlayerHealth>().SetMaxHealth(playerStartHealth);

        var level = ParseLevelStr(levelStr);

        for (int x = 0; x < patchDimension.x; x++)
        {
            for (int y = 0; y < patchDimension.y; y++)
            {
                var number = level[x, y];
                NumberCell cell = GetCell(x, y);
                cell.SetNumber(number);
                if (number == portalNumber)
                {
                    var portalCell = cell.AddComponent<LevelPortalCell>();
                    portalCell.SetUp(portalCellConfig, cell, number);
                    portalCell.SetLevel(levelNumber);
                }
            }
        }
    }

    public int[,] ParseLevelStr(string _levelStr)
    {
        var levelInts = new int[patchDimension.x, patchDimension.y];

        // turn non-numeric characters into spaces
        for (var i = 0; i < _levelStr.Length; i++)
        {
            if (!char.IsDigit(_levelStr[i]) && _levelStr[i] != '\n')
                _levelStr = _levelStr.Replace(_levelStr[i], ' ');
        }

        // get rid of extra spaces
        while (_levelStr.Contains("  "))
            _levelStr = _levelStr.Replace("  ", " ");
        // remove empty lines
        while (_levelStr.Contains("\n\n"))
            _levelStr = _levelStr.Replace("\n\n", "\n");

        // for each line, split by spaces and parse into int
        var lines = _levelStr.Split('\n');
        var parsedRows = 0;
        for (var row = 0; row < lines.Length; row++)
        {
            if (lines[row].Trim().Length == 0)
                continue;
            if (parsedRows++ >= patchDimension.y)
                break;
            var numbers = lines[row].Trim().Split(' ');
            for (var col = 0; col < numbers.Length; col++)
            {
                if (col >= patchDimension.x)
                    break;
                // strip out spaces
                var numberStr = numbers[col].Replace(" ", "");
                // in text, top left is 0,0, but in game, bottom left is 0,0
                levelInts[col, patchDimension.y - 1 - row] = int.Parse(numberStr);
            }
        }

        return levelInts;
    }
}