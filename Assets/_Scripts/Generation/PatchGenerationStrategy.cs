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

    protected NumberCell GetCell(int _x, int _y)
    {
        if (_x < 0 || _x >= patchDimension.x || _y < 0 || _y >= patchDimension.y)
            return null;
        var index = _x + _y * patchDimension.x;
        return numberCells[index];
    }
}