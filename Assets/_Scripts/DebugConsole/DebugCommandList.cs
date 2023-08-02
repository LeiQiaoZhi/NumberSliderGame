using UnityEngine;

public static class DebugCommandList
{
    public static DebugCommand testCommand =
        new DebugCommand("test", (_console) =>
        {
            XLogger.LogWarning(Category.DebugConsole, "TEST COMMAND");
            return "TEST COMMAND";
        });
    
    public static DebugCommand quitCommand =
        new DebugCommand("quit", (_console) =>
        {
            _console.console.SetActive(false);
            XLogger.LogWarning(Category.DebugConsole, "Close Debug Console");
            return "Console Closed";
        });
    
    public static DebugCommand helpCommand =
        new DebugCommand("help", (_console) =>
        {
            XLogger.LogWarning(Category.DebugConsole, "Help Command");
            var help = "Actions: [Tab] to toggle debug console; [Enter] to enter command\n" +
                       "Commands: help -- show help, quit -- close console";
            return help;
        });

}