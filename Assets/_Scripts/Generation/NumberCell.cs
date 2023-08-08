using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NumberCell : MonoBehaviour
{
    public bool isPortal = false;
    
    private ColorPreset colorPreset_;
    private TextMeshProUGUI text_;
    private CanvasGroup canvasGroup_;

    private int number_;
    private Cell cell_;

    private int x_;
    private int y_;
    private int visitedNum_;

    public void Init(Cell _cell, ColorPreset _colorPreset)
    {
        cell_ = _cell;
        x_ = cell_.x;
        y_ = cell_.y;
        visitedNum_ = 0;
        text_ = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup_ = GetComponent<CanvasGroup>();
        // color
        colorPreset_ = _colorPreset;
        cell_.spriteRenderer.color = colorPreset_.activeColor;
        text_.color = colorPreset_.activeTextColor;
    }

    public void SetVisited()
    {
        visitedNum_++;
        text_.color = new Color(0, 0, 0, 0);
        if (visitedNum_ == 1) // first time visited
        {
            SetColor(colorPreset_.visitedColor, 0.3f);
            text_.color = colorPreset_.visitedTextColor;
            text_.text = "+1";
        }
        else if (visitedNum_ > 1) // second time visited
            SetColor(colorPreset_.inactiveColor, 0.4f);
    }

    public bool IsActive()
    {
        return visitedNum_ <= 1;
    }

    public bool IsVisited()
    {
        return visitedNum_ > 0;
    }

    public void SetColor(Color _color, float _duration = 0.0f)
    {
        if (_duration == 0.0f)
            cell_.spriteRenderer.color = _color;
        else
            cell_.spriteRenderer.TweenColor(_color, _duration).Play();
    }

    public void SetTextColor(Color _color)
    {
        text_.color = _color;
    }

    public int GetNumber()
    {
        return number_;
    }

    public void SetNumber(int _number)
    {
        number_ = _number;
        text_.text = _number.ToString();
    }

    public Tuple<int, int> GetPosition()
    {
        return new Tuple<int, int>(x_, y_);
    }

    public override string ToString()
    {
        return cell_.ToString();
    }

    public void SetPlayerCell()
    {
        SetColor(colorPreset_.playerColor, 0.2f);
        SetTextColor(colorPreset_.playerTextColor);
    }
}