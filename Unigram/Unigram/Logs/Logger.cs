using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Unigram.Logs
{
    public sealed class Logger
    {
        [Conditional("DEBUG")]
        public static void Critical(LoggerTag tag, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Log(tag, LogLevel.Critical, null, message, member, filePath, line);
        }

        //[Conditional("DEBUG")]
        //public static void Critical(object sender, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(LogLevel.Critical, sender.GetType(), message, member, filePath, line);
        //}

        [Conditional("DEBUG")]
        public static void Debug(LoggerTag tag, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Log(tag, LogLevel.Debug, null, message, member, filePath, line);
        }

        //[Conditional("DEBUG")]
        //public static void Debug(object sender, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(LogLevel.Debug, sender.GetType(), message, member, filePath, line);
        //}

        [Conditional("DEBUG")]
        public static void Error(LoggerTag tag, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Log(tag, LogLevel.Error, null, message, member, filePath, line);
        }

        //[Conditional("DEBUG")]
        //public static void Error(object sender, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(LogLevel.Error, sender.GetType(), message, member, filePath, line);
        //}

        [Conditional("DEBUG")]
        public static void Info(LoggerTag tag, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Log(tag, LogLevel.Info, null, message, member, filePath, line);
        }

        //[Conditional("DEBUG")]
        //public static void Info(object sender, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(LogLevel.Info, sender.GetType(), message, member, filePath, line);
        //}

        [Conditional("DEBUG")]
        private static void Log(LoggerTag tag, LogLevel level, Type type, string message, string member, string filePath, int line)
        {
            Logs.Log.Write(LogHelper.CreateEntryWithoutType(DateTime.Now, level, member, line, message));
        }

        //[Conditional("DEBUG")]
        //public static void LogLocation(object sender, LogLevel level = LogLevel.Info, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(level, sender.GetType(), string.Empty, member, filePath, line);
        //}

        [Conditional("DEBUG")]
        public static void Warning(LoggerTag tag, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        {
            Log(tag, LogLevel.Warning, null, message, member, filePath, line);
        }

        //[Conditional("DEBUG")]
        //public static void Warning(object sender, string message, [CallerMemberName] string member = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int line = 0)
        //{
        //    Log(LogLevel.Warning, sender.GetType(), message, member, filePath, line);
        //}
    }

    [Flags]
    public enum LoggerTag
    {
        Lifecycle,
        API,
        Chat,
        Notifications,
        Contacts
    }
}
