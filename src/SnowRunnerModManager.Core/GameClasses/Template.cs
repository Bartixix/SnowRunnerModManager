using System.Text.Json.Serialization;
using SnowRunnerModManager.Core.GameClasses.Common;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class Template : IModificationTrackable, ICommonObject
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

        [XMLSpecial(SpecialOp.TagName)]
        public required string Name { get; init; }

        public Template? TypelessInclude { get; protected set; }

        public void SetModified() => State = ModificationState.Modified;
    }

    public abstract class Template<T> : Template where T : Template<T>
    {
        [XMLElement("#_template")]
        public required T? Include { get { return (T?)TypelessInclude; } set { TypelessInclude = value; } }
    }
}
