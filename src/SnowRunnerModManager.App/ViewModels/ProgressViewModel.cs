using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace SnowRunnerModManager.App.ViewModels
{
    public partial class ProgressViewModel : ObservableObject
    {
        public ProgressViewModel()
        {
            Id = "default";
            Message = "";
        }

        public ProgressViewModel(string id, string message, double value, bool isIndeteterminate)
        {
            Id = id;
            Message = message;
            Value = value;
            IsIndeteterminate = isIndeteterminate;
        }

        public static ObservableCollection<ProgressViewModel> ProgressOperations { get; } = [];

        public string Id { get; }

        [ObservableProperty]
        public partial string Message { get; set; }

        [ObservableProperty]
        public partial double Value { get; set; }

        [ObservableProperty]
        public partial bool IsIndeteterminate { get; set; }
    }
}
