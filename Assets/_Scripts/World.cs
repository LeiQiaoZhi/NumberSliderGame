using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GenerationStrategyItem
{
    public PatchGenerationStrategy generationStrategy;

    [Tooltip("Weight of choosing this strategy in the generation process")] [Range(0, 10)]
    public int weight = 1;
}

[CreateAssetMenu(fileName = "World", menuName = "World", order = 0)]
public class World : ScriptableObject
{
    public PatchGenerationStrategy startingGenerationStrategy;
    public List<GenerationStrategyItem> generationStrategies;

    [Header("Portal Generation")] [Tooltip("n means one portal guaranteed every n trys")]
    public int portalGenerationGuarantee = 5;

    public List<GenerationStrategyItem> portalGenerationStrategies;
    [Space(10)] public int minPortalDistanceFromCenter = 2;
    public int minPortalDistanceFromEachOther = 2;

    private int portalGenerationMissCounter_ = 0;

    public PatchGenerationStrategy GetStrategy(Vector2Int _patchPosition, List<Vector2Int> _portalPositions)
    {
        // test whether a portal should be generated
        if (TryGeneratePortal(_patchPosition, _portalPositions))
            return GetWeightedRandomStrat(portalGenerationStrategies);

        return GetWeightedRandomStrat(generationStrategies);
    }

    private bool TryGeneratePortal(Vector2Int _patchPosition, List<Vector2Int> _portalPositions)
    {
        if (GameUtils.ManhattenDistance(_patchPosition) < minPortalDistanceFromCenter)
            return false;
        foreach (Vector2Int portalPosition in _portalPositions)
        {
            if (GameUtils.ManhattenDistance(_patchPosition, portalPosition) < minPortalDistanceFromEachOther)
                return false;
        }

        return GameUtils.GuaranteeRandom(portalGenerationGuarantee, ref portalGenerationMissCounter_);
    }

    private PatchGenerationStrategy GetWeightedRandomStrat(List<GenerationStrategyItem> _strategies)
    {
        var totalWeight = _strategies.Sum(_item => _item.weight);
        var random = Random.Range(0, totalWeight);
        var currentWeight = 0;
        foreach (GenerationStrategyItem item in _strategies)
        {
            currentWeight += item.weight;
            if (random < currentWeight && item.weight > 0)
                return item.generationStrategy;
        }

        return null;
    }

    /// tests whether a strategy is a portal strategy
    public bool IsPortalStrategy(PatchGenerationStrategy _strategy)
    {
        return portalGenerationStrategies.Any(_item => (_item.generationStrategy == _strategy && _item.weight > 0));
    }
}

public class GameUtils
{
    public static int ManhattenDistance(Vector2Int _a, Vector2Int _b = default)
    {
        return Mathf.Abs(_a.x - _b.x) + Mathf.Abs(_a.y - _b.y);
    }

    public static bool GuaranteeRandom(int _guarantee, ref int _missCounter)
    {
        if (_missCounter >= _guarantee - 1)
        {
            _missCounter = 0;
            return true;
        }

        var probability = 1 / (_guarantee - _missCounter);
        var result = Random.Range(0.0f, 1.0f) < probability;
        if (result)
            _missCounter = 0;
        else
            _missCounter += 1;
        return result;
    }
}