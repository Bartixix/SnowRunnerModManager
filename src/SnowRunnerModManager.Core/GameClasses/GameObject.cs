using SnowRunnerModManager.Utils.Metadata;
using System.Text.Json.Serialization;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class GameObject : IModificationTrackable
    {
        private ModificationState _state;

        [JsonIgnore]
        public ModificationState State
        {
            get { return _state; }
            private set
            {
                if (value == ModificationState.Modified && _state != ModificationState.Custom)
                    _state = value;
            }
        }

        [XMLSpecial(SpecialOp.OriginalState)]
        public required ModificationState StateInit { get { return _state; } init { _state = value; } }

        [XMLElement("#$||%FileName")]
        public required string Name { get; init; }
        [XMLSpecial(SpecialOp.FileRelative)]
        public required string Origin { get; init; }

        [EditorField("Price", -3)]
        [XMLElement("GameData#$")]
        public required int Price { get; set; }
        [EditorField("Unlocked by exploration", -2)]
        [XMLElement("GameData#$", DefaultValue = "False")]
        public required bool UnlockByExploration { get; set; }
        [EditorField("Unlocked by rank", -1)]
        [XMLElement("GameData#$")]
        public required int UnlockByRank { get; set; }

        public void SetModified() => State = ModificationState.Modified;
    }
}
