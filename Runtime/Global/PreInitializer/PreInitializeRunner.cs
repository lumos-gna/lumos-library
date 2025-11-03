using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Lumos.DevKit
{
    public class PreInitializeRunner : MonoBehaviour
    {
        public void Run()
        {
            StartCoroutine(InitAsync());
        }
        
        private IEnumerator InitAsync()
        {
            var startTime = Time.realtimeSinceStartup;
            
            var preIniTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch (ReflectionTypeLoadException e) { return e.Types.Where(t => t != null); }
                })
                .Where(t => typeof(IPreInitialize).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();
            
            
            var initInstances = new Queue<IPreInitialize>();
            
            foreach (var type in preIniTypes)
            {
                IPreInitialize instance;

                if (typeof(MonoBehaviour).IsAssignableFrom(type))
                {
                    var go = new GameObject(type.Name);
                    go.AddComponent(type);
                    
                    instance = go.GetComponent<IPreInitialize>();
                }
                else
                {
                    instance = (IPreInitialize)Activator.CreateInstance(type);
                }

                if (instance == null)
                {
                    DebugUtil.LogWarning($"{type.Name}", " FAIL CREATE INSTANCE ");
                    continue;
                }
                
                initInstances.Enqueue(instance);
            }
            
            
            var initQueue = new Queue<IPreInitialize>(initInstances.OrderBy(x => x.PreInitOrder));

            while (initQueue.Count > 0)
            {
                var target = initQueue.Dequeue();
                
                target.PreInit();
                
                yield return new WaitUntil(() => target.PreInitialized); 
                
                DebugUtil.Log(" INIT COMPLETE ", $" { target.GetType().Name } ");
            }
            

            PreInitializer.SetInitialized(true);
            
            Destroy(gameObject);

            DebugUtil.Log($"[ {Time.realtimeSinceStartup - startTime} ]", " All INIT COMPLETE ");
        }
    }
}