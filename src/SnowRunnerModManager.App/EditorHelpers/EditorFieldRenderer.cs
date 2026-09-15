using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorUserControls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SnowRunnerModManager.App.EditorHelpers
{
    public partial class EditorFieldRenderer<T> where T : notnull
    {
        private readonly T _target;
        //private readonly StackPanel _panel;

        public static Type GameObjectType { get; } = typeof(T);

        private Editor RenderedEditor { get; }

        public EditorFieldRenderer(T target)
        {
            _target = target;
            RenderedEditor = new();

            Render();
        }

        private void Render()
        {
            List<Control> controls = EditorFieldHelper.GetControls(_target, 0);
            RenderedEditor.EditorFieldsPanel.Children.AddRange(controls);

            RenderedEditor.ShowDialog(MainWindow.MWindow);
        }
    }
}
