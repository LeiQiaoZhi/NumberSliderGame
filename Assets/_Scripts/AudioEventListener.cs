using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListener : MonoBehaviour
{
    public string playerMoveSoundName;
    
    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _mergeResult)
    {
        AudioManager.instance.PlaySound(playerMoveSoundName);
    }
}
