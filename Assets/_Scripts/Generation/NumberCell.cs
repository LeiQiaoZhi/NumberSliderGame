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
    }

    public void SetVisited()
    {
        visitedNum_++;
        text_.color = new Color(0, 0, 0, 0);
        if (visitedNum_ == 1) // first time visited
            cell_.spriteRenderer.color = Color.gray;
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
}