using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Same Number Rect GS", menuName = "Generation/SameNumberRect",
    order = 0)]
public class SameNumberRectGenerationStrategy : PatchGenerationStrategy
{
    [Header("Same Number Generation Strategy")]
    public List<int> sameNumberPool;

    [Header("Inclusive Ranges")]
    public Vector2Int clusterNumberRange;
    public List<Vector2Int> sameNumberDimensions;
    
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
        // generate clusters of same number
        var clusterNumber = Random.Range(clusterNumberRange.x, clusterNumberRange.y+1);
        for (var i = 0; i < clusterNumber; ++i)
        {
            Vector2Int size = sameNumberDimensions[Random.Range(0, sameNumberDimensions.Count)];
            // apply random rotation
            if (Random.Range(0,2) == 0)
                (size.x, size.y) = (size.y, size.x);
            var topLeft = new Vector2Int(Random.Range(0, patchDimension.x - size.x + 1),
                Random.Range(0, patchDimension.y - size.y + 1));
            var index = Random.Range(0, sameNumberPool.Count);
            var number = sameNumberPool[index];
            Color color = colorPreset.GetColorViaIndex(index);
            Rect(topLeft, size, number, color);
        }
    }

}