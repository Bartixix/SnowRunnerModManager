using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.ViewModels;
using SnowRunnerModManager.Utils.Events;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SnowRunnerModManager.App.UserContorls
{
    public partial class InfoPanel : UserControl
    {
        public InfoPanel()
        {
            InitializeComponent();

            AppEvents.OnProgress += OnProgress;
            AppEvents.OnMessage += OnMessage;
        }

        private async void OnProgress(object? sender, ProgressEventArgs e)
        {
            ProgressViewModel? progress = ProgressViewModel.ProgressOperations.FirstOrDefault(p => p.Id == e.Id);

            if (e.State == ProgressEventArgs.ProgressState.Completed)
            {
                if (progress == null)
                    return;

                ProgressViewModel.ProgressOperations.Remove(progress);
            }

            if (progress == null)
            {
                progress = new(e.Id, e.Message, e.Value, e.IsIndeterminate);

                ProgressViewModel.ProgressOperations.Add(progress);
            }

            progress.Message = e.Message;
            progress.Value = e.Value;
            progress.IsIndeteterminate = e.IsIndeterminate;

            if (e.State == ProgressEventArgs.ProgressState.Failed)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                ProgressViewModel.ProgressOperations.Remove(progress);
                return;
            }
        }

        private async void OnMessage(object? sender, MessageEventArgs e)
        {
            if (e.DebugOnly && !Debugger.IsAttached)
                return;

            MessageViewModel message = new(e.Message, e.Severity);

            MessageViewModel.Messages.Add(message);

            await Task.Delay(TimeSpan.FromSeconds(10));

            MessageViewModel.Messages.Remove(message);
        }
    }
}