using System.Collections.Generic;
using UnityEngine;

namespace Lumos.DevKit
{
    public class BaseResourceManager : MonoBehaviour, IPreInitialize, IResourceManager
    {
        #region  >--------------------------------------------------- PROPERTIES
        
        
        public int PreInitOrder => (int)PreInitializeOrder.Resource;
        public bool PreInitialized { get; private set; }
        
        
        #endregion
        #region  >--------------------------------------------------- FIELDS


        private Dictionary<string, object> _resources = new();
        
        
        #endregion
        #region  >--------------------------------------------------- INIT


        public void PreInit()
        {
            Global.Register<IResourceManager>(this);
            
            PreInitialized = true;
        }
        
        
        #endregion
        #region  >--------------------------------------------------- LOAD


        public T Load<T>(string path) where T : Object
        {
            if (_resources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T;
            }

            var resource = Resources.Load<T>(path);
            if (resource != null)
            {
                _resources[path] = resource;
            }
            return resource;
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            if (_resources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T[];
            }

            var resource = Resources.LoadAll<T>(path);
            if (resource != null)
            {
                _resources[path] = resource;
            }
            return resource;
        }
        

        #endregion

    }
}