using SnowRunnerModManager.Core.GameClasses.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SnowRunnerModManager.Core.Classes
{
    public interface IRegistry
    {
        Type ItemType { get; }
        IReadOnlyCollection<string> ItemNames { get; }

        IReadOnlyCollection<object> Items { get; }
        void Add(object item);
        bool Remove(string item);
        bool Remove(object item);
        bool TryGet(string item, [NotNullWhen(true)] out object? obj);
    }

    public interface IRegistry<T> : IRegistry where T : ICommonObject
    {
        new IReadOnlyCollection<T> Items { get; }
        void Add(T item);
        bool Remove(T item);
        bool TryGet(string item, [NotNullWhen(true)] out T? obj);
    }
}
