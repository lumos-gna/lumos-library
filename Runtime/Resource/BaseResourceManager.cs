using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public abstract class BaseResourceManager : MonoBehaviour, IPreInitialize
    {
        #region  >--------------------------------------------------- PROPERTIE

        
        public int PreInitOrder => (int)PreInitializeOrder.Resource;
        public abstract bool PreInitialized { get; protected set; }
        
        
        #endregion
        #region  >--------------------------------------------------- FIELD


        protected Dictionary<string, object> cahcedResources = new();
        
        
        #endregion
        #region  >--------------------------------------------------- UNITY


        public virtual void Awake()
        {
            Global.Register((IResourceManager)this);
        }
        
        
        #endregion
    }
}