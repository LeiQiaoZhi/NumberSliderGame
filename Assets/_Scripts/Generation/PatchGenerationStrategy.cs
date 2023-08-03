using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PatchGenerationStrategy : ScriptableObject
{
    [Tooltip("Pool of numbers to choose from when generating a patch")]
    public List<int> pool;

    protected List<NumberCell> numberCells_;
    protected Vector2Int patchDimension_;
    
    public void GetData(List<NumberCell> _numberCells, Vector2Int _patchDimension)
    {
        numberCells_ = _numberCells;
        patchDimension_ = _patchDimension;
    }

    public virtual void Generate()
    {
        // default -- set all cells to 1
        foreach (NumberCell cell in numberCells_)
        {
            cell.SetNumber(1);
        }
    }

    private NumberCell GetCell(int _x, int _y)
    {
        var index = _x + _y * patchDimension_.x;
        Assert.IsTrue(index >= 0 && index < numberCells_.Count);
        return numberCells_[index];
    }
}