using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.GameClasses.Objects
{
    public class Wheel : GameObject
    {
        [EditorField("Asphalt", 0)]
        [XMLElement("WheelFriction#$")]
        public required double BodyFrictionAsphalt { get; set; }
        [EditorField("Offroad", 1)]
        [XMLElement("WheelFriction#$")]
        public required double BodyFriction { get; set; }
        [EditorField("Mud", 2)]
        [XMLElement("WheelFriction#$")]
        public required double SubstanceFriction { get; set; }
    }
}
