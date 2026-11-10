using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class TruckTireTemplate : Template<TruckTireTemplate>
    {
        [XMLElement("WheelFriction", DefaultValue = null)]
        public required WheelFrictionTemplate? Friction { get; set; } = null;
    }
}
