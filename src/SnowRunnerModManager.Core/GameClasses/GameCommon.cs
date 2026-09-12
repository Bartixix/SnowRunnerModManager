using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class GameCommon
    {
        public enum GameState
        {
            Original,
            Modified,
            Custom
        }

        public string Name { get; set; }

        [EditorField("Price")]
        public int Price { get; set; }
        [EditorField("Unlocked by exploration")]
        public bool UnlockedByExploration { get; set; }
        [EditorField("Unlocked by rank")]
        public int UnlockByRank { get; set; }

    }
}
