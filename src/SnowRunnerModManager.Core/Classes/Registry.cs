using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public class Registry<T> : IRegistry where T : GameObject
    {
        public Type ItemType { get; } = typeof(T);

        private readonly Dictionary<string, T> _items = [];
        public IReadOnlyCollection<T> Items { get { return _items.Values; } }
        public IReadOnlyCollection<string> ItemNames { get { return _items.Keys; } }

        public void Add(T item) => _items.Add(item.Name, item);
        public bool Remove(string name) => _items.Remove(name);
        public T Get(string name) => _items[name];
    }
}
