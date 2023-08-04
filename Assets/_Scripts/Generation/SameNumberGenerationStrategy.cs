using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(fileName = "Same Number Generation Strategy", menuName = "Generation/SameNumberGenerationStrategy",
    order = 0)]
public class SameNumberGenerationStrategy : PatchGenerationStrategy
{
    [Header("Same Number Generation Strategy")]
    public List<int> sameNumberPool;

    public Vector2Int clusterNumberRange;
    
    public Vector2Int sameNumberSizeMin;

    public Vector2Int sameNumberSizeMax;
    
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
            var topLeft = new Vector2Int(Random.Range(0, patchDimension.x - sameNumberSizeMin.x),
                Random.Range(0, patchDimension.y - sameNumberSizeMin.y));
            var size = new Vector2Int(Random.Range(sameNumberSizeMin.x, sameNumberSizeMax.x+1),
                Random.Range(sameNumberSizeMin.y, sameNumberSizeMax.y+1));
            var number = sameNumberPool[Random.Range(0, sameNumberPool.Count)];
            var color = colorPreset.GetColor(number);
            Rect(topLeft, size, number, color);
        }
    }

    // fill a rectangle with same number
    private void Rect(Vector2Int _topLeft, Vector2Int _size, int _number, Color _color)
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
}