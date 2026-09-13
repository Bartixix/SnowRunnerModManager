using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Subclasses
{
    [EditorSimpleClass]
    public class Gear(double angVel, double fuelModifier)
    {
        [EditorField("Gear multiplier", 0)]
        public double AngVel { get; set; } = angVel;
        [EditorField("Fuel multiplier", 1)]
        public double FuelModifier { get; set; } = fuelModifier;
    }

    [EditorSimpleClass]
    public class ReverseGear(double angVel, double fuelModifier) : Gear(angVel, fuelModifier);
    [EditorSimpleClass]
    public class HighGear(double angVel, double fuelModifier) : Gear(angVel, fuelModifier);
}
