using System;
using System.Collections.Generic;
using System.Text;
using static SnowRunnerModManager.Utils.Events.MessageEventArgs;

namespace SnowRunnerModManager.Utils.Events
{
    public class AppEvents
    {
        public static event EventHandler<MessageEventArgs>? OnMessage;
        public static event EventHandler<ProgressEventArgs>? OnProgress;

        public static void RaiseMessage(object? sender, MessageEventArgs e) => OnMessage?.Invoke(sender, e);
        public static void RaiseProgress(object? sender, ProgressEventArgs e) => OnProgress?.Invoke(sender, e);
    }
}
