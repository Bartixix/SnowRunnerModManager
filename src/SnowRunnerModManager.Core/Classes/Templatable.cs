using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SnowRunnerModManager.Core.Classes
{
    public abstract class Templatable<T> : IModificationTrackable
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

        public bool IsTemplateValue { get; private set; }
        public Template? TemplateValue { get; private set; }
        public T? LocalValue { get; private set; }

        public bool IsOverridable { get; init; } = true;

        public Templatable(T value)
        {
            IsTemplateValue = false;
            LocalValue = value;
            TemplateValue = null;
        }

        public Templatable(Template template)
        {
            IsTemplateValue = true;
            LocalValue = default;
            TemplateValue = template;
        }

        public Templatable(T value, Template template)
        {
            IsTemplateValue = false;
            LocalValue = value;
            TemplateValue = template;
        }

        public void SetModified() => State = ModificationState.Modified;
    }
}
