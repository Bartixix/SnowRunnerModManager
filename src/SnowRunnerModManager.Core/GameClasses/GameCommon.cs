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

        [EditorField("Price", -3)]
        public int Price { get; set; }
        [EditorField("Unlocked by exploration", -2)]
        public bool UnlockedByExploration { get; set; }
        [EditorField("Unlocked by rank", -1)]
        public int UnlockByRank { get; set; }

    }
}
