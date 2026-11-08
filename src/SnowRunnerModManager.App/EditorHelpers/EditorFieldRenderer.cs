using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorUserControls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SnowRunnerModManager.App.EditorHelpers
{
    public class EditorFieldRenderer<T>(T target) where T : IModificationTrackable
    {
        private readonly T _target = target;
        //private readonly StackPanel _panel;

        public static Type GameObjectType { get; } = typeof(T);

        private Editor RenderedEditor { get; } = new();

        public bool Rendered { get; private set; } = false;

        public async Task<T> Render()
        {
            if (Rendered)
                throw new InvalidOperationException();

            Rendered = true;

            T temp = ObjectHelper.Clone(_target);
            temp.SetModified();

            List<Control> controls = EditorFieldHelper.GetControls(temp, 0);
            RenderedEditor.EditorFieldsPanel.Children.AddRange(controls);

            await RenderedEditor.ShowDialog(MainWindow.MWindow);

            if (RenderedEditor.IsSaved)
                return temp;

            return _target;
        }
    }
}
