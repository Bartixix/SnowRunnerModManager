using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class EngineTemplate : Template<EngineTemplate>
    {

        [XMLElement("#$")]
        public required double? CriticalDamageThreshold { get; set; } = null;


        [XMLElement("#$")]
        public required int? DamageCapacity { get; set; } = null;


        [XMLElement("#$")]
        public required double? DamagedConsumptionModifier { get; set; } = null;


        [XMLElement("#$")]
        public required double? EngineResponsiveness { get; set; } = null;


        [XMLElement("#$")]
        public required double? FuelConsumption { get; set; } = null;


        [XMLElement("#$")]
        public required int? Torque { get; set; } = null;


        [XMLElement("#$")]
        public required double? DamagedMinTorqueMultiplier { get; set; } = null;


        [XMLElement("#$")]
        public required double? DamagedMaxTorqueMultiplier { get; set; } = null;


        [XMLElement("#$")]
        public required double? MaxDeltaAngVel { get; set; } = null;


        [XMLElement("#$")]
        public required double? BreaksDelay { get; set; } = null;
    }
}
