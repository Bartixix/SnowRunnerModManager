using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;
using SnowRunnerModManager.App.Windows;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorFieldControl : EditorUserControl
    {
        public EditorFieldControl()
        {
            InitializeComponent();

            Initialized += SetupField;
        }

        public override void Save()
        {
            if (EditorFieldHelper.GetSimpleControlValue(MainControl!, Parameter.PropertyType, out object? value))
            {
                Parameter.SetValue(Target, value);
                return;
            }

            throw new Exception();
        }

        private void SetupField(object? sender, EventArgs e)
        {
            SetupGrid();

            InputHeader.Text = $"{FieldName}:";

            object obj = Parameter.GetValue(Target)!;

            Control inputField = EditorFieldHelper.GetSimpleTypeControl(obj) ?? throw new Exception();
            Grid.SetColumn(inputField, 2);

            MainControl = inputField;

            MainGrid.Children.Add(inputField);
        }

        private void SetupGrid()
        {
            int marginLeft = Depth * Editor.MarginPerDepth;

            MainGrid.ColumnDefinitions.Add(new() { Width = new(marginLeft), MinWidth = marginLeft, MaxWidth = marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = new(300 - marginLeft), MinWidth = 300 - marginLeft, MaxWidth = 300 - marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = GridLength.Star, MinWidth = 300 });
        }
    }
}