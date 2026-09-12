using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public class Group<T> where T : GameCommon
    {
        public string Name { get; }

        private readonly Dictionary<string, T> _objects;
        public IReadOnlyCollection<T> Objects { get { return _objects.Values; } }

        public Group(string groupName)
        {
            Name = groupName;
            _objects = [];
        }

        public Group(string groupName, List<T> objects)
        {
            Name = groupName;
            _objects = objects.ToDictionary(o => o.Name, o => o);
        }

        public Group(string groupName, Dictionary<string, T> objectGroup)
        {
            Name = groupName;
            _objects = objectGroup;
        }

        public void Add(T obj) => _objects.Add(obj.Name, obj);
        public bool Remove(T obj) => _objects.Remove(obj.Name);
        public T Get(string name) => _objects[name];
    }
}
