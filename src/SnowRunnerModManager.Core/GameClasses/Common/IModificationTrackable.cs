using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.GameClasses.Common
{
    public enum ModificationState
    {
        Original,
        Modified,
        Custom
    }

    public interface IModificationTrackable
    {
        public ModificationState State { get; }
        public void SetModified();
    }
}
