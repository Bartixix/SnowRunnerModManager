using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses.Templates;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class Engine : GameObject
    {
        [EditorField("Critical damage threshold", 0)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> CriticalDamageThreshold { get; set; }

        [EditorField("Damage capacity", 1)]
        [XMLElement("#$")]
        public required Templatable<int, EngineTemplate> DamageCapacity { get; set; }

        [EditorField("Damaged consumption modifier", 2)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> DamagedConsumptionModifier { get; set; }

        [EditorField("Engine responsiveness", 3)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> EngineResponsiveness { get; set; }

        [EditorField("Fuel consumption", 4)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> FuelConsumption { get; set; }

        [EditorField("Torque", 5)]
        [XMLElement("#$")]
        public required Templatable<int, EngineTemplate> Torque { get; set; }

        [EditorField("Damaged min torque multiplier", 6)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> DamagedMinTorqueMultiplier { get; set; }

        [EditorField("Damaged max torque multiplier", 7)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> DamagedMaxTorqueMultiplier { get; set; }

        [EditorField("Max delta ang vel", 8)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> MaxDeltaAngVel { get; set; }

        [EditorField("Breaks delay", 9)]
        [XMLElement("#$")]
        public required Templatable<double, EngineTemplate> BreaksDelay { get; set; }
    }
}
