using SnowRunnerModManager.Utils.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SnowRunnerModManager.Utils.Helpers
{
    public class ProgressHelper
    {
        private Guid Id { get; }
        public string Message { get; private set; }
        private int _progress;
        public int Progress { get { return _progress; } private set { _progress = value; } }
        public double RealProgress { get { return 1D / Max * Progress * 100; } }
        public int Max { get; }
        public bool IsIndeterminate { get; set; } = false;
        public ProgressEventArgs.ProgressState State { get; private set; }

        public ProgressHelper(string message, int progress, int max, ProgressEventArgs.ProgressState state = ProgressEventArgs.ProgressState.Started)
        {
            Id = Guid.NewGuid();
            Message = message;
            Progress = progress;
            Max = max;
            State = state;

            Update();
        }

        public void Update() => AppEvents.RaiseProgress(this, new(Id.ToString(), Message, RealProgress, State, IsIndeterminate));

        public void Update(int progress)
        {
            Progress = progress;

            if (RealProgress > 0)
                State = ProgressEventArgs.ProgressState.Running;

            Update();
        }

        public void Update(string message)
        {
            Message = message;
            Update();
        }

        public void Update(int progress, string message)
        {
            Progress = progress;
            Message = message;

            Update();
        }

        public void Increment()
        {
            Interlocked.Increment(ref _progress);
            AppEvents.RaiseProgress(this, new(Id.ToString(), Message, 1D / Max * _progress * 100, State, IsIndeterminate));
        }

        public void Fail() => AppEvents.RaiseProgress(this, new(Id.ToString(), Message, RealProgress, ProgressEventArgs.ProgressState.Failed, IsIndeterminate));
        public void Finish() => AppEvents.RaiseProgress(this, new(Id.ToString(), Message, RealProgress, ProgressEventArgs.ProgressState.Completed, IsIndeterminate));
    }
}
