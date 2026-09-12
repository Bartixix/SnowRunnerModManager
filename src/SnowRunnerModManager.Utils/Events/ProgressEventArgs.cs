using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Utils.Events
{
    public class ProgressEventArgs(string id, string message, double value, ProgressEventArgs.ProgressState state, bool isIndeterminate = false) : EventArgs
    {
        public enum ProgressState
        {
            Started,
            Running,
            Completed,
            Failed
        }

        public string Id { get; } = id;
        public string Message { get; } = message;
        public double Value { get; } = value;
        public bool IsIndeterminate { get; } = isIndeterminate;
        public ProgressState State { get; set; } = state;
    }
}
