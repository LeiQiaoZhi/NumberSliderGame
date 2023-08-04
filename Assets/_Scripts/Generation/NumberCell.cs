using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class NumberCell : MonoBehaviour
{
    private TextMeshProUGUI text_;
    private CanvasGroup canvasGroup_;

    private int number_;
    private Cell cell_;

    private int x_;
    private int y_;
    private int visitedNum_;

    public void Init(Cell _cell)
    {
        cell_ = _cell;
        x_ = cell_.x;
        y_ = cell_.y;
        visitedNum_ = 0;
        text_ = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup_ = GetComponent<CanvasGroup>();
        SetTransparency(1.0f);
    }

    public void SetVisited()
    {
        visitedNum_++;
        text_.color = new Color(0, 0, 0, 0);
        if (visitedNum_ == 1) // first time visited
        {
            cell_.spriteRenderer.color = new Color(0.8f, 0.8f, 0.8f, 1);
            text_.color = new Color(0.6f, 0.6f, 0.6f, 1);
            text_.text = "+1";
        }
        else if (visitedNum_ > 1) // second time visited
            cell_.spriteRenderer.color = Color.black;
    }

    public bool IsActive()
    {
        return visitedNum_ <= 1;
    }

    public bool IsVisited()
    {
        return visitedNum_ > 0;
    }

    public void SetColor(Color _color)
    {
        cell_.spriteRenderer.color = _color;
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

    public void SetTransparency(float _alpha)
    {
        Color color = cell_.spriteRenderer.color;
        color.a = _alpha;
        cell_.spriteRenderer.color = color;
        canvasGroup_.alpha = _alpha;
    }
}