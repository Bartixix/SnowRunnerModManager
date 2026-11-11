using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class TruckTireTemplate : Template<TruckTireTemplate>
    {
        [XMLElement("WheelFriction")]
        public required WheelFrictionTemplate? Friction { get; set; } = null;
    }
}
