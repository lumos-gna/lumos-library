using System.Collections;
using UnityEngine;

namespace LumosLib
{
    public abstract class BaseSceneManager<T> : MonoBehaviour where T : BaseSceneManager<T>
    {
        #region --------------------------------------------------- UNITY


        protected virtual void Awake()
        {
            StartCoroutine(InitAsync());
        }

        protected virtual void OnDestroy()
        {
            Global.Unregister<T>();
        }


        #endregion
        #region --------------------------------------------------- INIT


        protected virtual void Init()
        {
            Global.Register(this as T);
        }
        
        private IEnumerator InitAsync() 
        {
            yield return new WaitUntil(() => PreInitializer.Instance.Initialized);

            Init();
        }


        #endregion
    }
}