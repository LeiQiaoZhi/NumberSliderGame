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
        Wfc,
        Cell,
        Movement,
        GameManager,
        Generation,
        Tween,
        Score,
        Menu,
        LevelBonus
    }
    
    public static class XLogger
    {
        private const string InfoColor = nameof(Color.white);
        private const string WarningColor = nameof(Color.yellow);
        private const string ErrorColor = nameof(Color.red);

        [Conditional("DEBUG")]
        public static void Log(object _message)
        {
            Debug.Log(FormatMessage(InfoColor, _message));
        }

        [Conditional("DEBUG")]
        public static void Log(Category _category, object _message)
        {
            Debug.Log(FormatMessageWithCategory(InfoColor, _category, _message));
        }

        [Conditional("DEBUG")]
        public static void LogFormat(string _format, params object[] _args)
        {
            Debug.Log(FormatMessage(InfoColor, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogFormat(Category _category, string _format, params object[] _args)
        {
            Debug.Log(FormatMessageWithCategory(InfoColor, _category, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogWarning(object _message)
        {
            Debug.LogWarning(FormatMessage(WarningColor, _message));
        }

        [Conditional("DEBUG")]
        public static void LogWarning(Category _category, object _message)
        {
            Debug.LogWarning(FormatMessageWithCategory(WarningColor, _category, _message));
        }

        [Conditional("DEBUG")]
        public static void LogWarningFormat(string _format, params object[] _args)
        {
            Debug.LogWarningFormat(FormatMessage(WarningColor, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogWarningFormat(Category _category, string _format, params object[] _args)
        {
            Debug.LogWarningFormat(FormatMessageWithCategory(WarningColor, _category, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogError(object _message)
        {
            Debug.LogError(FormatMessage(ErrorColor, _message));
        }

        [Conditional("DEBUG")]
        public static void LogError(Category _category, object _message)
        {
            Debug.LogError(FormatMessageWithCategory(ErrorColor, _category, _message));
        }

        [Conditional("DEBUG")]
        public static void LogErrorFormat(string _format, params object[] _args)
        {
            Debug.LogErrorFormat(FormatMessage(ErrorColor, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogErrorFormat(Category _category, string _format, params object[] _args)
        {
            Debug.LogErrorFormat(FormatMessageWithCategory(ErrorColor, _category, string.Format(_format, _args)));
        }

        [Conditional("DEBUG")]
        public static void LogException(Exception _exception)
        {
            Debug.LogError(FormatMessage(ErrorColor, _exception.Message));
        }

        [Conditional("DEBUG")]
        public static void LogException(Category _category, Exception _exception)
        {
            Debug.LogError(FormatMessageWithCategory(ErrorColor, _category, _exception.Message));
        }

        private static string FormatMessage(string _color, object _message)
        {
            return $"<color={_color}>{_message}</color>";
        }

        private static string FormatMessageWithCategory(string _color, Category _category, object _message)
        {
            return $"<color={_color}><b>[{_category.ToString()}]</b> {_message}</color>";
        }
    }

  
}