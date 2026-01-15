using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Cysharp.Threading.Tasks;
using TriInspector;

namespace LumosLib
{
    [DeclareBoxGroup("Resources", Title = "Resources")]
    public class UIManager : MonoBehaviour, IUIManager, IPreInitializable
    {
        #region >--------------------------------------------------- FIELDS

        
        [Group("Resources"), SerializeField, ReadOnly] private List<UIBase> _uiPrefabs;
         
        private Dictionary<Type, UIBase> _createdUIs = new();
        private Dictionary<Type, UIBase> _prefabUIs = new();
        

        #endregion
        #region >--------------------------------------------------- INIT
        
        
        public UniTask<bool> InitAsync()
        {
            foreach (var prefab in _uiPrefabs)
            {
                _prefabUIs[prefab.GetType()] = prefab;
            }
            
            GlobalService.Register<IUIManager>(this);
            return UniTask.FromResult(true);
        }
     
        
        #endregion
        #region >--------------------------------------------------- GET & SET

        
        public virtual T Get<T>() where T : UIBase
        {
            if (_createdUIs.TryGetValue(typeof(T), out var ui))
            {
                return ui as T;
            }
            
            return CreateUI<T>();
        }
        
        public virtual void SetEnable<T>(bool enable) where T : UIBase
        {
            var ui = Get<T>();

            if (ui == null) return;

            ui.SetEnable(enable);
        }
        
        public virtual void SetToggle<T>() where T : UIBase
        {
            var ui = Get<T>();

            if (ui == null) return;

            ui.SetEnable(!ui.IsEnabled);
        }
        
        
        #endregion
        #region >--------------------------------------------------- CREATE

        
        protected virtual T CreateUI<T>() where T : UIBase
        {
            var type = typeof(T);
            
            if (!_prefabUIs.TryGetValue(type, out var prefab)) return null;

            var createdUI = Instantiate(prefab, transform);
            
            _createdUIs[type] = createdUI;
            
            createdUI.gameObject.SetActive(false);
            
            return createdUI as T;
        }
        

        #endregion
        #region >--------------------------------------------------- INSPECTOR
        
        
        [Group("Resources"),
         Button("Collect UI Resources")]
        public void SetUIResources()
        {
            _uiPrefabs = new();
            
            var entries = ResourcesUtil.Find<UIBase>(this, "", SearchOption.AllDirectories);

            foreach (var entry in entries)
            {
                var result = entry.GetResource<UIBase>();
                if (result != null)
                {
                    _uiPrefabs.Add(result);
                }
            }
        }
        
        
        #endregion
    }
}