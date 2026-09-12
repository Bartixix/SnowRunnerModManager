using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Engine : GameCommon
    {
        [EditorField("Critical damage threshold")]
        public double CriticalDamageThreshold { get; set; }
        [EditorField("Damage capacity")]
        public int DamageCapacity { get; set; }
        [EditorField("Damaged consumption modifier")]
        public double DamagedConsumptionModifier { get; set; }
        [EditorField("Engine responsiveness")]
        public double EngineResponsiveness { get; set; }
        [EditorField("Fuel consumption")]
        public double FuelConsumption { get; set; }

        [EditorField("Torque")]
        public int Torque { get; set; }
        [EditorField("Damaged min torque multiplier")]
        public double DamagedMinTorqueMultiplier { get; set; }
        [EditorField("Damaged max torque multiplier")]
        public double DamagedMaxTorqueMultiplier { get; set; }
        [EditorField("MaxDeltaAngVel")]
        public double MaxDeltaAngVel { get; set; }
    }
}
