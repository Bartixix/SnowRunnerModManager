using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class GameContainer
    {
        [XMLSpecial(SpecialOp.FileName)]
        public string FileName { get; }

        [XMLRoot(RootElement.Template)]
        public Dictionary<TemplateType, Dictionary<string, Template>> Templates { get; } = [];
    }

    public class GameContainer<T> : GameContainer where T : GameObject
    {
        [XMLRoot(RootElement.Object)]
        public Dictionary<string, T> GameItems { get; } = [];
    }
}
