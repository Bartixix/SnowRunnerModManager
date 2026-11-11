

using Avalonia.Controls.Chrome;
using Avalonia.Interactivity;
using SnowRunnerModManager.App.EditorHelpers;
using SnowRunnerModManager.App.Windows;
using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses.Objects;
using SnowRunnerModManager.Core.GameParsers;
using SnowRunnerModManager.Core.Helpers;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Infrastructure.Helpers;
using SnowRunnerModManager.RuntimeTests.Renderer;
using SnowRunnerModManager.Utils.Events;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SnowRunnerModManager.Infrastructure.Xml;
using SnowRunnerModManager.Core.GameClasses.Templates;
using System.Linq;
using SnowRunnerModManager.Core.GameClasses;
using Microsoft.VisualBasic;

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

        private async void NewProjectClick(object? sender, RoutedEventArgs e)
        {
            Window window = (Window?)TopLevel.GetTopLevel(this) ?? throw new Exception();
            NewProject np = new();
            await np.ShowDialog(window);

            if (np.Saved == false)
                return;

            await Project.Create(np.ProjectName, np.ProjectFolder, np.EXEPath);
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

        RendererTestClass rtc = new("defstr", -1, -0.1, RendererTestClass.TestEnum.A, false, true, new("defstrnest", -2), ["t1", "t3"], ["t2"], [new("test 1", 1), new("test 2", 2)]);
        private async void TestRendererfc(object? sender, RoutedEventArgs e)
        {
            Project p = Project.CreateDummy();

            EditorFieldRenderer<RendererTestClass> efr = new(rtc);
            rtc = await efr.Render();
        }

        //private async void TestLoad(object? sender, RoutedEventArgs e)
        //{
        //    string TP = "E:\\Coding\\snowrnn-mod\\test-load\\trucks.xml";
        //    string EP = "E:\\Coding\\snowrnn-mod\\test-load\\engine.xml";
        //    string TrP = "E:\\Coding\\snowrnn-mod\\test-load\\truck_n.xml";
        //    string WP = "E:\\Coding\\snowrnn-mod\\test-load\\wheels.xml";
        //    string GP = "E:\\Coding\\snowrnn-mod\\test-load\\gearbox.xml";

        //    Project p = Project.CreateDummy();

        //    Parser engineParser = Parser.GetParser<Engine>();
        //    Parser truckParser = Parser.GetParser<Truck>();
        //    Parser wheelParser = Parser.GetParser<Wheel>();
        //    Parser gearboxParser = Parser.GetParser<Gearbox>();

        //    XElement templateDoc = await ParserHelper.LoadXML(TP);
        //    XElement[] templateElements = [.. templateDoc.Element("_templates")!.Element("WheelFriction")!.Descendants()];

        //    XElement engineDoc = await ParserHelper.LoadXML(EP);
        //    XElement[] engines = [.. engineDoc.Element("EngineVariants")!.Elements("Engine")];

        //    XElement truckDoc = await ParserHelper.LoadXML(TrP);

        //    XElement wheelDoc = await ParserHelper.LoadXML(WP);
        //    XElement[] wheels = [.. wheelDoc.Element("TruckWheels")!.Element("TruckTires")!.Elements("TruckTire")];

        //    XElement gearboxDoc = await ParserHelper.LoadXML(GP);
        //    XElement[] gearboxes = [.. gearboxDoc.Element("GearboxVariants")!.Elements("Gearbox")];

        //    Registry<Wheel> templates = ActiveProject.WheelTemplates;
        //    Group<Engine> engineGroup;
        //    Truck t;
        //    Group<Wheel> wheelGroup;
        //    Group<Gearbox> gearboxGroup;

        //    foreach (XElement TE in templateElements)
        //        ActiveProject.WheelTemplates.Add((Wheel)wheelParser.Parse(TE, TP)!);

        //    List<Engine> EL = [];
        //    foreach (XElement E in engines)
        //        EL.Add((Engine)engineParser.Parse(E, EP)!);

        //    engineGroup = new("testE", "", EL);

        //    t = (Truck)truckParser.Parse(truckDoc, TrP)!;

        //    List<Wheel> WL = [];
        //    foreach (XElement W in wheels)
        //        WL.Add((Wheel)wheelParser.Parse(W, WP)!);

        //    wheelGroup = new("testW", "", WL);

        //    List<Gearbox> GL = [];
        //    foreach (XElement G in gearboxes)
        //        GL.Add((Gearbox)gearboxParser.Parse(G, GP)!);

        //    gearboxGroup = new("testG", "", GL);
        //}

        private async void CustomLoadTest(object? sender, RoutedEventArgs e)
        {
            string TrP = "E:\\Coding\\snowrnn-mod\\test-load\\truck_n.xml";

            Project p = Project.CreateDummy();

            XDocument doc = XDocument.Load(TrP);

            Parser wtp = Parser.GetParser<WheelTemplate>();

            List<XTemplate> templates = doc.TemplateRoot!.Element("Wheel")!.Children;

            List<WheelTemplate> wt = [.. templates.Select(t => (WheelTemplate)wtp.Parse(t, null, TrP)!)];

            GameContainer<Truck> gc = new(TrP);
            gc.Templates.Add(typeof(WheelTemplate), wt.ToDictionary(t => t.Name, t => (Template)t));

            XElementBase truckE = doc.ElementRoot!;

            Parser tp = Parser.GetParser<Truck>();
            Truck truck = (Truck)tp.Parse(truckE, gc, TrP)!;
        }
    }
}