using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    private UIManager uiManager_;
    
    void Start()
    {
        uiManager_ = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiManager_.gameStates.state == GameStates.GameState.Playing)
                uiManager_.Pause();
            else if (uiManager_.gameStates.state == GameStates.GameState.Pause)
                uiManager_.Resume();
        }
        
    }
}
