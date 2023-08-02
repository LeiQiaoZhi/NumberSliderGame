using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Rule Only Contains", menuName = "Rules/OnlyContains")]
public class RuleOnlyContains : Rule
{
    public List<WfcItem> onlyContains;
    public override bool TestRuleValid(WfcItem _self, WfcItem _other)
    {
        return onlyContains.Contains(_other);
    }
}
