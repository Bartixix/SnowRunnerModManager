using SnowRunnerModManager.Core.Classes;
using SnowRunnerModManager.Core.GameClasses.Templates;
using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class TruckTire : GameObject
    {
        [XMLElement("#_template")]
        public TruckTireTemplate? TruckTireInclude { get; set; }

        [EditorField("Asphalt", 0)]
        [XMLElement("WheelFriction#$")]
        public required Templatable<double, WheelFrictionTemplate> BodyFrictionAsphalt { get; set; }
        [EditorField("Offroad", 1)]
        [XMLElement("WheelFriction#$")]
        public required Templatable<double, WheelFrictionTemplate> BodyFriction { get; set; }
        [EditorField("Mud", 2)]
        [XMLElement("WheelFriction#$")]
        public required Templatable<double, WheelFrictionTemplate> SubstanceFriction { get; set; }
    }
}
