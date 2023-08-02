using System.Collections;

public class DebugCommand
{
    public readonly string commandName;
    public delegate string DebugAction(DebugConsole _debugConsole);

    public readonly DebugAction action;

    // constructor
    public DebugCommand(string _name, DebugAction _a)
    {
        commandName = _name;
        action = _a;
    }

    public string Raise(DebugConsole _debugConsole)
    {
        return action.Invoke(_debugConsole);
    }
}