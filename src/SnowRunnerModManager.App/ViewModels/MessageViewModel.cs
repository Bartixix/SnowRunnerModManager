using Avalonia.Media;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using SnowRunnerModManager.Utils.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SnowRunnerModManager.App.ViewModels
{
    public class MessageViewModel
    {
        public static ObservableCollection<MessageViewModel> Messages { get; } = [];

        private static readonly SolidColorBrush White = new(Color.FromRgb(255, 255, 255));

        public MessageViewModel()
        {
            Message = "";
        }

        public MessageViewModel(string message, MessageEventArgs.MessageSeverity severity)
        {
            Message = message;
            Severity = severity;
        }

        public SolidColorBrush BrushColor
        {
            get
            {
                IResourceDictionary? resources = Avalonia.Application.Current?.Resources;
                if (resources == null)
                    return White;

                string resourceName = $"Text{Enum.GetName(Severity)}";

                if (!resources.TryGetValue(resourceName, out object? brushObj))
                    return White;

                if (brushObj is SolidColorBrush brush)
                    return brush;

                return White;
            }
        }

        public string Message { get; }
        public MessageEventArgs.MessageSeverity Severity { get; }
    }
}
