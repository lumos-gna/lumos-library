using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LumosLib
{
    public class PreInitializer : SingletonGlobal<PreInitializer>
    {
        public bool Initialized { get; private set; }
        
        
        private List<IPreInitialize> PreInitializes = new();
        
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            _ = Instance;
        }

        protected override void Awake()
        {
            base.Awake();
            
            var config = Resources.Load<PreInitializerConfigSO>(Constant.PreInitializerConfig);
            if (config == null)
            {
                DebugUtil.LogWarning($" not found {Constant.PreInitializerConfig} "," INIT FAIL ");
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
            
            StartCoroutine(InitAsync(PreInitializes));
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


            Initialized = true;
         
            Destroy(gameObject);
        }
    }
}