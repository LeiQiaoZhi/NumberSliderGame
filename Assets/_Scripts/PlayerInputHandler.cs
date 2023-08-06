using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInputHandler : MonoBehaviour
{
    public GameStates gameStates;
    private PlayerMovement playerMovement_;
    private void Start()
    {
        playerMovement_ = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            playerMovement_.Move(new Vector2Int(0, 1));
        else if (Input.GetKeyDown(KeyCode.S))
            playerMovement_.Move(new Vector2Int(0, -1));
        else if (Input.GetKeyDown(KeyCode.A))
            playerMovement_.Move(new Vector2Int(-1, 0));
        else if (Input.GetKeyDown(KeyCode.D))
            playerMovement_.Move(new Vector2Int(1, 0));

    }
}
