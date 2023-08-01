using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rule Edge", menuName = "Rules/Edge")]
public class RuleEdge : Rule
{
    /// <summary>
    /// wrt self
    /// </summary>
    public DirectionEnum direction;

    public override bool TestRuleValid(WfcItem _selfBase, WfcItem _otherBase)
    {
        var self = _selfBase as WfcItemEdges;
        var other = _otherBase as WfcItemEdges;
        if (self != null && other != null)
        {
            List<EdgeType> selfEdges = new List<EdgeType>();
            List<EdgeType> otherEdges = new List<EdgeType>();
            switch (direction)
            {
                case DirectionEnum.Top:
                    selfEdges = self.topEdges;
                    otherEdges = other.downEdges;
                    break;
                case DirectionEnum.Down:
                    selfEdges = self.downEdges;
                    otherEdges = other.topEdges;
                    break;
                case DirectionEnum.Left:
                    selfEdges = self.leftEdges;
                    otherEdges = other.rightEdges;
                    break;
                case DirectionEnum.Right:
                    selfEdges = self.rightEdges;
                    otherEdges = other.leftEdges;
                    break;
            }

            if (selfEdges.Count != otherEdges.Count)
            {
                return false;
            }

            for (int i = 0; i < selfEdges.Count; i++)
            {
                if (selfEdges[i] != otherEdges[otherEdges.Count - 1 - i])
                {
                    return false;
                }
            }

            return true;
        }

        XLogger.LogWarning(Category.WFC, "arguments can't be casted to WFCItemEdges");
        return false;
    }
}

public enum DirectionEnum
{
    Top,
    Down,
    Left,
    Right
}