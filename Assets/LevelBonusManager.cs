using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBonusManager : MonoBehaviour
{
    public VerticalLayoutGroup bonusHolder;
    public GameObject bonusPrefab;
    public GameObject bonusPanel;

    private List<LevelBonus> levelBonuses_;
    private InfiniteGridSystem gridSystem_;
    
    private void OnEnable()
    {
        NumberGridGenerator.OnGenerationStart += SetLevelBonusUI;
    }

    private void OnDisable()
    {
        NumberGridGenerator.OnGenerationStart -= SetLevelBonusUI;
    }

    private void SetLevelBonusUI(World _world, InfiniteGridSystem _gridsystem)
    {
        bonusPanel.SetActive(false);
        // clear previous bonuses
        foreach (Transform child in bonusHolder.transform)
            Destroy(child.gameObject);
        
        // set new bonuses
        foreach (var bonus in _world.levelBonuses)
        {
            var bonusObject = Instantiate(bonusPrefab, bonusHolder.transform);
            var bonusText = bonusObject.GetComponentInChildren<TextMeshProUGUI>();
            bonusText.text = bonus.description;
        }
        
        levelBonuses_ = _world.levelBonuses;
        gridSystem_ = _gridsystem;
    }

    // called when enter portal
    public void CheckBonusConditions()
    {
        foreach (var bonus in levelBonuses_)
        {
            bonus.CheckCondition(gridSystem_);
        }
    }
}
