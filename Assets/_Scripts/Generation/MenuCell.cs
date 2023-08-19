using System;
using UnityEngine;

public class MenuCell : MonoBehaviour
{
    private MenuCellConfig config_;
    private NumberCell numberCell_;

    public void SetUp(MenuCellConfig _config, NumberCell _numberCell)
    {
        config_ = _config;
        numberCell_ = _numberCell;
        // don't show the number
        numberCell_.SetNumber(0);
        numberCell_.SetTextColor(new Color(0,0,0,0));
        numberCell_.SetColor(config_.color);
        GameObject overlay = Instantiate(config_.overlayPrefab, transform);
        overlay.transform.localScale = Vector3.one;
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
            XLogger.Log(Category.Menu ,$"Player move to {config_}");
            config_.visitEvent.Raise();
            GameManager.Instance.LoadLevel(config_.progression);
        }
    }
}