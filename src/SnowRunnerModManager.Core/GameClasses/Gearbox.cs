using SnowRunnerModManager.Core.GameClasses.Subclasses;

using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Gearbox : GameCommon
    {
        [EditorField("AWD Consumption modidier")]
        public double AWDConsumptionModifier { get; set; }
        [EditorField("Critical damage threshold")]
        public double CriticalDamageThreshold { get; set; }
        [EditorField("Damage capacity")]
        public int DamageCapacity { get; set; }
        [EditorField("Damaged consumption modifier")]
        public double DamagedConsumptionModifier { get; set; }
        [EditorField("Fuel consumption")]
        public double FuelConsumption { get; set; }
        [EditorField("Idle fuel modifier")]
        public double IdleFuelModifier { get; set; }
        [EditorField("Min break freq")]
        public double MinBreakFreq { get; set; }
        [EditorField("Max break freq")]
        public double MaxBreakFreq { get; set; }

        [EditorField("Reverse gear")]
        public ReverseGear ReverseGear { get; set; }
        [EditorField("High gear")]
        public HighGear HighGear { get; set; }
        [EditorList("Gears")]
        public List<Gear> Gears { get; set; }

        [EditorField("High gear")]
        public bool IsHighGearExists { get; set; }
        [EditorField("Low gear")]
        public bool IsLowerGearExists { get; set; }
        [EditorField("Low+ gear")]
        public bool IsLowerPlusGearExists { get; set; }
        [EditorField("Low- gear")]
        public bool IsLowerMinusGearExists { get; set; }
    }
}
