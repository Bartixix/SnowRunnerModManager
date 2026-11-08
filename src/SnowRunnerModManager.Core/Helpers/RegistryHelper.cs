using SnowRunnerModManager.Core.Classes;

using System.Reflection;
using SnowRunnerModManager.Infrastructure.IO;

using SnowRunnerModManager.Utils.Metadata;
using System.Diagnostics.CodeAnalysis;
using SnowRunnerModManager.Utils.Helpers;
using SnowRunnerModManager.Infrastructure.Xml;
using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Core.GameParsers;
using SnowRunnerModManager.Core.GameClasses.Objects;
using SnowRunnerModManager.Core.GameClasses;


namespace SnowRunnerModManager.Core.Helpers
{
    public class RegistryHelper
    {
        public static void GetAllRegistries<T>(out Dictionary<string, IRegistry> registries, out Dictionary<string, IRegistry> groupRegistries) where T : class
        {
            registries = [];
            groupRegistries = [];

            Type type = typeof(T);
            List<PropertyInfo> properties = [.. type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(p => p.GetCustomAttribute<EditorSource>() != null && p.CanRead)];

            foreach (PropertyInfo p in properties)
            {
                Type propertyType = p.PropertyType;

                if (!propertyType.IsGenericType)
                    continue;

                Type genericDefinition = propertyType.GetGenericTypeDefinition();

                if (genericDefinition == typeof(Registry<>))
                {
                    registries.Add(p.Name, (IRegistry)p.GetValue(null)!);
                    continue;
                }

                if (genericDefinition == typeof(GroupRegistry<>))
                {
                    groupRegistries.Add(p.Name, (IRegistry)p.GetValue(null)!);
                    continue;
                }
            }
        }

        public static bool GetBoundRegistry(Dictionary<string, IRegistry> registries, Dictionary<string, IRegistry> groupRegistries, EditorBinding binding, [NotNullWhen(true)] out IRegistry? registry)
        {
            if (registries.TryGetValue(binding.SourceName, out registry))
                return true;

            if (groupRegistries.TryGetValue(binding.SourceName, out registry))
                return true;

            registry = null;
            return false;
        }

        public static async Task LoadAllRegistries()
        {
            //const int LoadTasks = 4;

            //ProgressHelper generalProgress = new("Loading project", 0, LoadTasks);

            //Task wheels = LoadWheels(generalProgress);
            //Task engines = LoadEngines(generalProgress);
            //Task gearboxes = LoadGearboxes(generalProgress);
            //Task trucks = LoadTrucks(generalProgress);

            //await Task.WhenAll(wheels, engines, gearboxes, trucks);
            //generalProgress.Finish();
        }

        //public static async Task LoadWheels(ProgressHelper general)
        //{

        //    string templatePath = Path.Combine(Project.CurrentProject.RootFolder, Constants.InitialUnpacked, Constants.Templates);
        //    string[] tireFiles = ProjectIO.GetFilesInKeyFolders(Project.CurrentProject.InitialPath, Constants.Wheels);

        //    ProgressHelper wheelHelper = new("Loading tire templates", 1, 1 + tireFiles.Length);

        //    XElement templates = await ParserHelper.LoadXML(templatePath);
        //    XElement[] templateElements = [.. templates.Element("_templates")!.Element("WheelFriction")!.Descendants()];

        //    Parser parser = Parser.GetParser<Wheel>();

        //    foreach (XElement templateElement in templateElements)
        //    {
        //        Wheel wheel = parser.Parse<Wheel>(templateElement, templatePath);
        //        ActiveProject.WheelTemplates.Add(wheel);
        //    }

        //    wheelHelper.Update("Loading tires");

        //    List<Wheel> wheelList = [];
        //    for (int i = 0; i < tireFiles.Length; i++)
        //    {
        //        wheelHelper.Update(i + 2);

