using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Progression", order = 1)]
public class Progression : ScriptableObject
{
    public List<World> worldPool;
    public Progression nextProgression;
    [Tooltip("Null means restart with self")]
    public Progression restartProgression;
    

    public World GetWorld()
    {
        World world = worldPool[Random.Range(0, worldPool.Count)];
        world.InitVisibleAreaDimensions();
        world.InitColorPreset();
        return world;
    }

    public Progression GetRestartProgression()
    {
        if (restartProgression == null)
            return this;
        return restartProgression;
    }
}