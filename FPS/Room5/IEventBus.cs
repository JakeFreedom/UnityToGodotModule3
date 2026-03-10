using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace FPS.Room5
{
    public interface IEventBus<TBase> where TBase : class
    {
        void Subscribe<T>(Action<T> handler) where T : TBase;
        void Unsubscribe<T>(Action<T> handler) where T : TBase;
        void Publish<T>(T eventData) where T : TBase;
    }

    public class EventBus<TBase> : IEventBus<TBase> where TBase : class
    {
        private readonly Dictionary<Type, List<Delegate>> handlers = new Dictionary<Type, List<Delegate>>();
        private readonly object threadLock = new();

        public void Subscribe<T>(Action<T> handler) where T : TBase
        {
            lock (threadLock)
            {
                var type = typeof(T);
                if (!handlers.ContainsKey(type))
                    handlers[type] = new List<Delegate>();
                handlers[type].Add(handler);
            }
        }

        public void Unsubscribe<T>(Action<T> handler) where T : TBase
        {
            lock (threadLock)
                if (handlers.TryGetValue(typeof(T), out var list))
                    list.Remove(handler);
        }

        public void Publish<T>(T eventData) where T : TBase
        {
            List<Delegate> snapshot;
            lock (threadLock)
            {
                if (!handlers.TryGetValue(typeof(T), out var list)) return;
                snapshot = list.ToList();
            }
            foreach (var handler in snapshot)
            {
                ((Action<T>)handler)(eventData);
                GD.Print($"call event{handler}");
            }
        }
    }
}
