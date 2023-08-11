using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOpeningObject : MonoBehaviour
{
    public GameEvent sceneOpeningEvent;

    public void OnSceneOpened()
    {
        sceneOpeningEvent.Raise();
    }
}
