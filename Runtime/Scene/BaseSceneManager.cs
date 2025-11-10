using System.Collections;
using UnityEngine;

namespace LumosLib
{
    public abstract class BaseSceneManager : SingletonScene<BaseSceneManager>
    {
        #region --------------------------------------------------- UNITY


        protected override void Awake()
        {
            base.Awake();
            
            StartCoroutine(InitAsync());
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            BaseGlobal.Unregister(this);
        }


        #endregion
        #region --------------------------------------------------- INIT


        protected virtual void Init()
        {
            BaseGlobal.Register(this);
        }
        
        private IEnumerator InitAsync() 
        {
            yield return new WaitUntil(() => PreInitializer.Instance.Initialized);

            Init();
        }


        #endregion
    }
}