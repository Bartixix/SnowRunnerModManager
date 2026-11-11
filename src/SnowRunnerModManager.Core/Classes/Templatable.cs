using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.GameClasses.Common;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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

    public class Templatable<TValue, TTemplate> : IModificationTrackable, ICommonObject
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

        public string Name { get; private set; }

        public TemplatableType TemplateMode { get; private set; }
        public TTemplate? TemplateValue { get; private set; }
        public TValue? LocalValue { get; private set; }

        // Managed by GameConatiner
        public TTemplate? ParentTemplate { get; set; } = null;

        public bool IsOverridable { get; init; } = true;

        public Templatable(string name)
        {
            Name = name;
            TemplateMode = TemplatableType.Empty;
            LocalValue = default;
            TemplateValue = null;
        }

        public Templatable(string name, TValue value)
        {
            Name = name;
            TemplateMode = TemplatableType.Overriden;
            LocalValue = value;
            TemplateValue = null;
        }

        public Templatable(string name, TTemplate template)
        {
            Name = name;
            TemplateMode = TemplatableType.Templated;
            LocalValue = default;
            TemplateValue = template;
        }

        public Templatable(string name, TValue value, TTemplate template)
        {
            Name = name;
            TemplateMode = TemplatableType.Overriden;
            LocalValue = value;
            TemplateValue = template;
        }

        public void SetModified() => State = ModificationState.Modified;

        public bool TryResolveTemplate([NotNullWhen(true)] out TValue? value)
        {
            value = default;

            if (LocalValue != null)
            {
                value = LocalValue;
                return true;
            }

            if (TemplateValue == null)
                return false;

            if (!TryResolveLowLevel(TemplateValue, out value))
                return false;

            return true;
        }

        private bool TryResolveLowLevel(TTemplate initial, [NotNullWhen(true)] out TValue? value)
        {
            value = default;
            Type tType = typeof(TTemplate);

            PropertyInfo? property = tType.GetProperty(Name, BindingFlags.Instance | BindingFlags.Public);

            while (true)
            {
                if (property == null)
                    return false;

                if (!property.CanRead)
                    return false;

                if (property.GetValue(initial) is TValue tVal)
                {
                    if (tVal != null)
                    {
                        value = tVal;
                        return true;
                    }

                    if (initial.Include == null)
                        return false;

                    initial = initial.Include;
                    continue;
                }

                return false;
            }
        }
    }
}
