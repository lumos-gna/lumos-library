using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LumosLib
{
    public static class Global
    {
        #region >--------------------------------------------------- FIELD

        
        private static Dictionary<Type, object> _services = new();
        
        
        #endregion
        #region >--------------------------------------------------- REGISTER

        
        public static void Register<T>(T service) where T : class
        {
            _services[typeof(T)] = service;
        }

        public static void Unregister<T>() where T : class
        {
            var type = typeof(T);
            
            if (_services.ContainsKey(type))
            {
                _services.Remove(type);
            }
        }
        

        #endregion
        #region >--------------------------------------------------- GET

        
        public static T Get<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            DebugUtil.LogWarning($"{typeof(T)}", " NOT REGISTERED ");
            return default;
        }
        
        internal static T GetInternal<T>() where T : class
        {
            if (_services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            
            return null;
        }
        
        
        #endregion
    }
}