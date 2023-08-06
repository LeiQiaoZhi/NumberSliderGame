using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Random Generation Strategy", menuName = "Generation/RandomGenerationStrategy", order = 0)]
public class RandomGenerationStrategy : PatchGenerationStrategy
{
    public override void Generate()
    {
        // default -- set all cells to 1
        foreach (NumberCell cell in numberCells)
        {
            var random = pool[Random.Range(0, pool.Count)];
            cell.SetNumber(random);
        }
    }
}
