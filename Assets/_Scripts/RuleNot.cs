using UnityEngine;

[CreateAssetMenu(fileName = "Rule Not", menuName = "Rules/Not")]
public class RuleNot : Rule
{
    public Rule negateAgainst;
    public override bool TestRuleValid(WfcItem _self, WfcItem _other)
    {
        return !negateAgainst.TestRuleValid(_self, _other);
    }
}