using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    public bool collapsed = false;
    public int x;
    public int y;
    public SpriteRenderer spriteRenderer;


    public void Init(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    private void Start()
    {
        spriteRenderer.color = Color.gray;
    }


    // public void Collapse()
    // {
    //     // change sprite
    //     var item = GetRandomItem();
    //     XLogger.Log(Category.WFC, $"cell ({x},{y}) collapsed with item {item}");
    //     spriteRenderer.sprite = item.image;
    //     spriteRenderer.color = Color.white;
    //     collapsed = true;
    //
    //     // update neighbour's possibilities
    //     var top = gridSystem.GetCell(x, y + 1);
    //     if (top)
    //         top.UpdatePossibilities(item.topRule, item);
    //
    //     var down = gridSystem.GetCell(x, y - 1);
    //     if (down)
    //         down.UpdatePossibilities(item.downRule, item);
    //
    //     var left = gridSystem.GetCell(x - 1, y);
    //     if (left)
    //         left.UpdatePossibilities(item.leftRule, item);
    //
    //     var right = gridSystem.GetCell(x + 1, y);
    //     if (right)
    //         right.UpdatePossibilities(item.rightRule, item);
    // }

    public override string ToString()
    {
        return $"cell ({x},{y})";
    }
}