        //        XElement wheelRoot = await ParserHelper.LoadXML(tireFiles[i]);
        //        XElement[] wheels = wheelRoot.Element("TruckWheels")?.Element("TruckTires")?.Elements("TruckTire")?.ToArray() ?? wheelRoot.Elements("TruckWheel").ToArray() ?? throw new Exception();

        //        ParseAndAdd(wheels, wheelList, tireFiles[i], ActiveProject.Wheels, parser);
        //    }

        //    wheelHelper.Finish();
        //    general.Increment();
        //}

        //public static async Task LoadEngines(ProgressHelper general)
        //{
        //    string[] engineFiles = ProjectIO.GetFilesInKeyFolders(Project.CurrentProject.InitialPath, Constants.Engines);

        //    ProgressHelper engineHelper = new("Loading engines", 0, engineFiles.Length);

        //    Parser parser = Parser.GetParser<Engine>();

        //    List<Engine> engineList = [];
        //    for (int i = 0; i < engineFiles.Length; i++)
        //    {
        //        engineHelper.Update(i + 1);

        //        XElement engineRoot = await ParserHelper.LoadXML(engineFiles[i]);
        //        XElement[] engines = [.. engineRoot.Element("EngineVariants")!.Elements("Engine")];

        //        ParseAndAdd(engines, engineList, engineFiles[i], ActiveProject.Engines, parser);
        //    }

        //    engineHelper.Finish();
        //    general.Increment();
        //}

        //public static async Task LoadGearboxes(ProgressHelper general)
        //{
        //    string[] gearboxFiles = ProjectIO.GetFilesInKeyFolders(Project.CurrentProject.InitialPath, Constants.Gearboxes);

        //    ProgressHelper gearboxHelper = new("Loading gearboxes", 0, gearboxFiles.Length);

        //    Parser parser = Parser.GetParser<Gearbox>();

        //    List<Gearbox> gearboxList = [];
        //    for (int i = 0; i < gearboxFiles.Length; i++)
        //    {
        //        gearboxHelper.Update(i + 1);

        //        XElement gearboxRoot = await ParserHelper.LoadXML(gearboxFiles[i]);
        //        if (gearboxRoot.Element("_parent") != null)
        //            continue;

        //        XElement[] gearboxes = [.. gearboxRoot.Element("GearboxVariants")!.Elements("Gearbox")];

        //        ParseAndAdd(gearboxes, gearboxList, gearboxFiles[i], ActiveProject.Gearboxes, parser);
        //    }

        //    gearboxHelper.Finish();
        //    general.Increment();
        //}

        //public static async Task LoadTrucks(ProgressHelper general)
        //{
        //    string[] truckFiles = ProjectIO.GetFilesInKeyFolders(Project.CurrentProject.InitialPath, Constants.Trucks);

        //    ProgressHelper truckHelper = new("Loading trucks", 0, truckFiles.Length);

        //    Parser parser = Parser.GetParser<Truck>();
        //    for (int i = 0; i < truckFiles.Length; i++)
        //    {
        //        truckHelper.Update(i + 1);

        //        XElement truckRoot = await ParserHelper.LoadXML(truckFiles[i]);

        //        Truck truck = parser.Parse<Truck>(truckRoot, truckFiles[i]);
        //        ActiveProject.Trucks.Add(truck);
        //    }

        //    truckHelper.Finish();
        //    general.Increment();
        //}

        //private static void ParseAndAdd<T>(XElement[] elements, List<T> list, string file, GroupRegistry<T> registry, Parser parser) where T : GameObject
        //{
        //    foreach (XElement element in elements)
        //    {
        //        T obj = parser.Parse<T>(element, file);
        //        list.Add(obj);
        //    }

        //    string fileName = Path.GetFileNameWithoutExtension(file);
        //    Group<T> group = new(fileName, file, list);
        //    registry.Add(group);

        //    list.Clear();
        //}
    }
}
