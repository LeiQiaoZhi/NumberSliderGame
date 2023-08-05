using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public World world;
    
    private PlayerMovement playerMovement_;
    private NumberGridGenerator numberGridGenerator_;
    
    private void Start()
    {
        playerMovement_ = FindObjectOfType<PlayerMovement>();
        numberGridGenerator_ = FindObjectOfType<NumberGridGenerator>();
        if (playerMovement_ == null)
            XLogger.LogError(Category.GameManager, $"player movement is null");
        StartGame();
    }

    public void StartGame()
    {
        numberGridGenerator_.SetWorld(world);
        playerMovement_.OnGameStart(numberGridGenerator_);
    }
}
