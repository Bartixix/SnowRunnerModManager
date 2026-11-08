using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class Gearbox : GameObject
    {
        [EditorField("AWD Consumption modidier", 0)]
        [XMLElement("#$")]
        public required double AWDConsumptionModifier { get; set; }
        [EditorField("Critical damage threshold", 1)]
        [XMLElement("#$")]
        public required double CriticalDamageThreshold { get; set; }
        [EditorField("Damage capacity", 2)]
        [XMLElement("#$")]
        public required int DamageCapacity { get; set; }
        [EditorField("Damaged consumption modifier", 3)]
        [XMLElement("#$")]
        public required double DamagedConsumptionModifier { get; set; }
        [EditorField("Fuel consumption", 4)]
        [XMLElement("#$")]
        public required double FuelConsumption { get; set; }
        [EditorField("Idle fuel modifier", 5)]
        [XMLElement("#$")]
        public required double IdleFuelModifier { get; set; }
        [EditorField("Min break freq", 6)]
        [XMLElement("#$")]
        public required double MinBreakFreq { get; set; }
        [EditorField("Max break freq", 7)]
        [XMLElement("#$")]
        public required double MaxBreakFreq { get; set; }

        [EditorField("Reverse gear", 8)]
        [EditorSimpleClass]
        [XMLElement("$")]
        public required ReverseGear ReverseGear { get; set; }
        [EditorField("High gear", 9)]
        [EditorSimpleClass]
        [XMLElement("$")]
        public required HighGear HighGear { get; set; }
        [EditorList("Gears", 10)]
        [XMLElement("Gear")]
        public required List<Gear> Gears { get; set; }

        [EditorField("High gear", 11)]
        [XMLElement("GameData>GearboxParams#$")]
        public required bool IsHighGearExists { get; set; }
        [EditorField("Low gear", 12)]
        [XMLElement("GameData>GearboxParams#$")]
        public required bool IsLowerGearExists { get; set; }
        [EditorField("Low+ gear", 13)]
        [XMLElement("GameData>GearboxParams#$")]
        public required bool IsLowerPlusGearExists { get; set; }
        [EditorField("Low- gear", 14)]
        [XMLElement("GameData>GearboxParams#$")]
        public required bool IsLowerMinusGearExists { get; set; }
    }
}
