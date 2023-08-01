using UnityEngine;

[CreateAssetMenu(fileName = "Rule Not", menuName = "Rules/Not")]
public class RuleNot : Rule
{
    public Rule negateAgainst;
    public override bool TestRuleValid(WFCItem self, WFCItem other)
    {
        return !negateAgainst.TestRuleValid(self, other);
    }
}