using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;

namespace SnowRunnerModManager.App.EditorUserControls
{
    public partial class EditorListControl : EditorUserControl
    {
        public bool AllowAdd { get; init; } = true;
        public bool AllowRemove { get; init; } = true;
        public bool AllowReorder { get; init; } = true;

        public EditorListControl()
        {
            InitializeComponent();
        }
    }
}