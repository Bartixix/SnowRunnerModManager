using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;

using System.Collections.Generic;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorSimpleClassControl : EditorUserControl
    {
        public EditorSimpleClassControl()
        {
            InitializeComponent();

            Initialized += SetupFields;
        }

        public override void Save()
        {
            foreach (Control control in MainPanel.Children)
            {
                if (control is not EditorUserControl editorUserControl)
                    continue;

                editorUserControl.Save();
            }
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            InputHeader.Text = $"{FieldName}:";

            object obj = Parameter.GetValue(Target)!;
            List<Control> controls = EditorFieldHelper.GetControls(obj, Depth);

            MainPanel.Children.AddRange(controls);
        }
    }
}