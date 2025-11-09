using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace LumosLib
{
    public abstract class BasePoolManager : MonoBehaviour, IPreInitialize, IPoolManager
    {
        public int PreInitOrder  => (int)PreInitializeOrder.Pool;
        public abstract bool PreInitialized { get; protected set; }
        
        
        protected Dictionary<string, object> pools = new();
        protected Dictionary<string, HashSet<MonoBehaviour>> activeObjects = new();


        protected void Awake()
        {
            Global.Register<IPoolManager>(this);
        }
      

        protected abstract ObjectPool<T> CreatePool<T>(string key, T prefab, int defaultCapacity, int maxSize) where T : MonoBehaviour, IPoolable;
        public abstract ObjectPool<T> GetPool<T>(T prefab, int defaultCapacity, int maxSize) where T : MonoBehaviour, IPoolable;
        public abstract T Get<T>(T prefab) where T : MonoBehaviour, IPoolable;
        public abstract void Release<T>(T obj) where T : MonoBehaviour, IPoolable;
        public abstract void DestroyActiveObjectsAll();
        public abstract void DestroyActiveObjects<T>(T prefab) where T : MonoBehaviour, IPoolable;

    }
}