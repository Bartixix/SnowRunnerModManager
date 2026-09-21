using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;

using System.Collections.Generic;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorSimpleClassControl : EditorUserControl, ITopLevelSavable
    {
        public bool ListItemOverride { get; init; } = false;

        public EditorSimpleClassControl()
        {
            InitializeComponent();

            Initialized += SetupFields;
        }

        public void Save()
        {
            foreach (Control control in MainPanel.Children)
            {
                if (control is not ITopLevelSavable editorUserControl)
                    continue;

                editorUserControl.Save();
            }
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            InputHeader.Text = $"{FieldName}:";

            object obj = ListItemOverride ? Target : Parameter.GetValue(Target)!;

            List<Control> controls = EditorFieldHelper.GetControls(obj, Depth);

            MainPanel.Children.AddRange(controls);
        }
    }
}