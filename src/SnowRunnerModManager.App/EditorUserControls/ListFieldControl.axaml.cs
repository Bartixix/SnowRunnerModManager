using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;
using SnowRunnerModManager.Utils.Metadata;
using System.Collections.Generic;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class ListFieldControl : EditorUserControl, ITopLevelSavable
    {
        public event EventHandler<RoutedEventArgs>? MoveUp;
        public event EventHandler<RoutedEventArgs>? MoveDown;
        public event EventHandler<RoutedEventArgs>? Remove;

        public ListFieldControl()
        {
            InitializeComponent();

            Initialized += SetupFields;
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            EditorSimpleClassControl control = new() { FieldName = FieldName, Parameter = Parameter, Target = Target, ListItemOverride = true };

            MainControl = control;

            SetupButtons(control);

            MainPanel.Children.Add(MainControl);
        }

        private void SetupButtons(EditorSimpleClassControl control)
        {
            StackPanel sp = new() { Orientation = Avalonia.Layout.Orientation.Horizontal };
            Grid.SetColumn(sp, 1);

            Button up = new() { Content = "Up" };
            Button down = new() { Content = "Down" };
            Button remove = new() { Content = "Remove" };

            up.Click += MoveUpClick;
            down.Click += MoveDownClick;
            remove.Click += RemoveClick;

            sp.Children.AddRange([up, down, remove]);

            control.HeaderPanel.Children.Add(sp);
        }

        public void Save()
        {
            if (MainControl is not ITopLevelSavable control)
                return;

            control.Save();
        }

        private void MoveUpClick(object? sender, RoutedEventArgs e) => MoveUp?.Invoke(this, e);
        private void MoveDownClick(object? sender, RoutedEventArgs e) => MoveDown?.Invoke(this, e);
        private void RemoveClick(object? sender, RoutedEventArgs e) => Remove?.Invoke(this, e);
    }
}