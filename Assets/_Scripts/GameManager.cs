using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public World world;
    public ColorPreset colorPreset;
    public GameStates gameStates;
    
    private PlayerMovement playerMovement_;
    private NumberGridGenerator numberGridGenerator_;
    
    private void Start()
    {
        gameStates.Init();
        playerMovement_ = FindObjectOfType<PlayerMovement>();
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();
        if (playerMovement_ == null)
            XLogger.LogError(Category.GameManager, $"player movement is null");
        StartGame();
    }

    private void StartGame()
    {
        numberGridGenerator_.Init(colorPreset, world, gameStates);
        playerMovement_.OnGameStart(numberGridGenerator_, gameStates);
        gameStates.state = GameStates.GameState.Playing;
    }
}
