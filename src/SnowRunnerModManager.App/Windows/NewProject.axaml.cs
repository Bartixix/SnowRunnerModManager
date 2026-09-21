using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Infrastructure.IO;
using System.Diagnostics.CodeAnalysis;

namespace SnowRunnerModManager.App.Windows
{
    public partial class NewProject : Window
    {

        [MemberNotNullWhen(true, nameof(ProjectName), nameof(EXEPath), nameof(ProjectFolder))]
        public bool Saved { get; private set; } = false;

        public string? ProjectName { get; private set; }
        public string? EXEPath { get; private set; }
        public string? ProjectFolder { get; private set; }

        public NewProject()
        {
            InitializeComponent();

            Activated += SetupFields;
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            if (ProjectIO.TryGetSnowRunnerPath(out string? path))
                EXEField.MainBox.Text = path;
        }

        private void OnCreate(object? sender, RoutedEventArgs e)
        {
            ProjectName = NameField.MainBox.Text;
            EXEPath = EXEField.MainBox.Text;
            ProjectFolder = FolderField.MainBox.Text;

            Saved = true;
            Close();

            GameCommon c = new Wheel();
            c.s
        }
    }
}