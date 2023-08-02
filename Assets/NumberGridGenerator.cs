using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class NumberGridGenerator : MonoBehaviour
{
    public GridSystem gridSystem;
    private List<NumberCell> numberCells_;

    public void GenerateCells()
    {
        var cells = gridSystem.CreateDefaultCells();
        numberCells_ = new List<NumberCell>();
        foreach (var cell in cells)
        {
            var number = Random.Range(1, 21);
            var numberCell = new NumberCell(gridSystem.GetCell(cell));
            numberCells_.Add(numberCell);
            numberCell.SetNumber(number);

        }
    }
    public NumberCell GetCell(int _x, int _y)
    {
        if (_x < 0 || _x >= gridSystem.width || _y < 0 || _y >= gridSystem.height)
        {
            XLogger.LogWarning(Category.Wfc, $"coordinate {_x},{_y} out of bounds");
            return null;
        }

        if (numberCells_.Count != gridSystem.width * gridSystem.height)
        {
            XLogger.LogWarning((Category.Wfc, "WaveFunctionCollapse.wfc cells not properly initialized"));
            return null;
        }

        var index = _x + _y * gridSystem.width;
        Assert.IsTrue(_x == numberCells_[index].GetPosition().Item1 && _y == numberCells_[index].GetPosition().Item2,
            $"index {_x},{_y} not match with result cell position {numberCells_[index].GetPosition().Item1},{numberCells_[index].GetPosition().Item2}");
        return numberCells_[index];
    }

    public NumberCell GetCell(Tuple<int, int> _position)
    {
        return GetCell(_position.Item1, _position.Item2);
    }

    public int Width()
    {
        return gridSystem.width;
    }
    
    public int Height()
    {
        return gridSystem.height;
    }
}
