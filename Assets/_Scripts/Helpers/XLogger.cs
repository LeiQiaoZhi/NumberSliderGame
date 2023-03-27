using System;
using System.Diagnostics;

namespace UnityEngine
{
    public enum Category
    {
        Numeric,
        Audio,
        Animation,
        Physics,
        Scene,
        Level,
        Achievement,
        Ability,
        Weapon,
        UI,
        DebugConsole,
        Settings,
        GridSystem,
        WFC,
        Cell
    }
    
    public static class XLogger
    {
        private const string InfoColor = nameof(Color.white);
        private const string WarningColor = nameof(Color.yellow);
        private const string ErrorColor = nameof(Color.red);

        [Conditional("DEBUG")]
        public static void Log(object message)
        {
            Debug.Log(FormatMessage(InfoColor, message));
        }

        [Conditional("DEBUG")]
        public static void Log(Category category, object message)
        {
            Debug.Log(FormatMessageWithCategory(InfoColor, category, message));
        }

        [Conditional("DEBUG")]
        public static void LogFormat(string format, params object[] args)
        {
            Debug.Log(FormatMessage(InfoColor, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogFormat(Category category, string format, params object[] args)
        {
            Debug.Log(FormatMessageWithCategory(InfoColor, category, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogWarning(object message)
        {
            Debug.LogWarning(FormatMessage(WarningColor, message));
        }

        [Conditional("DEBUG")]
        public static void LogWarning(Category category, object message)
        {
            Debug.LogWarning(FormatMessageWithCategory(WarningColor, category, message));
        }

        [Conditional("DEBUG")]
        public static void LogWarningFormat(string format, params object[] args)
        {
            Debug.LogWarningFormat(FormatMessage(WarningColor, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogWarningFormat(Category category, string format, params object[] args)
        {
            Debug.LogWarningFormat(FormatMessageWithCategory(WarningColor, category, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogError(object message)
        {
            Debug.LogError(FormatMessage(ErrorColor, message));
        }

        [Conditional("DEBUG")]
        public static void LogError(Category category, object message)
        {
            Debug.LogError(FormatMessageWithCategory(ErrorColor, category, message));
        }

        [Conditional("DEBUG")]
        public static void LogErrorFormat(string format, params object[] args)
        {
            Debug.LogErrorFormat(FormatMessage(ErrorColor, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogErrorFormat(Category category, string format, params object[] args)
        {
            Debug.LogErrorFormat(FormatMessageWithCategory(ErrorColor, category, string.Format(format, args)));
        }

        [Conditional("DEBUG")]
        public static void LogException(Exception exception)
        {
            Debug.LogError(FormatMessage(ErrorColor, exception.Message));
        }

        [Conditional("DEBUG")]
        public static void LogException(Category category, Exception exception)
        {
            Debug.LogError(FormatMessageWithCategory(ErrorColor, category, exception.Message));
        }

        private static string FormatMessage(string color, object message)
        {
            return $"<color={color}>{message}</color>";
        }

        private static string FormatMessageWithCategory(string color, Category category, object message)
        {
            return $"<color={color}><b>[{category.ToString()}]</b> {message}</color>";
        }
    }

  
}