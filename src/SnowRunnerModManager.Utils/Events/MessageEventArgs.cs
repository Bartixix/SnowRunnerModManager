using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Events
{
    public class MessageEventArgs(string message, MessageEventArgs.MessageSeverity severity, bool debugOnly = false) : EventArgs
    {
        public enum MessageSeverity
        {
            Success,
            Info,
            Warning,
            Error,
            Critical
        }

        public string Message { get; } = message;
        public MessageSeverity Severity { get; } = severity;
        public bool DebugOnly { get; } = debugOnly;
    }
}
