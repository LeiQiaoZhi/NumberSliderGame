using System;
using UnityEngine;

public class MenuCell : MonoBehaviour
{
    private MenuCellConfig config_;
    private NumberCell numberCell_;

    public void SetUp(MenuCellConfig _config, NumberCell _numberCell, int _number = 0)
    {
        config_ = _config;
        numberCell_ = _numberCell;

        numberCell_.SetNumber(_number);
        numberCell_.SetTextColor(_config.textColor);
        if (_config.text != "")
            numberCell_.SetText(_config.text);

        numberCell_.SetColor(config_.color);
        if (config_.overlayPrefab != null)
        {
            GameObject overlay = Instantiate(config_.overlayPrefab, transform);
            overlay.transform.localScale = Vector3.one;
        }
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _result)
    {
        if (_result.targetTransform == numberCell_.transform)
        {
            XLogger.Log(Category.Menu, $"Player moved to {config_}");
            if (config_.visitEvent != null) config_.visitEvent.Raise();
            if (config_.progression != null)
                GameManager.Instance.LoadLevel(config_.progression, 0.1f);
        }
    }
}