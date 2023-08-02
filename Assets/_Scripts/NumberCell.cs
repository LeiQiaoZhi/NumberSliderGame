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
    private bool inactive_;

    public void Init(Cell _cell)
    {
        cell_ = _cell;
        x_ = cell_.x;
        y_ = cell_.y;
        inactive_ = false;
        text_ = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetInActive()
    {
        inactive_ = true;
        // set text and background colors to transparent
        text_.color = new Color(0, 0, 0, 0);
        cell_.spriteRenderer.color = new Color(0, 0, 0, 0);
    }

    public bool IsActive()
    {
        return !inactive_;
    }

    public void SetActive()
    {
        cell_.spriteRenderer.color = Color.green;
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