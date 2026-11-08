using SnowRunnerModManager.Core.GameClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public class Group<T> where T : GameObject
    {
        public string Name { get; }
        public string Origin { get; }

        private readonly Dictionary<string, T> _objects;
        public IReadOnlyCollection<T> Objects { get { return _objects.Values; } }

        public Group(string groupName, string origin)
        {
            Name = groupName;
            _objects = [];
            Origin = origin;
        }

        public Group(string groupName, string origin, List<T> objects)
        {
            Name = groupName;
            _objects = objects.ToDictionary(o => o.Name, o => o);
            Origin = origin;
        }

        public Group(string groupName, string origin, Dictionary<string, T> objectGroup)
        {
            Name = groupName;
            _objects = objectGroup;
            Origin = origin;
        }

        public void Add(T obj) => _objects.Add(obj.Name, obj);
        public bool Remove(T obj) => _objects.Remove(obj.Name);
        public T Get(string name) => _objects[name];
    }
}
