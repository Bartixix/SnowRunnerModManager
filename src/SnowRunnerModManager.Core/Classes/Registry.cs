using SnowRunnerModManager.Core.GameClasses;
using SnowRunnerModManager.Core.GameClasses.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        public bool TryGet(string item, [NotNullWhen(true)] out T? obj) => Entries.TryGetValue(item, out obj);

        // object-based implementation, do not use outside of reflection
        IReadOnlyCollection<object> IRegistry.Items => [.. Entries.Values];
        void IRegistry.Add(object item) => Entries.Add(((T)item).Name, (T)item);
        bool IRegistry.Remove(object item) => Entries.Remove(((T)item).Name);
        bool IRegistry.TryGet(string item, [NotNullWhen(true)] out object? obj)
        {
            bool res = Entries.TryGetValue(item, out T? temp);
            obj = temp;
            return res;
        }
    }
}
