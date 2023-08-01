using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class WFCCell 
{
    private Cell cell_;

    private List<WfcItem> possibilities_;
    private WaveFunctionCollapse waveFunctionCollapse_;

    private int x_;
    private int y_;

    public WFCCell(List<WfcItem> _possibleItems, WaveFunctionCollapse _waveFunctionCollapse, Cell _cell)
    {
        possibilities_ = _possibleItems;
        waveFunctionCollapse_ = _waveFunctionCollapse;
        cell_ = _cell;
    }

    /// <returns> number of possibilities</returns>
    public int GetEntropy()
    {
        return possibilities_.Count;
    }

    private WfcItem GetRandomItem()
    {
        return possibilities_[Random.Range(0, possibilities_.Count)];
    }

    public List<WFCCell> GetNeighbours()
    {
        List<WFCCell> neighbours = new List<WFCCell>();
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
    private void UpdatePossibilities(Rule _rule, WfcItem _item)
    {
        XLogger.Log(Category.Cell, $"applying {_rule} to {cell_}");
        var newPossibilities = new List<WfcItem>();
        foreach (var possibility in possibilities_)
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
        var item = GetRandomItem();
        XLogger.Log(Category.WFC, $"{cell_} collapsed with item {item}");

        // update neighbour's possibilities
        var top = waveFunctionCollapse_.GetCell(x_, y_ + 1);
        top?.UpdatePossibilities(item.topRule, item);

        var down = waveFunctionCollapse_.GetCell(x_, y_ - 1);
        down?.UpdatePossibilities(item.downRule, item);

        var left = waveFunctionCollapse_.GetCell(x_ - 1, y_);
        left?.UpdatePossibilities(item.leftRule, item);

        var right = waveFunctionCollapse_.GetCell(x_ + 1, y_);
        right?.UpdatePossibilities(item.rightRule, item);
    }
    
}