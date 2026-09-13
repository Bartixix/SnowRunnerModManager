using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Engine : GameCommon
    {
        [EditorField("Critical damage threshold", 0)]
        public double CriticalDamageThreshold { get; set; }
        [EditorField("Damage capacity", 1)]
        public int DamageCapacity { get; set; }
        [EditorField("Damaged consumption modifier", 2)]
        public double DamagedConsumptionModifier { get; set; }
        [EditorField("Engine responsiveness", 3)]
        public double EngineResponsiveness { get; set; }
        [EditorField("Fuel consumption", 4)]
        public double FuelConsumption { get; set; }

        [EditorField("Torque", 5)]
        public int Torque { get; set; }
        [EditorField("Damaged min torque multiplier", 6)]
        public double DamagedMinTorqueMultiplier { get; set; }
        [EditorField("Damaged max torque multiplier", 7)]
        public double DamagedMaxTorqueMultiplier { get; set; }
        [EditorField("MaxDeltaAngVel", 8)]
        public double MaxDeltaAngVel { get; set; }
    }
}
