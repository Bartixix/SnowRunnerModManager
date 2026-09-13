using Avalonia.Controls;
using System.Diagnostics.CodeAnalysis;


namespace SnowRunnerModManager.App;

public partial class MainWindow : Window
{

    [MemberNotNullWhen(true, nameof(_window))]
    private static bool WInitialized { get; set; } = false;

    private static MainWindow? _window;
    public static MainWindow MWindow { get { if (!WInitialized) throw new InvalidOperationException(); return _window; } }

    public MainWindow()
    {
        InitializeComponent();

        _window = this;
        WInitialized = true;
    }
}