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

    private List<WfcItem> possibilities;
    private GridSystem gridSystem;

    public void Init(List<WfcItem> possibleItems, int _x, int _y, GridSystem grid)
    {
        possibilities = possibleItems;
        x = _x;
        y = _y;
        gridSystem = grid;
    }

    private void Start()
    {
        spriteRenderer.color = Color.gray;
    }

    /// <returns> number of possibilities</returns>
    public int GetEntropy()
    {
        return possibilities.Count;
    }

    private WfcItem GetRandomItem()
    {
        return possibilities[Random.Range(0, possibilities.Count)];
    }

    public List<Cell> GetNeighbours()
    {
        List<Cell> neighbours = new List<Cell>();
        var top = gridSystem.GetCell(x, y + 1);
        if (top)
        {
            neighbours.Add(top);
        }

        var down = gridSystem.GetCell(x, y - 1);
        if (down)
        {
            neighbours.Add(down);
        }

        var left = gridSystem.GetCell(x - 1, y);
        if (left)
        {
            neighbours.Add(left);
        }

        var right = gridSystem.GetCell(x + 1, y);
        if (right)
        {
            neighbours.Add(right);
        }

        return neighbours;
    }

    /// <summary>
    /// update this cell's possibilities based on item's rule
    /// </summary>
    private void UpdatePossibilities(Rule rule, WfcItem item)
    {
        XLogger.Log(Category.Cell, $"applying {rule} to cell ({x},{y})");
        var newPossibilities = new List<WfcItem>();
        foreach (var possibility in possibilities)
        {
            var valid = rule.TestRuleValid(item, possibility);
            if (valid)
            {
                newPossibilities.Add(possibility);
            }
        }

        possibilities = newPossibilities;

        foreach (var possibility in possibilities)
        {
           XLogger.Log(Category.Cell,$"cell ({x},{y}) has {possibility.name}");
        }
    }

    public void Collapse()
    {
        // change sprite
        var item = GetRandomItem();
        XLogger.Log(Category.WFC, $"cell ({x},{y}) collapsed with item {item}");
        spriteRenderer.sprite = item.image;
        spriteRenderer.color = Color.white;
        collapsed = true;

        // update neighbour's possibilities
        var top = gridSystem.GetCell(x, y + 1);
        if (top)
            top.UpdatePossibilities(item.topRule, item);

        var down = gridSystem.GetCell(x, y - 1);
        if (down)
            down.UpdatePossibilities(item.downRule, item);

        var left = gridSystem.GetCell(x - 1, y);
        if (left)
            left.UpdatePossibilities(item.leftRule, item);

        var right = gridSystem.GetCell(x + 1, y);
        if (right)
            right.UpdatePossibilities(item.rightRule, item);
    }

    public override string ToString()
    {
        return $"cell ({x},{y})";
    }
}