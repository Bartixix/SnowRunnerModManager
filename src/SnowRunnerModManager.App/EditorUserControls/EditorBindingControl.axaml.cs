using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorBindingControl : EditorUserControl
    {
        public required IRegistry BoundRegistry { get; init; }
        public required bool MultiChoice { get; init; }


        private string DisplayString { get; set; } = string.Empty;
        private List<string> Enabled { get; set; } = [];
        //private Butt? _mainBox;
        //private TextBox MainBox { get { return _mainBox!; } }

        public EditorBindingControl()
        {
            InitializeComponent();

            Initialized += SetupField;
        }

        private void SetupField(object? sender, EventArgs e)
        {
            Enabled = (List<string>)Parameter.GetValue(Target)!;
            DisplayString = GetRegString(Enabled);

            SetupGrid();

            MainText.Text = FieldName;
            MainBtn.Content = $"Edit ({DisplayString})";
        }

        //private void SetupField(object? sender, EventArgs e)
        //{
        //    Enabled = (List<string>)Parameter.GetValue(Target)!;
        //    DisplayString = GetRegString(Enabled);

        //    PropertyInfo displayStringInfo = typeof(EditorBindingControl).GetProperty(nameof(DisplayString), BindingFlags.NonPublic | BindingFlags.Instance)!;

        //    EditorFieldControl editorFieldControl = new()
        //    {
        //        FieldName = FieldName,
        //        Parameter = displayStringInfo,
        //        Target = this,
        //        Depth = Depth,
        //    };

        //    editorFieldControl.Initialized += OnBoxInit;

        //    MainPanel.Children.Add(editorFieldControl);

        //    MainControl = editorFieldControl;
        //}

        //private void OnBoxInit(object? sender, EventArgs e)
        //{
        //    if (MainControl is not EditorFieldControl efc || efc.MainControl is not TextBox mainBox)
        //        throw new Exception();

        //    _mainBox = mainBox;

        //    MainBox.IsReadOnly = true;
        //    MainBox.DoubleTapped += OpenBindingEditor;
        //    MainBox.Text = DisplayString;
        //}

        private async void OpenBindingEditor(object? sender, RoutedEventArgs e)
        {
            Window window = (Window?)TopLevel.GetTopLevel(this) ?? throw new Exception();

            BindingEditor editor = new() { BindableNames = [.. BoundRegistry.ItemNames], Enabled = Enabled, MultiChoice = MultiChoice };
            await editor.ShowDialog(window);

            DisplayString = GetRegString(Enabled);
            MainBtn.Content = $"Edit ({DisplayString})";
        }

        private static string GetRegString(ICollection<string> regCollection) => string.Join(", ", regCollection);

        private void SetupGrid()
        {
            int marginLeft = Depth * Editor.MarginPerDepth;

            MainGrid.ColumnDefinitions.Add(new() { Width = new(marginLeft), MinWidth = marginLeft, MaxWidth = marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = new(300 - marginLeft), MinWidth = 300 - marginLeft, MaxWidth = 300 - marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = GridLength.Star, MinWidth = 300 });
        }
    }
}