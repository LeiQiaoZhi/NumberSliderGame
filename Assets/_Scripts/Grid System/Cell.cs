using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public SpriteRenderer spriteRenderer;


    public void Init(int _x, int _y)
    {
        x = _x;
        y = _y;
        spriteRenderer.color = Color.gray;
    }

    public override string ToString()
    {
        return $"cell ({x},{y})";
    }

    public void SetSprite(Sprite _itemImage)
    {
        spriteRenderer.sprite = _itemImage;
        spriteRenderer.color = Color.white;
    }
}