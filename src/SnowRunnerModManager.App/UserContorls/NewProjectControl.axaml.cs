using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowRunnerModManager.App.UserContorls
{
    public partial class NewProjectControl : UserControl
    {
        public enum PickerType
        {
            None,
            File,
            Folder
        }


        public required string FieldName { get; init; }
        public required PickerType PickType { get; init; }

        public string FileType { get; init; } = string.Empty;


        public NewProjectControl()
        {
            InitializeComponent();

            Initialized += SetupFields;
        }

        private void SetupFields(object? sender, EventArgs e)
        {
            MainText.Text = FieldName;

            if (PickType == PickerType.None)
                MainBtn.IsVisible = false;
        }

        private async void Pick(object? sender, RoutedEventArgs e)
        {
            Window window = (Window?)TopLevel.GetTopLevel(this) ?? throw new Exception();

            IStorageProvider storage = window.StorageProvider;


            string path;

            if (PickType == PickerType.File)
            {
                FilePickerFileType fpt = new(FileType) { Patterns = [FileType] };

                var files = await storage.OpenFilePickerAsync(new()
                {
                    AllowMultiple = false,
                    FileTypeFilter = [fpt],
                    SuggestedFileType = fpt,
                });

                if (files.Count == 0)
                    return;

                path = files[0].Path.AbsolutePath;
            }
            else
            {
                var folders = await storage.OpenFolderPickerAsync(new() { AllowMultiple = false });

                if (folders.Count == 0)
                    return;

                path = folders[0].Path.AbsolutePath;
            }

            MainBox.Text = path;
        }

        //private IReadOnlyList<FilePickerFileType> GetFilePickerTypes(IReadOnlyList<string> fileTypes)
        //{
        //    List<FilePickerFileType> types = [];

        //    foreach (string type in fileTypes)
        //    {
        //        string[] stringTypes = [.. type.Split(',').Select(s => s.Trim())];
        //        types.Add(new(type) { Patterns = stringTypes });
        //    }

        //    return types;
        //}
    }
}