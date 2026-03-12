using System;

namespace FPS.Room5
{
    public interface IEventBus<TBase> where TBase : class
    {
        void Subscribe<T>(Action<T> handler) where T : TBase;
        void Unsubscribe<T>(Action<T> handler) where T : TBase;
        void Publish<T>(T eventData) where T : TBase;
    }
}
