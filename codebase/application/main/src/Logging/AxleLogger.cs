﻿using System;
using System.Diagnostics;
using System.IO;

namespace Axle.Logging
{
    internal sealed class AxleLogger : ILogger
    {
        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static readonly object _syncRoot = new object();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _enableConsoleLogs = true;
        #endif

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type _targetType;

        public AxleLogger(Type targetType)
        {
            _targetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
        private static void LogToConsole(ILogEntry message)
        {
            lock (_syncRoot)
            lock (typeof(Console))
            lock (Console.Out)
            lock (Console.Error)
            {
                Console.ResetColor();
                var oldText = Console.ForegroundColor;
                var oldBg = Console.BackgroundColor;
                ConsoleColor primaryColor   = ConsoleColor.Magenta,       primaryBackground   = oldBg,
                             secondaryColor = ConsoleColor.DarkMagenta,   secondaryBackground = oldBg,
                             defaultColor   = oldText,                    defaultBackground   = oldBg;
                TextWriter writer = Console.Out;
                var abbr = "  msg";

                switch (message.Severity)
                {
                    case LogSeverity.Debug:
                        primaryColor = ConsoleColor.DarkCyan;
                        secondaryColor = ConsoleColor.Blue;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = "debug";
                        break;
                    case LogSeverity.Info:
                        primaryColor = ConsoleColor.Green;
                        secondaryColor = ConsoleColor.Cyan;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = " info";
                        break;
                    case LogSeverity.Warning:
                        primaryColor = ConsoleColor.Yellow;
                        secondaryColor = ConsoleColor.DarkYellow;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = " warn";
                        break;
                    case LogSeverity.Error:
                        primaryColor = ConsoleColor.Red;
                        secondaryColor = ConsoleColor.DarkRed;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        abbr = "error";
                        break;
                    case LogSeverity.Fatal:
                        primaryColor = ConsoleColor.Yellow;
                        secondaryColor = ConsoleColor.DarkYellow;
                        defaultColor = ConsoleColor.White;
                        primaryBackground = ConsoleColor.DarkRed;
                        secondaryBackground = ConsoleColor.DarkRed;
                        defaultBackground = ConsoleColor.DarkRed;
                        writer = Console.Error;
                        abbr = "fatal";
                        break;
                    default:
                        primaryColor = ConsoleColor.Cyan;
                        secondaryColor = ConsoleColor.Blue;
                        primaryBackground = oldBg;
                        secondaryBackground = oldBg;
                        break;
                }
                try
                {
                    var timestampText = string.Format("{0:yyyy MMM dd HH:mm:ss}", message.Timestamp);
                    #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
                    var preLength = Math.Max(timestampText.Length, message.ThreadID.Length + abbr.Length + 1);
                    var threadName = new System.Text.StringBuilder()
                        .Append(message.ThreadID)
                        .Append(' ', preLength - message.ThreadID.Length - abbr.Length - 1)
                        .ToString();
                    timestampText = new System.Text.StringBuilder()
                        .Append(timestampText)
                        .Append(' ', preLength - timestampText.Length)
                        .ToString();
                    #else
                    var preLength = timestampText.Length;
                    var threadName = string.Empty;
                    #endif

                    Console.ForegroundColor = secondaryColor;
                    Console.BackgroundColor = secondaryBackground;
                    writer.Write("{0} {1}", timestampText, message.Type.FullName);
                    writer.Flush();

                    Console.ForegroundColor = primaryColor;
                    Console.BackgroundColor = primaryBackground;
                    writer.Write("\n{0} {1} ", threadName, abbr);
                    writer.Flush();

                    Console.ForegroundColor = defaultColor;
                    Console.BackgroundColor = defaultBackground;
                    writer.Write(message.Message);
                    writer.Flush();

                    if (message.Exception != null)
                    {
                        Console.ForegroundColor = secondaryColor;
                        Console.BackgroundColor = secondaryBackground;
                        const string exceptionPrefix = "exception";
                        writer.Write(
                            "\n{0} {1}",
                            new System.Text.StringBuilder()
                                .Append(exceptionPrefix)
                                .Append(' ', preLength - exceptionPrefix.Length)
                                .ToString(),
                            message.Exception.GetType().FullName);
                        Console.ForegroundColor = defaultColor;
                        Console.BackgroundColor = defaultBackground;
                        writer.Write("{0}\n{1}", message.Exception.Message, message.Exception.StackTrace);
                        writer.Flush();
                    }
                }
                finally
                {
                    writer.WriteLine();
                    writer.Flush();
                    Console.ForegroundColor = oldText;
                    Console.BackgroundColor = oldBg;
                }
            }
        }
        #endif

        private static void LogToDebug(ILogEntry message) => Debug.WriteLine(message);

        #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
        private static void LogToTrace(ILogEntry message) => Trace.WriteLine(message);
        #endif

        void ILogger.Write(ILogEntry entry)
        {
            #if NETSTANDARD1_6_OR_NEWER || NETFRAMEWORK
            var action = entry.Severity == LogSeverity.Debug ? new Action<ILogEntry>(LogToDebug) : LogToTrace;
            #else
            var action = new Action<ILogEntry>(LogToDebug);
            #endif
            #if NETSTANDARD1_3_OR_NEWER || NETFRAMEWORK
            if (_enableConsoleLogs)
            {
                action += LogToConsole;
            }
            #endif
            action(entry);
        }
        
        Type ILogger.TargetType => _targetType;
    }
}