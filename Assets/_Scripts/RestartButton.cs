using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public GameEvent restartButtonPressedEvent;
    
    public void OnRestartButtonPressed()
    {
        restartButtonPressedEvent.Raise();
    }
}
