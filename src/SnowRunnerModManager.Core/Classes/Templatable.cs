using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.GameClasses.Common;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SnowRunnerModManager.Core.Classes
{
    public enum TemplatableType
    {
        Empty,
        Templated,
        Overriden
    }

    public class Templatable<TValue, TTemplate> : IModificationTrackable
        where TTemplate : Template<TTemplate>
    {
        private ModificationState _state = ModificationState.Original;

        public ModificationState State
        {
            get { return _state; }
            private set
            {
                if (value == ModificationState.Modified && _state != ModificationState.Custom)
                    _state = value;
            }
        }

        public TemplatableType TemplateMode { get; private set; }
        public TTemplate? TemplateValue { get; private set; }
        public TValue? LocalValue { get; private set; }

        public bool IsOverridable { get; init; } = true;

        public Templatable()
        {
            TemplateMode = TemplatableType.Empty;
            LocalValue = default;
            TemplateValue = null;
        }

        public Templatable(TValue value)
        {
            TemplateMode = TemplatableType.Overriden;
            LocalValue = value;
            TemplateValue = null;
        }

        public Templatable(TTemplate template)
        {
            TemplateMode = TemplatableType.Templated;
            LocalValue = default;
            TemplateValue = template;
        }

        public Templatable(TValue value, TTemplate template)
        {
            TemplateMode = TemplatableType.Overriden;
            LocalValue = value;
            TemplateValue = template;
        }

        public void SetModified() => State = ModificationState.Modified;
    }
}
