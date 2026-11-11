using SnowRunnerModManager.Core.Projects;
using SnowRunnerModManager.Utils.Metadata;
using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses.Templates;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class Truck : GameObject
    {
        public enum TorqueMode
        {
            None,
            Connectable,
            Full,
            Default
        }

        public enum DiffLock
        {
            None,
            Uninstalled,
            Installed,
            Always,
            Connected,
            Switchable
        }

        [EditorField("First axle AWD", 0)]
        [XMLTemplate(TemplateType.Wheel, "FrontWheel#Torque", "FirstAxle#Torque")]
        public required Templatable<TorqueMode, WheelTemplate> FirstAWD { get; set; }

        [EditorField("Second axle AWD", 1)]
        [XMLTemplate(TemplateType.Wheel, "SecondAxle#Torque")]
        public required Templatable<TorqueMode, WheelTemplate> SecondAWD { get; set; }

        [EditorField("Third axle AWD", 2)]
        [XMLTemplate(TemplateType.Wheel, "MiddleAxle#Torque")]
        public required Templatable<TorqueMode, WheelTemplate> ThirdAWD { get; set; }

        [EditorField("Rear axle AWD", 3)]
        [XMLTemplate(TemplateType.Wheel, "RearWheel#Torque", "RearAxle#Torque")]
        public required Templatable<TorqueMode, WheelTemplate> RearAWD { get; set; }

        [PropertyIgnoreIf(false, nameof(FirstAWD))]
        [XMLTemplate(TemplateType.Wheel, "\0%HasAxle@1")]
        public required bool HasFirstAxle { get; init; }

        [PropertyIgnoreIf(false, nameof(SecondAWD))]
        [XMLTemplate(TemplateType.Wheel, "\0%HasAxle@2")]
        public required bool HasSecondAxle { get; init; }

        [PropertyIgnoreIf(false, nameof(ThirdAWD))]
        [XMLTemplate(TemplateType.Wheel, "\0%HasAxle@3")]
        public required bool HasThirdAxle { get; init; }

        [PropertyIgnoreIf(false, nameof(RearAWD))]
        [XMLTemplate(TemplateType.Wheel, "\0%HasAxle@4")]
        public required bool HasRearAxle { get; init; }

        [EditorField("Diff lock type", 4)]
        [XMLElement("TruckData#$")]
        public required DiffLock DiffLockType { get; set; }

        [EditorList("Compatible wheels", 5)]
        [XMLElement("TruckData>$", Special = SpecialResolver.MultipleValues)]
        public required List<CompatibleWheels> CompatibleWheels { get; set; }

        [EditorBinding("Gearboxes", 6, nameof(ActiveProject.Gearboxes), true)]
        [XMLElement("TruckData>$#Type", ListSeparator = ",")]
        public required List<string> GearboxSocket { get; set; }

        [EditorBinding("Engines", 7, nameof(ActiveProject.Engines), true)]
        [XMLElement("TruckData>$#Type", ListSeparator = ",")]
        public required List<string> EngineSocket { get; set; }
    }
}
