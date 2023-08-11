using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ScreenShaker : MonoBehaviour
{
    // Settings
    [Header("Shake Settings")] 
    [Tooltip("Magnitude of the camera shake")]
    [SerializeField] private float defaultShakeMagnitude = 0.1f;
    [SerializeField] private float gameOverShakeMagnitude;

    // Private variables
    private CinemachineImpulseSource impulseSource_;

    private void Start()
    {
        impulseSource_ = GetComponent<CinemachineImpulseSource>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerInvalidMove += OnPlayerInvalidMove;
        PlayerMovement.OnGameOver += OnGameOver;
    }

    private void OnGameOver(Vector2Int _direction)
    {
        Vector2 vel = (Vector2)_direction * gameOverShakeMagnitude;
        impulseSource_.GenerateImpulseWithVelocity(vel);
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerInvalidMove -= OnPlayerInvalidMove;
        PlayerMovement.OnGameOver -= OnGameOver;
    }

    private void OnPlayerInvalidMove(Vector2Int _direction)
    {
        Vector2 vel = (Vector2)_direction * (defaultShakeMagnitude);
        impulseSource_.GenerateImpulseWithVelocity(vel);
    }
}