using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBonusManager : MonoBehaviour
{
    public VerticalLayoutGroup bonusHolder;
    public VerticalLayoutGroup levelFinishBonusHolder;
    public GameObject bonusPrefab;
    public GameObject bonusPanel;
    public Color completedColor;
    public Color notCompletedColor;

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
            GameObject bonusObject = Instantiate(bonusPrefab, bonusHolder.transform);
            var bonusText = bonusObject.GetComponentInChildren<TextMeshProUGUI>();
            bonusText.text = bonus.description;
        }
        
        levelBonuses_ = _world.levelBonuses;
        gridSystem_ = _gridsystem;
    }
    
    public void OpenBonusPanel()
    {
        bonusPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }
    
    public void CloseBonusPanel()
    {
        bonusPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    // called when a level is completed
    public void CheckBonusConditions()
    {
        // clear previous bonuses
        foreach (Transform child in levelFinishBonusHolder.transform)
            Destroy(child.gameObject);
        
        foreach (LevelBonus bonus in levelBonuses_)
        {
            var meet = bonus.CheckCondition(gridSystem_);
            GameObject bonusObject = Instantiate(bonusPrefab, levelFinishBonusHolder.transform);
            var bonusText = bonusObject.GetComponentInChildren<TextMeshProUGUI>();
            bonusText.text = bonus.description;
            bonusText.color = (meet) ? completedColor : notCompletedColor;
            bonusObject.GetComponentInChildren<Image>().color = (meet) ? completedColor : notCompletedColor;
        }
        
        GameManager.Instance.gameStates.state = GameStates.GameState.Over;
    }
}
