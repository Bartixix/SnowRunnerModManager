using System.Text.Json.Serialization;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class Template : IModificationTrackable
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

        [XMLElement("#_template")]
        public required Template? Include { get; init; }

        //[XMLElement("", Special = SpecialResolver.AllChildren)]
        //public required List<TemplateProperty> Properties { get; init; }

        public void SetModified() => State = ModificationState.Modified;

    }
}
