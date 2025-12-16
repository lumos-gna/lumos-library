using System;

namespace LumosLib
{
    public interface IEventManager
    {
        void Subscribe<T>(Action<T> listener);
        void Unsubscribe<T>(Action<T> listener);
        void Publish<T>(T evt);
    }
}
