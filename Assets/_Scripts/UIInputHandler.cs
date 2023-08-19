using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    private UIManager uiManager_;
    private UIConfig uiConfig_;

    void Start()
    {
        uiManager_ = GetComponent<UIManager>();
    }

    private void OnEnable()
    {
        NumberGridGenerator.OnGenerationStart += SetUIConfig;
    }

    private void OnDisable()
    {
        NumberGridGenerator.OnGenerationStart -= SetUIConfig;
    }

    // Update is called once per frame
    void Update()
    {
        if (uiConfig_!= null && !uiConfig_.pause) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiManager_.gameStates.state == GameStates.GameState.Playing)
                uiManager_.Pause();
            else if (uiManager_.gameStates.state == GameStates.GameState.Pause)
                uiManager_.Resume();
        }
        
    }

    public void SetUIConfig(World _world, InfiniteGridSystem _gridSystem)
    {
        uiConfig_ = _world.UIConfig;
    }
}
