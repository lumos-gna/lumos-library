using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseGameManager : SingletonGlobalMono<BaseGameManager>
{
    #region  --------------------------------------------------- PROPERTIES
    
    public bool IsInitialized { get; private set; }
    #endregion
    
    #region  --------------------------------------------------- FIEDLS

    private List<ManagerComponent> _mgrComponents = new();
    #endregion
    
    #region  --------------------------------------------------- UNITY
    
    protected override void Awake()
    {
        base.Awake();

        AddManagerComponents();
        
        StartCoroutine(InitManagerComponents());
    }
    #endregion
    
    #region  --------------------------------------------------- INIT 
 
    private IEnumerator InitManagerComponents()
    {
        _mgrComponents = _mgrComponents.OrderBy(manager => manager.Order).ToList();
        
        foreach (var manager in _mgrComponents)
        {
            manager.Init();

            yield return new WaitUntil(() => manager.IsInitialized);
            DebugUtil.Log(" INIT COMPLETE ", $" {manager.GetType()} ");
        }

        IsInitialized = true;
        
        DebugUtil.Log("", " All Managers INIT COMPLETE ");
    }
    #endregion
    
    #region  --------------------------------------------------- ADD

    protected virtual void AddManagerComponents()
    {
        
    }
    
    protected void AddManagerComponent<T>(T mgrComponent) where T : ManagerComponent
    {
        GameObject go = new GameObject(typeof(T).Name);
        go.transform.SetParent(transform);
        
        var instance = go.AddComponent<T>();
        _mgrComponents.Add(instance);
        
        Global.Register(instance);
    }
    #endregion
}