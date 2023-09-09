using System;
using UnityEngine;

public class MenuCell : MonoBehaviour
{
    protected MenuCellConfig config;
    protected NumberCell numberCell;
    protected bool active; // if inactive, player cannot move to this cell

    public MenuCell SetUp(MenuCellConfig _config, NumberCell _numberCell, int _number = 0)
    {
        config = _config;
        numberCell = _numberCell;

        numberCell.SetNumber(_number);
        numberCell.SetTextColor(_config.textColor);
        if (_config.text != "")
            numberCell.SetText(_config.text);

        numberCell.SetColor(config.color);
        if (config.overlayPrefab != null)
        {
            GameObject overlay = Instantiate(config.overlayPrefab, transform);
            overlay.transform.localScale = Vector3.one;
        }

        return this;
    }

    public MenuCell SetActive(bool _active)
    {
        active = _active;
        if (!active)
            numberCell.SetInActive();
        return this;
    }

    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    protected virtual void OnPlayerMove(PlayerMovement.MergeResult _result)
    {
        if (_result.targetTransform == numberCell.transform)
        {
            XLogger.Log(Category.Menu, $"Player moved to {config}");
            if (config.visitEvent != null) config.visitEvent.Raise();
            if (config.progression != null)
                GameManager.Instance.LoadLevel(config.progression, 0.1f);
        }
    }
}