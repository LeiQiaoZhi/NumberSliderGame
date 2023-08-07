using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PatchGenerationStrategy : ScriptableObject
{
    [Tooltip("Pool of numbers to choose from when generating a patch")]
    public List<int> pool;

    protected List<NumberCell> numberCells;
    protected Vector2Int patchDimension;
    
    // get data from number grid generator
    public virtual void Init(List<NumberCell> _numberCells, Vector2Int _patchDimension)
    {
        numberCells = _numberCells;
        patchDimension = _patchDimension;
    }

    public virtual void Generate()
    {
        // default -- set all cells to 1
        foreach (NumberCell cell in numberCells)
        {
            cell.SetNumber(1);
        }
    }

    protected Vector2Int GetCenterCoord()
    {
        return new Vector2Int(Mathf.FloorToInt(patchDimension.x / 2.0f),
            Mathf.FloorToInt(patchDimension.y / 2.0f));
    }

    protected NumberCell GetCell(int _x, int _y)
    {
        if (_x < 0 || _x >= patchDimension.x || _y < 0 || _y >= patchDimension.y)
        {
            XLogger.LogWarning(Category.Generation, $"  GetCell({_x}, {_y}) out of range.");
            return null;
        }
        var index = _x + _y * patchDimension.x;
        return numberCells[index];
    }
    
    protected List<NumberCell> GetDiagonal(bool _positiveSlope, bool _negativeSlope)
    {
        var diagonal = new List<NumberCell>();
        for (var i = 0; i < patchDimension.x; i++)
        {
            if (_positiveSlope)
                diagonal.Add(GetCell(i, i));
            if (_negativeSlope)
                diagonal.Add(GetCell(i, patchDimension.y - i - 1));
        }
        return diagonal;
    }
    
    // fill a rectangle with same number
    protected void Rect(Vector2Int _topLeft, Vector2Int _size, int _number, Color _color)
    {
        for (var x = _topLeft.x; x < _topLeft.x + _size.x; ++x)
        {
            for (var y = _topLeft.y; y < _topLeft.y + _size.y; ++y)
            {
                NumberCell cell = GetCell(x, y);
                if (cell == null) continue;
                cell.SetNumber(_number);
                cell.SetColor(_color);
            }
        }
    }

    protected List<NumberCell> GetEdgeCells(Vector2Int _wallHalfDimension, Vector2Int _doorHalfDimension)
    {
        var center = new Vector2Int(patchDimension.x / 2, patchDimension.y / 2);
        if (_wallHalfDimension.x > center.x || _wallHalfDimension.y > center.y)
        {
            XLogger.LogError(Category.Generation, "Wall half dimension is too big");
            return null;
        }

        var edgeCells = new List<NumberCell>();
        // top and bottom
        for (int x = -_wallHalfDimension.x; x <= _wallHalfDimension.x; x++)
        {
            if (Math.Abs(x) <= _doorHalfDimension.x) // skip door
                continue;
            edgeCells.Add(GetCell(center.x + x, center.y - _wallHalfDimension.y));
            edgeCells.Add(GetCell(center.x + x, center.y + _wallHalfDimension.y));
        }

        // left and right
        for (int y = -_wallHalfDimension.y + 1; y < _wallHalfDimension.y; y++)
        {
            if (Math.Abs(y) <= _doorHalfDimension.y) // skip door
                continue;
            edgeCells.Add(GetCell(center.x - _wallHalfDimension.x, center.y + y));
            edgeCells.Add(GetCell(center.x + _wallHalfDimension.x, center.y + y));
        }

        return edgeCells;
    }

    protected List<NumberCell> GetDoorCells(Vector2Int _wallHalfDimension, Vector2Int _doorHalfDimension)
    {
        var center = new Vector2Int(patchDimension.x / 2, patchDimension.y / 2);
        if (_wallHalfDimension.x > center.x || _wallHalfDimension.y > center.y)
        {
            XLogger.LogError(Category.Generation, "Wall half dimension is too big");
            return null;
        }

        var doorCells = new List<NumberCell>();
        // top and bottom
        for (int x = -_doorHalfDimension.x; x <= _doorHalfDimension.x; x++)
        {
            if (Math.Abs(x) <= _doorHalfDimension.x)
            {
                doorCells.Add(GetCell(center.x + x, center.y - _wallHalfDimension.y));
                doorCells.Add(GetCell(center.x + x, center.y + _wallHalfDimension.y));
            }
        }
        // left and right
        for (int y = -_wallHalfDimension.y + 1; y < _wallHalfDimension.y; y++)
        {
            if (Math.Abs(y) <= _doorHalfDimension.y)
            {
                doorCells.Add(GetCell(center.x - _wallHalfDimension.x, center.y + y));
                doorCells.Add(GetCell(center.x + _wallHalfDimension.x, center.y + y));
            }
        }
        return doorCells;
    }
}