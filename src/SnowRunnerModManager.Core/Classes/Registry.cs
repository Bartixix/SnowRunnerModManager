using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public class Registry<T> where T : GameCommon
    {
        private readonly Dictionary<string, T> _items = [];
        public IReadOnlyCollection<T> Items { get { return _items.Values; } }

        public void Add(T item) => _items.Add(item.Name, item);
        public bool Remove(string name) => _items.Remove(name);
        public T Get(string name) => _items[name];
    }
}
