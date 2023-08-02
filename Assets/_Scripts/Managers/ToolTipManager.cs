using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTipManager : MonoBehaviour
{
    public RectTransform tootTip;
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI contentText;

    // singleton
    public static ToolTipManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        HideToolTip();
    }

    public void ShowToolTip(string _header, string _content)
    {
        headerText.text = _header;
        contentText.text = _content;
        tootTip.gameObject.SetActive(true);
        SetToolTipPosition();
    }

    private void SetToolTipPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tootTip.parent as RectTransform, mousePosition,
            null, out var mouseCanvasPos);

        var rect = tootTip.rect;
        var dimension = new Vector2(rect.width, rect.height)/2;
        var sign = Vector2.one;

        // ReSharper disable PossibleLossOfFraction
        if (mousePosition.x < Screen.width / 2 && mousePosition.y < Screen.height / 2)
        {
            sign = -Vector2.one;
        }
        else if (mousePosition.x >= Screen.width / 2 && mousePosition.y < Screen.height / 2)
        {
            sign = new Vector2(1, -1);
        }
        else if (mousePosition.x < Screen.width / 2 && mousePosition.y >= Screen.height / 2)
        {
            sign = new Vector2(-1, 1);
        }

        tootTip.anchoredPosition = new Vector2(mouseCanvasPos.x - sign.x * dimension.x,
            mouseCanvasPos.y - sign.y * dimension.y);
    }

    public void HideToolTip()
    {
        tootTip.gameObject.SetActive(false);
    }
}