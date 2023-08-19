using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    public bool infinite;
    public bool cameraFollow;
    public bool enterResetScore;
    [Space(10)]
    public PatchGenerationStrategy startingGenerationStrategy;
    public List<GenerationStrategyItem> generationStrategies;

    [Header("Portal Generation")] [Tooltip("n means one portal guaranteed every n trys")]
    public int portalGenerationGuarantee = 5;

    public List<GenerationStrategyItem> portalGenerationStrategies;
    [Space(10)] public int minPortalDistanceFromCenter = 2;
    public int minPortalDistanceFromEachOther = 2;
    [Header("Color")] public ColorPreset colorPreset;
    [Header("Grid")] public Vector2Int patchDimension;
    [Tooltip("Used to calculate cell dimension")]
    public Vector2Int screenAreaDimension;
    [Space(10)]
    public bool automaticVisibleAreaDimension = true;
    public Vector2Int visibleAreaDimension;
    public Vector2Int visibleAreaDimensionOuter;
    [Space(10)]
    public UIConfig UIConfig;
    [Space(10)]
    public PlayerStartPositionType playerStartPositionType;

    private int portalGenerationMissCounter_ = 0;

    // pass color preset to all generation strategies
    public void InitColorPreset()
    {
        startingGenerationStrategy.SetColorPreset(colorPreset);
        foreach (GenerationStrategyItem item in generationStrategies)
            item.generationStrategy.SetColorPreset(colorPreset);
        foreach (GenerationStrategyItem item in portalGenerationStrategies)
            item.generationStrategy.SetColorPreset(colorPreset);
    }

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

    public void InitVisibleAreaDimensions()
    {
        if (automaticVisibleAreaDimension)
        {
            visibleAreaDimension = patchDimension;
            visibleAreaDimensionOuter = patchDimension;
        }
    }

    public Vector2Int GetPlayerStartPosition()
    {
        return playerStartPositionType switch
        {
            PlayerStartPositionType.Center => patchDimension / 2,
            PlayerStartPositionType.BotLeft => new Vector2Int(0, 0),
            _ => patchDimension / 2
        };
    }
}

public enum PlayerStartPositionType
{
    Center,
    BotLeft
}