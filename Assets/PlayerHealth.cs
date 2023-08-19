using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int health_ = 2;
    [Header("UI")]
    public RectTransform healthBar;
    public GameObject healthObjectPrefab;

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameManager.OnGameStart -= OnGameStart;
    }

    private void OnSceneLoaded(Scene _arg0, LoadSceneMode _arg1)
    {
        health_ = maxHealth;
    }
    private void OnGameStart()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var numHealthObjects = healthBar.childCount;
        
        for (int i = 0; i < numHealthObjects; i++)
            Destroy(healthBar.GetChild(i).gameObject);
        
        for (int i = 0; i < health_; i++)
            Instantiate(healthObjectPrefab, healthBar);
    }

    public void ChangeHealth(int _i)
    {
        health_ += _i;
        UpdateUI();
    }
    
    public int GetHealth()
    {
        return health_;
    }

    public void SetMaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
        health_ = maxHealth;
        UpdateUI();
    }
}
