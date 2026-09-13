using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorFieldControl : EditorUserControl
    {
        public int MarginLeft { get; init; } = 0;

        public EditorFieldControl()
        {
            InitializeComponent();

            Loaded += SetupField;
        }

        private void SetupField(object? sender, RoutedEventArgs e)
        {
            SetupGrid();

            InputHeader.Text = $"{FieldName}:";

            Control inputField = EditorFieldHelper.GetSimpleTypeControl(Target) ?? throw new Exception();
            Grid.SetColumn(inputField, 2);
            MainGrid.Children.Add(inputField);
        }

        private void SetupGrid()
        {
            MainGrid.ColumnDefinitions.Add(new() { Width = new(MarginLeft), MinWidth = MarginLeft, MaxWidth = MarginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = new(300 - MarginLeft), MinWidth = 300 - MarginLeft, MaxWidth = 300 - MarginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = GridLength.Star, MinWidth = 300 });
        }
    }
}