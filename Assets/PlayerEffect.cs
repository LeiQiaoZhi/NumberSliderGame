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

    private void OnPlayerMove(PlayerMovement.MergeResult _targetcell)
    {
        if (numberText != null)
            numberText.text = _targetcell.result.ToString();
    }
}
