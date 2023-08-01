using UnityEngine;

[CreateAssetMenu(fileName = "Rule Not", menuName = "Rules/Not")]
public class RuleNot : Rule
{
    public Rule negateAgainst;
    public override bool TestRuleValid(WFCItem _self, WFCItem _other)
    {
        return !negateAgainst.TestRuleValid(_self, _other);
    }
}