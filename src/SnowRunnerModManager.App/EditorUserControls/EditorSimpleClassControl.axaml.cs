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
        public int Depth { get; init; } = 0;

        public EditorSimpleClassControl()
        {
            InitializeComponent();

            Loaded += SetupFields;
        }

        private void SetupFields(object? sender, RoutedEventArgs e)
        {
            List<Control> controls = EditorFieldHelper.GetControls(Target, Depth);

            MainPanel.Children.AddRange(controls);
        }
    }
}