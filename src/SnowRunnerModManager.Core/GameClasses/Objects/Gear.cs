using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    [EditorSimpleClass]
    public class Gear
    {
        [EditorField("Gear multiplier", 0)]
        [XMLElement("#$")]
        public required double AngVel { get; set; }
        [EditorField("Fuel multiplier", 1)]
        [XMLElement("#$")]
        public required double FuelModifier { get; set; }
    }

    [EditorSimpleClass]
    public class ReverseGear : Gear;
    [EditorSimpleClass]
    public class HighGear : Gear;
}
