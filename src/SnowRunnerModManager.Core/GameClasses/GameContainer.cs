using SnowRunnerModManager.Core.GameClasses.Common;
using SnowRunnerModManager.Utils.Metadata;

namespace SnowRunnerModManager.Core.GameClasses
{
    public abstract class GameContainer(string name) : ICommonObject
    {
        [XMLSpecial(SpecialOp.FileName)]
        public string Name { get; } = name;

        [XMLRoot(RootElement.Template)]
        public Dictionary<Type, Dictionary<string, Template>> Templates { get; } = [];
    }

    public class GameContainer<T>(string name) : GameContainer(name) where T : GameObject
    {
        [XMLRoot(RootElement.Object)]
        public Dictionary<string, T> GameItems { get; } = [];
    }
}
