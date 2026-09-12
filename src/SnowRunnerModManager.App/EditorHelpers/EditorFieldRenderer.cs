using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.App.EditorHelpers
{
    public partial class EditorFieldRenderer<T> where T : GameCommon
    {
        private readonly T _target;
        private readonly StackPanel _panel;

        public EditorFieldRenderer(T target, StackPanel panel)
        {
            _target = target;
            _panel = panel;

            Render();
        }

        private void Render()
        {

        }
    }
}
