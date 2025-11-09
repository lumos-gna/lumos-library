using UnityEngine;

namespace LumosLib
{
    public class ResourceManager : BaseResourceManager, IResourceManager
    {
        #region  >--------------------------------------------------- PROPERTIES


        public override bool PreInitialized { get; protected set; } = true;
        
        
        #endregion
        #region  >--------------------------------------------------- LOAD


        public T Load<T>(string path) where T : Object
        {
            if (cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T;
            }

            return Resources.Load<T>(path);
        }

        public T[] LoadAll<T>(string path) where T : Object
        {
            if (cahcedResources.TryGetValue(path, out var cacheResource))
            {
                return cacheResource as T[];
            }

            return Resources.LoadAll<T>(path);
        }

        #endregion
    }
}