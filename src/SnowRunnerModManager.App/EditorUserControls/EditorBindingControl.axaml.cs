using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.Core.Classes;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorBindingControl : EditorUserControl
    {
        public required IRegistry BoundRegistry { get; init; }

        public EditorBindingControl()
        {
            InitializeComponent();
        }
    }
}