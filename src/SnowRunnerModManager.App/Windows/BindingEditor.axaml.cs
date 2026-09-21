using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using SnowRunnerModManager.App.EditorUserControls;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SnowRunnerModManager.App.Windows
{
    public partial class BindingEditor : Window
    {
        public required string[] BindableNames { get; init; }
        public required List<string> Enabled { get; init; }
        private int CurrentlyEnabled { get; set; } = 0;

        public required bool MultiChoice { get; init; }

        BindingFieldControl[] BindingControls { get; set; } = [];

        public BindingEditor()
        {
            InitializeComponent();

            Loaded += SetupFields;

        }

        private void SetupFields(object? sender, EventArgs e)
        {
            BindingControls = new BindingFieldControl[BindableNames.Length];
            CurrentlyEnabled = Enabled.Count;

            for (int i = 0; i < BindableNames.Length; i++)
            {
                BindingFieldControl control = new() { Id = i };
                control.MainText.Text = BindableNames[i];
                control.MainBox.IsChecked = Enabled.Contains(BindableNames[i]);
                control.OnInput += OnInput;

                MainPanel.Children.Add(control);

                BindingControls[i] = control;
            }
        }

        private void OnInput(object? sender, RoutedEventArgs e)
        {
            if (sender is not BindingFieldControl control)
                throw new InvalidOperationException();

            if (CurrentlyEnabled == -1)
                return;

            int excludedId = control.Id;

            if (control.MainBox.IsChecked == true && !MultiChoice)
            {
                CurrentlyEnabled = -1;

                for (int i = 0; i < BindingControls.Length; i++)
                {
                    if (i == excludedId)
                        continue;

                    if (BindingControls[i].MainBox.IsChecked == true)
                        BindingControls[i].MainBox.IsChecked = false;
                }

                CurrentlyEnabled = 1;
                return;
            }

            if (control.MainBox.IsChecked == true)
            {
                CurrentlyEnabled++;
                return;
            }

            if (CurrentlyEnabled == 1)
            {
                CurrentlyEnabled--;
                control.MainBox.IsChecked = true;
                return;
            }

            if (control.MainBox.IsChecked == false)
            {
                CurrentlyEnabled--;
                return;
            }
        }

        private void Save(object? sender, RoutedEventArgs e)
        {
            for (int i = 0; i < BindingControls.Length; i++)
            {
                if (BindingControls[i].MainBox.IsChecked == true)
                {
                    if (!Enabled.Contains(BindingControls[i].MainText.Text!))
                        Enabled.Add(BindingControls[i].MainText.Text!);
                }
                else
                {
                    if (Enabled.Contains(BindingControls[i].MainText.Text!))
                        Enabled.Remove(BindingControls[i].MainText.Text!);
                }
            }

            Close();
        }
    }
}