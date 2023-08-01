using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WfcCell 
{
    private Cell cell_;

    private List<WFCItem> possibilities_;
    private readonly WaveFunctionCollapse waveFunctionCollapse_;

    private int x_;
    private int y_;

    public WfcCell(List<WFCItem> _possibleItems, WaveFunctionCollapse _waveFunctionCollapse, Cell _cell)
    {
        possibilities_ = _possibleItems;
        waveFunctionCollapse_ = _waveFunctionCollapse;
        cell_ = _cell;
        x_ = _cell.x;
        y_ = _cell.y;
    }

    /// <returns> number of possibilities</returns>
    public int GetEntropy()
    {
        return possibilities_.Count;
    }

    private WFCItem GetRandomItem()
    {
        return possibilities_[Random.Range(0, possibilities_.Count)];
    }

    public List<WfcCell> GetNeighbours()
    {
        List<WfcCell> neighbours = new List<WfcCell>();
        var top = waveFunctionCollapse_.GetCell(x_, y_ + 1);
        if (top != null)
            neighbours.Add(top);

        var down = waveFunctionCollapse_.GetCell(x_, y_ - 1);
        if (down != null)
            neighbours.Add(down);

        var left = waveFunctionCollapse_.GetCell(x_ - 1, y_);
        if (left != null)
            neighbours.Add(left);

        var right = waveFunctionCollapse_.GetCell(x_ + 1, y_);
        if (right != null)
            neighbours.Add(right);

        return neighbours;
    }

    /// <summary>
    /// update this cell's possibilities based on item's rule
    /// </summary>
    private void UpdatePossibilities(Rule _rule, WFCItem _item)
    {
        XLogger.Log(Category.Cell, $"applying {_rule} to {cell_}");
        var newPossibilities = new List<WFCItem>();
        foreach (WFCItem possibility in possibilities_)
        {
            var valid = _rule.TestRuleValid(_item, possibility);
            if (valid)
            {
                newPossibilities.Add(possibility);
            }
        }

        possibilities_ = newPossibilities;

        foreach (var possibility in possibilities_)
        {
           XLogger.Log(Category.Cell,$"{cell_} has {possibility.name}");
        }
    }

    public void Collapse()
    {
        // change sprite
        WFCItem item = GetRandomItem();
        XLogger.Log(Category.WFC, $"{cell_} collapsed with item {item}");
        cell_.SetSprite(item.image);

        // update neighbour's possibilities
        WfcCell top = waveFunctionCollapse_.GetCell(x_, y_ + 1);
        if (top != null)
            top.UpdatePossibilities(item.topRule, item);

        WfcCell down = waveFunctionCollapse_.GetCell(x_, y_ - 1);
        if (down != null)
            down.UpdatePossibilities(item.downRule, item);

        WfcCell left = waveFunctionCollapse_.GetCell(x_ - 1, y_);
        if (left != null)
            left.UpdatePossibilities(item.leftRule, item);

        WfcCell right = waveFunctionCollapse_.GetCell(x_ + 1, y_);
        if (right != null)
            right?.UpdatePossibilities(item.rightRule, item);
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