using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LumosLib
{
    public class PreInitializer : MonoBehaviour
    {
        public void Run(List<IPreInitialize> preInitializes)
        {
            StartCoroutine(InitAsync(preInitializes));
        }
        
        private IEnumerator InitAsync(List<IPreInitialize> preInitializes)
        {
            var startTime = Time.realtimeSinceStartup;
            DebugUtil.Log($" Start ", " INITIALIZED ");
            
            
            //Initialize
            for (int i = 0; i < preInitializes.Count; i++)
            {
                var initStartTime = Time.realtimeSinceStartup;
                
                var prefab = (preInitializes[i] as MonoBehaviour).gameObject;
                
                var instance = Instantiate(prefab);
                DontDestroyOnLoad(instance);
                
                var initialize = instance.GetComponent<IPreInitialize>();
                
                if (!initialize.PreInitialized)
                {
                    yield return new WaitUntil(() => initialize.PreInitialized); 
                }

                DebugUtil.Log($" { initialize.GetType().Name } ( {(Time.realtimeSinceStartup - initStartTime) * 1000f:F3} ms )", $" INITIALIZED ");
            }


            var totalElapsed = Time.realtimeSinceStartup - startTime;
            DebugUtil.Log($" Finish ( {totalElapsed * 1000f:F3} ms )", " INITIALIZED ");
            
            Global.SetInitialized(true);
         
            Destroy(gameObject);
        }
    }
}