using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Same Number Diagonal GS", menuName = "Generation/SameNumberDiagonal",
    order = 0)]
public class SameNumberDiagonalGenerationStrategy : PatchGenerationStrategy
{
    [Header("Same Number Generation Strategy")]
    public List<int> sameNumberPool;

    [Header("Diagonal")]
    public bool positiveSlope;
    public bool negativeSlope;
    
    [Header("Color")]
    public ColorPreset colorPreset;

    // generate clusters of same number, rest are random
    public override void Generate()
    {
        // default -- all random
        foreach (NumberCell cell in numberCells)
        {
            var random = pool[Random.Range(0, pool.Count)];
            cell.SetNumber(random);
        }

        var number = sameNumberPool[Random.Range(0, sameNumberPool.Count)];
        Color color = colorPreset.GetColor(number);
        foreach (NumberCell cell in GetDiagonal(positiveSlope, negativeSlope))
        {
            cell.SetNumber(number);
            cell.SetColor(color);   
        }
    }
    
    protected List<NumberCell> GetDiagonal(bool _positiveSlope, bool _negativeSlope)
    {
        var diagonal = new List<NumberCell>();
        for (var i = 0; i < patchDimension.x; ++i)
        {
            if (_positiveSlope)
                diagonal.Add(GetCell(i, i));
            if (_negativeSlope)
                diagonal.Add(GetCell(i, patchDimension.y - i - 1));
        }
        return diagonal;
    }

}