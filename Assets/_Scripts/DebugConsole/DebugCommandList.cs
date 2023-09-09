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
            const string help =
                "Actions: [Tab] to toggle debug console; [Enter] to enter command\n" +
                "Commands: help -- show help, quit -- close console, unlock -- unlock all levels, reset -- reset all levels";
            return help;
        });

    public static DebugCommand unlockAllLevelsCommand =
        new DebugCommand("unlock", (_console) =>
        {
            XLogger.LogWarning(Category.DebugConsole, "Unlock All Levels Command");
            LevelSaveHandler.ForceSetLevel(999);
            GameManager.Instance.RestartGame();
            return "All levels unlocked";
        });

    public static DebugCommand resetAllLevelsCommand =
        new DebugCommand("reset", (_console) =>
        {
            XLogger.LogWarning(Category.DebugConsole, "Reset All Levels Command");
            LevelSaveHandler.ForceSetLevel(1);
            GameManager.Instance.RestartGame();
            return "All levels reset";
        });
}