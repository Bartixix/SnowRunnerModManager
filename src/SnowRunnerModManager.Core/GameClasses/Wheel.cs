using SnowRunnerModManager.Utils.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.GameClasses
{
    public class Wheel : GameCommon
    {
        [EditorField("Asphalt", 0)]
        public double BodyFrictionAsphalt { get; set; }
        [EditorField("Offroad", 1)]
        public double BodyFriction { get; set; }
        [EditorField("Mud", 2)]
        public double SubstanceFriction { get; set; }
        [EditorField("Chains", 3)]
        public bool IsIgnoreIce { get; set; }


        [PropertyIgnoreIf(true, nameof(Price), nameof(UnlockedByExploration), nameof(UnlockByRank))]
        public bool IsWheelTemplate { get; }
    }
}
