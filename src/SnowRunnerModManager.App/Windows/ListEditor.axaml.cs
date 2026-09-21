using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.EditorUserControls;
using SnowRunnerModManager.App.ViewModels;
using SnowRunnerModManager.Utils.Metadata;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace SnowRunnerModManager.App.Windows
{
    public partial class ListEditor : Window
    {
        public ObservableCollection<ListFieldControl> Collection { get; } = [];

        public required Type ObjType { get; init; }
        public required PropertyInfo Parameter { get; init; }
        public required IList Target { get; init; }

        public ListEditor()
        {
            InitializeComponent();

            Loaded += SetupFields;
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            if (ObjType.GetConstructor(Type.EmptyTypes) == null)
                throw new Exception();

            foreach (var item in Target)
            {
                ListFieldControl control = CreateControl(item);

                Collection.Add(control);
                MainPanel.Children.Add(control);
            }
        }

        private ListFieldControl CreateControl(object obj)
        {
            ListFieldControl control = new() { FieldName = "List item", Target = obj, Parameter = Parameter };

            control.MoveUp += MoveUp;
            control.MoveDown += MoveDown;
            control.Remove += Remove;

            return control;
        }

        private void SaveFields(object? sender, RoutedEventArgs e)
        {
            Target.Clear();

            foreach (var item in Collection)
            {
                item.Save();
                Target.Add(item.Target);
            }

            Close();
        }

        private void AddField(object? sender, RoutedEventArgs e)
        {
            object obj = Activator.CreateInstance(ObjType)!;
            ListFieldControl control = CreateControl(obj);

            Collection.Add(control);
            MainPanel.Children.Add(control);
        }

        private void MoveUp(object? sender, RoutedEventArgs e)
        {
            ListFieldControl control = (ListFieldControl)sender!;

            int index = Collection.IndexOf(control);

            if (index < 1)
                return;

            Collection.Move(index, index - 1);
            MainPanel.Children.Move(index, index - 1);
        }

        private void MoveDown(object? sender, RoutedEventArgs e)
        {
            ListFieldControl control = (ListFieldControl)sender!;

            int index = Collection.IndexOf(control);

            if (index > Collection.Count - 2)
                return;

            Collection.Move(index, index + 1);
            MainPanel.Children.Move(index, index + 1);
        }

        private void Remove(object? sender, RoutedEventArgs e)
        {
            ListFieldControl control = (ListFieldControl)sender!;

            Collection.Remove(control);
            MainPanel.Children.Remove(control);
        }
    }
}