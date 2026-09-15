using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class BindingFieldControl : UserControl
    {
        public required int Id { get; init; }

        public event EventHandler<RoutedEventArgs>? OnInput;

        public BindingFieldControl()
        {
            InitializeComponent();
        }

        private void CheckBoxClicked(object? sender, RoutedEventArgs e) => OnInput?.Invoke(this, e);
    }
}