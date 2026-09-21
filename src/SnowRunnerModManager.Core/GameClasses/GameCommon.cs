using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class GameCommon : IModificationTrackable
    {
        private ModificationState _state;
        public ModificationState State
        {
            get { return _state; }
            private set
            {
                if (value == ModificationState.Modified && _state != ModificationState.Custom)
                    _state = value;
            }
        }

        public string Name { get; set; }

        [EditorField("Price", -3)]
        public int Price { get; set; }
        [EditorField("Unlocked by exploration", -2)]
        public bool UnlockedByExploration { get; set; }
        [EditorField("Unlocked by rank", -1)]
        public int UnlockByRank { get; set; }

        public void SetModified() => State = ModificationState.Modified;
    }
}
