﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Progression", order = 1)]
public class Progression : ScriptableObject
{
    public List<World> worldPool;
    public Progression nextProgression;
    public World GetWorld()
    {
        return worldPool[Random.Range(0, worldPool.Count)];
    }
}