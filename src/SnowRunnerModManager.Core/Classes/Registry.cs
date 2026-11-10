using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.GameClasses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public class Registry<T> : IRegistry<T> where T : ICommonObject
    {
        private Dictionary<string, T> Entries { get; } = [];

        public IReadOnlyCollection<T> Items => Entries.Values;
        public IReadOnlyCollection<string> ItemNames => Entries.Keys;

        public Type ItemType { get; } = typeof(T);


        public void Add(T item) => Entries.Add(item.Name, item);
        public bool Remove(string item) => Entries.Remove(item);
        public bool Remove(T item) => Entries.Remove(item.Name);
        public T Get(string item) => Entries[item];
    }
}
