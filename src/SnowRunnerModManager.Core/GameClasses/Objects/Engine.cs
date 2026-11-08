using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class Engine : GameObject
    {
        [EditorField("Critical damage threshold", 0)]
        [XMLElement("#$")]
        public required Templatable<double> CriticalDamageThreshold { get; set; }

        [EditorField("Damage capacity", 1)]
        [XMLElement("#$")]
        public required Templatable<int> DamageCapacity { get; set; }

        [EditorField("Damaged consumption modifier", 2)]
        [XMLElement("#$")]
        public required Templatable<double> DamagedConsumptionModifier { get; set; }

        [EditorField("Engine responsiveness", 3)]
        [XMLElement("#$")]
        public required Templatable<double> EngineResponsiveness { get; set; }

        [EditorField("Fuel consumption", 4)]
        [XMLElement("#$")]
        public required Templatable<double> FuelConsumption { get; set; }

        [EditorField("Torque", 5)]
        [XMLElement("#$")]
        public required Templatable<int> Torque { get; set; }

        [EditorField("Damaged min torque multiplier", 6)]
        [XMLElement("#$")]
        public required Templatable<double> DamagedMinTorqueMultiplier { get; set; }

        [EditorField("Damaged max torque multiplier", 7)]
        [XMLElement("#$")]
        public required Templatable<double> DamagedMaxTorqueMultiplier { get; set; }

        [EditorField("Max delta ang vel", 8)]
        [XMLElement("#$")]
        public required Templatable<double> MaxDeltaAngVel { get; set; }

        [EditorField("Breaks delay", 9)]
        [XMLElement("#$")]
        public required Templatable<double> BreaksDelay { get; set; }
    }
}
