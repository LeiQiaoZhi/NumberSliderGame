using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rule Only Contains", menuName = "Rules/OnlyContains")]
public class RuleOnlyContains : Rule
{
    public List<WFCItem> onlyContains;
    public override bool TestRuleValid(WFCItem self, WFCItem other)
    {
        return onlyContains.Contains(other);
    }
}
