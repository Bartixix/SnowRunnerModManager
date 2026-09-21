using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Utils.Metadata;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorListControl : EditorUserControl
    {
        public bool AllowAdd { get; init; } = true;
        public bool AllowRemove { get; init; } = true;
        public bool AllowReorder { get; init; } = true;

        private Type? _listType;
        private Type ListType { get { return _listType ?? throw new InvalidOperationException(); } set { _listType = value; } }

        private Type? _classType;
        private Type ClassType { get { return _classType ?? throw new InvalidOperationException(); } set { _classType = value; } }

        private IList? _listObj;
        private IList ListObj { get { return _listObj ?? throw new InvalidOperationException(); } set { _listObj = value; } }

        private int ElementCount => (int)ListType.GetProperty(nameof(List<>.Count))!.GetValue(ListObj)!;



        public EditorListControl()
        {
            InitializeComponent();

            Initialized += SetupFields;
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            object obj = Parameter.GetValue(Target)!;

            Type type = obj.GetType();
            if (!type.IsAssignableTo(typeof(IList)))
                throw new InvalidOperationException();

            Type classType = type.GetGenericArguments()[0];

            if (classType.GetCustomAttribute<EditorSimpleClass>() == null)
                throw new InvalidOperationException();

            ListType = type;
            ClassType = classType;
            ListObj = (IList)obj;

            SetupGrid();

            MainText.Text = $"{FieldName}:";
            MainBtn.Content = $"Edit ({ElementCount} elements)";
        }

        public async void OpenListEditor(object? sender, RoutedEventArgs e)
        {
            Window window = (Window?)TopLevel.GetTopLevel(this) ?? throw new Exception();

            Type genericType = ListObj.GetType().GetGenericArguments()[0];

            ListEditor editor = new() { Target = ListObj, Parameter = Parameter, ObjType = genericType };
            await editor.ShowDialog(window);

            MainBtn.Content = $"Edit ({ElementCount} elements)";
        }

        private void SetupGrid()
        {
            int marginLeft = Depth * Editor.MarginPerDepth;

            MainGrid.ColumnDefinitions.Add(new() { Width = new(marginLeft), MaxWidth = marginLeft, MinWidth = marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = new(300 - marginLeft), MaxWidth = 300 - marginLeft, MinWidth = 300 - marginLeft });
            MainGrid.ColumnDefinitions.Add(new() { Width = GridLength.Star, MinWidth = 300 });
        }
    }
}