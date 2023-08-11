using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    public TextMeshProUGUI numberText;
    private InfiniteGridSystem gridSystem_;

    void Start()
    {
        gridSystem_ = FindObjectOfType<InfiniteGridSystem>();
        transform.localScale = new Vector3(gridSystem_.GetCellDimension().x, gridSystem_.GetCellDimension().y, 1);
    }


    private void OnEnable()
    {
        PlayerMovement.OnPlayerMove += OnPlayerMove;
    }

    private void OnDisable()
    {
        PlayerMovement.OnPlayerMove -= OnPlayerMove;
    }

    private void OnPlayerMove(PlayerMovement.MergeResult _mergeResult)
    {
        if (numberText != null)
            numberText.TweenNumber(_mergeResult.result, 0.14f, 5).Play();
        if (_mergeResult.type == PlayerMovement.MergeType.Divide)
        {
            // effect
        }

        if (_mergeResult.type == PlayerMovement.MergeType.Minus)
        {
            // effect
        }
    }
}