using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LumosLib
{
    public static class Global
    {
        #region >--------------------------------------------------- PROPERTIE


        public static bool Initialized { get; private set; }
     

        #endregion
        #region >--------------------------------------------------- FIELD

        
        private static readonly Dictionary<Type, IGlobal> Services = new();
        private static List<IPreInitialize> PreInitializes = new();
        
        
        #endregion
        #region >--------------------------------------------------- INIT

        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var config = Resources.Load<GlobalConfigSO>(Constant.GlobalConfig);
            if (config == null)
            {
                DebugUtil.LogWarning($" not found {Constant.GlobalConfig} "," INIT FAIL ");
                return;
            }
            
            foreach (var mono in config.PreInitializes)
            {
                if (mono is IPreInitialize preInit)
                {
                    PreInitializes.Add(preInit);
                }
            }
            
            var idHash = new HashSet<int>();
            PreInitializes.RemoveAll(x => !idHash.Add(x.PreInitOrder));
            PreInitializes = PreInitializes.OrderBy(x => x.PreInitOrder).ToList();
            
            var initializer = new GameObject("Initializer").AddComponent<PreInitializer>();
            initializer.Run(PreInitializes);
        }
        
        
        #endregion
        #region >--------------------------------------------------- REGISTER

        
        public static void Register<T>(T service) where T : IGlobal
        {
            if (Services.ContainsKey(typeof(T)))
            {
                Unregister<T>();
            }
         
            Services[typeof(T)] = service;
            
            if(service is MonoBehaviour serviceMono)
            {
                Object.DontDestroyOnLoad(serviceMono.gameObject);
            }
        }

        public static void Unregister<T>(T service) where T : IGlobal
        {
            Unregister<T>();
        }

        public static void Unregister<T>() where T : IGlobal
        {
            var service = Get<T>();
            if (service == null) return;
            
            Services.Remove(typeof(T));
            
            if(service is MonoBehaviour serviceMono)
            {
                Object.Destroy(serviceMono.gameObject);
            }
        }


        #endregion
        #region >--------------------------------------------------- GET & SET
        
        
        public static T Get<T>() where T : IGlobal
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            DebugUtil.LogWarning($"{typeof(T)}", " NOT REGISTERED ");
            return default;
        }
        
        public static void SetInitialized(bool enabled)
        {
            Initialized = enabled;
        }
        
        
        #endregion
    }
}