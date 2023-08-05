using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GenerationStrategyItem
{
    public PatchGenerationStrategy generationStrategy;
    [Tooltip("Weight of choosing this strategy in the generation process")]
    [Range(0,10)]
    public int weight = 1; 
}

[CreateAssetMenu(fileName = "World", menuName = "World", order = 0)]
public class World : ScriptableObject
{
    public PatchGenerationStrategy startingGenerationStrategy;
    public List<GenerationStrategyItem> generationStrategies;

    public PatchGenerationStrategy GetRandomStrategy()
    {
        var totalWeight = generationStrategies.Sum(_item => _item.weight);
        var random = Random.Range(0, totalWeight);
        var currentWeight = 0;
        foreach (GenerationStrategyItem item in generationStrategies)
        {
            currentWeight += item.weight;
            if (random < currentWeight && item.weight > 0)
                return item.generationStrategy;
        }
        return null;
    }
}