using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Wheel : GameCommon
    {
        [EditorField("Asphalt")]
        public double BodyFrictionAsphalt { get; set; }
        [EditorField("Offroad")]
        public double BodyFriction { get; set; }
        [EditorField("Mud")]
        public double SubstanceFriction { get; set; }
        [EditorField("Chains")]
        public bool IsIgnoreIce { get; set; }


        [PropertyIgnoreIf(true, nameof(Price), nameof(UnlockedByExploration), nameof(UnlockByRank))]
        public bool IsWheelTemplate { get; }
    }
}
