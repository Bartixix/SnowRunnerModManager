using SnowRunnerModManager.Core.GameClasses.Subclasses;

using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Gearbox : GameCommon
    {
        [EditorField("AWD Consumption modidier", 0)]
        public double AWDConsumptionModifier { get; set; }
        [EditorField("Critical damage threshold", 1)]
        public double CriticalDamageThreshold { get; set; }
        [EditorField("Damage capacity", 2)]
        public int DamageCapacity { get; set; }
        [EditorField("Damaged consumption modifier", 3)]
        public double DamagedConsumptionModifier { get; set; }
        [EditorField("Fuel consumption", 4)]
        public double FuelConsumption { get; set; }
        [EditorField("Idle fuel modifier", 5)]
        public double IdleFuelModifier { get; set; }
        [EditorField("Min break freq", 6)]
        public double MinBreakFreq { get; set; }
        [EditorField("Max break freq", 7)]
        public double MaxBreakFreq { get; set; }

        [EditorField("Reverse gear", 8)]
        [EditorSimpleClass]
        public ReverseGear ReverseGear { get; set; }
        [EditorField("High gear", 9)]
        [EditorSimpleClass]
        public HighGear HighGear { get; set; }
        [EditorList("Gears", 10)]
        public List<Gear> Gears { get; set; }

        [EditorField("High gear", 11)]
        public bool IsHighGearExists { get; set; }
        [EditorField("Low gear", 12)]
        public bool IsLowerGearExists { get; set; }
        [EditorField("Low+ gear", 13)]
        public bool IsLowerPlusGearExists { get; set; }
        [EditorField("Low- gear", 14)]
        public bool IsLowerMinusGearExists { get; set; }
    }
}
