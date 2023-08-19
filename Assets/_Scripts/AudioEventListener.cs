using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventListener : MonoBehaviour
{
    public string playerMoveDivideSoundName;
    public string playerMoveMinusSoundName;
    public string playerMovePlusOneSoundName;
    public string playerMoveInvalidSoundName;
    
    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
        PlayerMovement.OnPlayerInvalidMove += OnPlayerInvalidMove;
    }

    private void OnPlayerInvalidMove(Vector2Int _direction)
    {
        AudioManager.instance.PlaySound(playerMoveInvalidSoundName);
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _mergeResult)
    {
        if (_mergeResult.type == PlayerMovement.MergeType.Divide)
        {
            AudioManager.instance.PlaySound(playerMoveDivideSoundName);
        }
        else if (_mergeResult.type == PlayerMovement.MergeType.Minus)
        {
            AudioManager.instance.PlaySound(playerMoveMinusSoundName);
        }
        else if (_mergeResult.type == PlayerMovement.MergeType.PlusOne || _mergeResult.type == PlayerMovement.MergeType.Static)
        {
            AudioManager.instance.PlaySound(playerMovePlusOneSoundName);
        }
    }
}
