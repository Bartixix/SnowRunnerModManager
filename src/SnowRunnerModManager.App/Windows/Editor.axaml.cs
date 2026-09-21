using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.CustomContorls;
using SnowRunnerModManager.App.EditorHelpers;

namespace SnowRunnerModManager.App.Windows
{
    public partial class Editor : Window
    {
        public const int MarginPerDepth = 30;

        public bool IsSaved { get; private set; } = false;

        public Editor()
        {
            InitializeComponent();
        }

        private void SaveFields(object sender, RoutedEventArgs e)
        {
            foreach (Control control in EditorFieldsPanel.Children)
            {
                if (control is not ITopLevelSavable editorUserControl)
                    continue;

                editorUserControl.Save();
            }

            IsSaved = true;

            Close();
        }
    }
}