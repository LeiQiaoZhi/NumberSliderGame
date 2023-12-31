﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WorldUIManager : MonoBehaviour
{
    public GridWorldPosSetter pauseButton;
    public GridWorldPosSetter editorButton;
    public GridWorldPosSetter scorePanel;
    public GridWorldPosSetter healthPanel;
    public GridWorldPosSetter gameTitle;
 public GridWorldPosSetter bonusButton;
    [Space(10)]
    public VisibilityMask visibilityMask;
    public VisibilityMask visibilityMaskOuter;

    private void OnEnable()
    {
        NumberGridGenerator.OnGenerationStart += SetUpUI;
    }

    private void OnDisable()
    {
        NumberGridGenerator.OnGenerationStart -= SetUpUI;
    }

    private void SetUpUI(InfiniteGridSystem _gridSystem, UIConfig _uiConfig, Vector2Int _visibleAreaDimension,
        Vector2Int _visibleAreaDimensionOuter)
    {
        var uiGameObjects = new List<GridWorldPosSetter> { pauseButton, editorButton, scorePanel, healthPanel };
        var uiBools = new List<bool> { _uiConfig.pause, _uiConfig.editor, _uiConfig.score, _uiConfig.health };
        for (var i = 0; i < uiGameObjects.Count; i++)
        {
            if (uiGameObjects[i] == null) continue;
            uiGameObjects[i].gameObject.SetActive(uiBools[i]);
            uiGameObjects[i].SetPosition(_gridSystem);
        }
        
        if (visibilityMask.areaType == AreaType.CustomDimension)
            visibilityMask.customVisibleAreaDimension = _visibleAreaDimension;
        if (visibilityMaskOuter.areaType == AreaType.CustomDimension)
            visibilityMaskOuter.customVisibleAreaDimension = _visibleAreaDimensionOuter;
        if (!_uiConfig.visibilityMaskFollowCamera)
        {
            visibilityMask.transform.SetParent(null);
            visibilityMaskOuter.transform.SetParent(null);
        }
        visibilityMask.ChangeMaskSize();
        visibilityMaskOuter.ChangeMaskSize();
        
        if (gameTitle != null)
        {
            gameTitle.gameObject.SetActive(_uiConfig.gameTitle);
            gameTitle.SetPosition(_gridSystem);
        }
        
        if (bonusButton != null)
        {
            bonusButton.gameObject.SetActive(_uiConfig.bonusButton);
            bonusButton.SetPosition(_gridSystem);
        }
    }

    private void SetUpUI(World _world, InfiniteGridSystem _gridSystem)
    {
        SetUpUI(_gridSystem, _world.UIConfig, _world.visibleAreaDimension, _world.visibleAreaDimensionOuter);
    }
}