using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Subclasses
{
    public class Gear(double angVel, double fuelModifier)
    {
        [EditorField("Gear multiplier")]
        public double AngVel { get; set; } = angVel;
        [EditorField("Fuel multiplier")]
        public double FuelModifier { get; set; } = fuelModifier;
    }

    public class ReverseGear(double angVel, double fuelModifier) : Gear(angVel, fuelModifier);
    public class HighGear(double angVel, double fuelModifier) : Gear(angVel, fuelModifier);
}
