using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    #if UNITY_EDITOR
    [Multiline]
    public string developerDescription = "";
    #endif

    private readonly List<GameEventListener> gameEventListeners_ = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = gameEventListeners_.Count-1; i >= 0; i--)
        {
            gameEventListeners_[i].OnRaise();
        }
    }

    public void RegisterGameEventListener(GameEventListener _listener)
    {
        if (!gameEventListeners_.Contains(_listener))
        {
            gameEventListeners_.Add(_listener); 
        }
    }

    public void UnregisterGameEventListener(GameEventListener _listener)
    {
        if (gameEventListeners_.Contains(_listener))
        {
            gameEventListeners_.Remove(_listener);
        }
    }

}
