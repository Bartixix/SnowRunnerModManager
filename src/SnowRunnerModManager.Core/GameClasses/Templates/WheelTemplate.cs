using static SnowRunnerModManager.Core.GameClasses.Objects.Truck;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses.Templates
{
    public class WheelTemplate : Template<WheelTemplate>
    {
        [XMLElement("#$")]
        public required TorqueMode Torque { get; set; }
    }
}
