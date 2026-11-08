using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class TruckTireTemplate : Template
    {
        [XMLElement("WheelFriction", DefaultValue = null)]
        public required WheelFrictionTemplate? Friction { get; set; } = null;
    }
}
