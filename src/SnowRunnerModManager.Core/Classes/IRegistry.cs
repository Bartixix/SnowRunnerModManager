using SnowRunnerModManager.Core.GameClasses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public interface IRegistry
    {
        Type ItemType { get; }
        IReadOnlyCollection<string> ItemNames { get; }
    }

    public interface IRegistry<T> : IRegistry where T : ICommonObject
    {
        IReadOnlyCollection<T> Items { get; }
        void Add(T item);
        bool Remove(string item);
        bool Remove(T item);
        T Get(string item);
    }
}
