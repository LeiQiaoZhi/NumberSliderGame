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

        var index = Random.Range(0, sameNumberPool.Count);
        var number = sameNumberPool[index];
        Color color = colorPreset.GetColorViaIndex(index);
        foreach (NumberCell cell in GetDiagonal(positiveSlope, negativeSlope))
        {
            cell.SetNumber(number);
            cell.SetColor(color);   
        }
    }
   

}