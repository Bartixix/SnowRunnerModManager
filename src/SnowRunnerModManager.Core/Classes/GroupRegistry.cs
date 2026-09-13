using SnowRunnerModManager.Core.GameClasses;

namespace SnowRunnerModManager.Core.Classes
{
    public class GroupRegistry<T> : IRegistry where T : GameCommon
    {
        public Type ItemType { get; } = typeof(T);

        private readonly Dictionary<string, Group<T>> _items = [];
        public IReadOnlyCollection<Group<T>> Items { get { return _items.Values; } }
        public IReadOnlyCollection<string> ItemNames { get { return _items.Keys; } }

        public void Add(Group<T> item) => _items.Add(item.Name, item);
        public bool Remove(string name) => _items.Remove(name);
        public Group<T> Get(string name) => _items[name];
    }
}
