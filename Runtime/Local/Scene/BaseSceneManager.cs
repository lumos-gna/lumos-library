using System.Collections;
using UnityEngine;

namespace Lumos.DevKit
{
    public abstract class BaseSceneManager : SingletonScene<BaseSceneManager>
    {
        #region --------------------------------------------------- UNITY


        protected override void Awake()
        {
            base.Awake();
            
            StartCoroutine(InitAsync());
        }


        #endregion
        #region --------------------------------------------------- INIT


        protected abstract void Init();
        
        private IEnumerator InitAsync() 
        {
            yield return new WaitUntil(() => PreInitializer.Initialized);

            Init();
        }


        #endregion
    }
}