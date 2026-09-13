

using Avalonia.Controls.Chrome;
using Avalonia.Interactivity;
using SnowRunnerModManager.App.EditorHelpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Infrastructure.Helpers;
using SnowRunnerModManager.RuntimeTests.Renderer;
using SnowRunnerModManager.Utils.Events;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SnowRunnerModManager.App.UserContorls
{
    public partial class TitleBar : UserControl
    {
        public TitleBar()
        {
            InitializeComponent();
            Loaded += OnLoad;
        }

        private void OnLoad(object? sender, RoutedEventArgs e)
        {
            SetupWindowTitleBar();

        }

        private void SetupWindowTitleBar()
        {
            TopLevel? topLevel = TopLevel.GetTopLevel(this);
            if (topLevel is not Window window)
                throw new Exception("Title bar not attached to a window");

            window.WindowDecorations = WindowDecorations.Full;
            window.ExtendClientAreaToDecorationsHint = true;
            window.ExtendClientAreaTitleBarHeightHint = 32;


        }

        private void OnMinimize(object? sender, RoutedEventArgs e)
        {
            TopLevel? topLevel = TopLevel.GetTopLevel(this);

            if (topLevel is not Window window)
                return;

            window.WindowState = WindowState.Minimized;
        }




        // Tests

        private async void TestMessage(object? sender, RoutedEventArgs e)
        {
            AppEvents.RaiseMessage(sender, new("Test info", MessageEventArgs.MessageSeverity.Info));
            await Task.Delay(250);
            AppEvents.RaiseMessage(sender, new("Test success", MessageEventArgs.MessageSeverity.Success));
            await Task.Delay(250);
            AppEvents.RaiseMessage(sender, new("Test warning", MessageEventArgs.MessageSeverity.Warning));
            await Task.Delay(250);
            AppEvents.RaiseMessage(sender, new("Test error", MessageEventArgs.MessageSeverity.Error));
            await Task.Delay(250);
            AppEvents.RaiseMessage(sender, new("Test critical", MessageEventArgs.MessageSeverity.Critical));
        }
        private void TestProgress0(object? sender, RoutedEventArgs e) => AppEvents.RaiseProgress(sender, new("test-progress", "test progress", 0, ProgressEventArgs.ProgressState.Started));
        private void TestProgress30(object? sender, RoutedEventArgs e) => AppEvents.RaiseProgress(sender, new("test-progress", "test progress", 30, ProgressEventArgs.ProgressState.Running));
        private void TestProgress100(object? sender, RoutedEventArgs e) => AppEvents.RaiseProgress(sender, new("test-progress", "test progress", 100, ProgressEventArgs.ProgressState.Completed));
        private void TestProgressF(object? sender, RoutedEventArgs e) => AppEvents.RaiseProgress(sender, new("test-progress", "test progress super long text lul+", 60, ProgressEventArgs.ProgressState.Failed));

        private void TestSteamPath(object? sender, RoutedEventArgs e)
        {
            if (!SteamHelper.TryGetSnowRunnerPath(out string? path))
            {
                AppEvents.RaiseMessage(this, new("Failed to snowrunner path lol", MessageEventArgs.MessageSeverity.Warning, true));
                return;
            }

            AppEvents.RaiseMessage(this, new(path, MessageEventArgs.MessageSeverity.Info));
        }

        private async void TestRendererfc(object? sender, RoutedEventArgs e)
        {
            Project p = await Project.Create("", "", "");
            RendererTestClass rtc = new("defstr", -1, -0.1, RendererTestClass.TestEnum.A, false, true, new("defstrnest", -2));

            EditorFieldRenderer<RendererTestClass> efr = new(rtc);
        }
    }
}