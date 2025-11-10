using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LumosLib
{
    public abstract class BaseGlobal
    {
        #region >--------------------------------------------------- PROPERTIE
        
        
        public static IUIManager UI
        {
            get => _ui ??= Get<IUIManager>();
            set => _ui = value;
        }
        
        public static IAudioManager Audio
        {
            get => _audio ??= Get<IAudioManager>();
            set => _audio = value;
        }
        
        public static IDataManager Data
        {
            get => _data ??= Get<IDataManager>();
            set => _data = value;
        }
        
        public static IPoolManager Pool
        {
            get => _pool ??= Get<IPoolManager>();
            set => _pool = value;
        }
        
        public static IResourceManager Resource
        {
            get => _resource ??= Get<IResourceManager>();
            set => _resource = value;
        }
        
        
        #endregion
        #region >--------------------------------------------------- FIELD

        
        private static Dictionary<Type, object> _services = new();
       
        private static IUIManager _ui;
        private static IAudioManager _audio;
        private static IDataManager _data;
        private static IPoolManager _pool;
        private static IResourceManager _resource;
        
        
        #endregion
        #region >--------------------------------------------------- REGISTER

        
        public static void Register<T>(T service) where T : class
        {
            if (_services.ContainsKey(typeof(T)))
            {
                Unregister<T>();
            }
         
            _services[typeof(T)] = service;
            
            if(service is MonoBehaviour serviceMono)
            {
                Object.DontDestroyOnLoad(serviceMono.gameObject);
            }
        }

        public static void Unregister<T>(T service) where T : class
        {
            Unregister<T>();
        }

        public static void Unregister<T>() where T : class
        {
            var service = Get<T>();
            if (service == null) return;
            
            _services.Remove(typeof(T));
            
            if(service is MonoBehaviour serviceMono)
            {
                Object.Destroy(serviceMono.gameObject);
            }
        }


        #endregion
        #region >--------------------------------------------------- GET & SET
        
        
        protected static T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            DebugUtil.LogWarning($"{typeof(T)}", " NOT REGISTERED ");
            return default;
        }
        
        
        #endregion
    }
}