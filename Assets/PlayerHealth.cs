using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 2;
    private int health_ = 2;
    [Header("UI")] public RectTransform healthBar;
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
        SetMaxHealth(maxHealth);
    }

    private void OnGameStart()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var numHealthObjects = healthBar.childCount;

        // ensure that the number of health objects is the same as the maxhealth
        if (numHealthObjects > maxHealth)
        {
            for (int i = numHealthObjects - 1; i >= maxHealth; i--)
                Destroy(healthBar.GetChild(i).gameObject);
        }
        else if (numHealthObjects < maxHealth)
        {
            for (int i = numHealthObjects; i < maxHealth; i++)
                Instantiate(healthObjectPrefab, healthBar);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            healthBar.GetChild(i).GetComponentInChildren<CanvasGroup>()
                .TweenFloat(i < health_ ? 1.0f : 0.0f, 0.1f)
                .Play();
        }
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

        // destroy all health objects
        for (int i = healthBar.childCount - 1; i >= 0; i--)
        {
            Destroy(healthBar.GetChild(i).gameObject);
        }

        for (int i = 0; i < maxHealth; i++)
        {
            Instantiate(healthObjectPrefab, healthBar);
        }
    }
}