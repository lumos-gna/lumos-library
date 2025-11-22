using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LumosLib
{
    public class InputManager : MonoBehaviour, IPreInitializer
    {
        #region >--------------------------------------------------- PROPERTIE

        
        public int PreInitOrder => (int)PreInitializeOrder.Input;


        #endregion
        #region >--------------------------------------------------- FIELD


        private InputActionAsset _inputAsset;
        private readonly Dictionary<string, InputAction> _actionTable = new();
        private readonly Dictionary<string, InputActionMap> _mapTable = new();


        #endregion

        public IEnumerator InitAsync()
        {
            _inputAsset = Project.Config.InputAsset;

            if (_inputAsset == null)
            {
                Project.PrintInitFail("Input Asset is Null");
            }

            CacheMapsAndActions();

            yield break;
        }

        protected virtual void Awake()
        {
            GlobalService.Register(this);

            DontDestroyOnLoad(gameObject);
        }

        private void CacheMapsAndActions()
        {
            foreach (var map in _inputAsset.actionMaps)
            {
                _mapTable[map.name] = map;

                foreach (var action in map.actions)
                {
                    string key = $"{map.name}/{action.name}";
                    _actionTable[key] = action;
                }
            }
        }


        public void SetEnableMap(string mapName, bool enable)
        {
            if (_mapTable.TryGetValue(mapName, out var map))
            {
                if (enable) map.Enable();
                else map.Disable();
            }
            else
            {
                DebugUtil.LogWarning($"[InputManager] Map '{mapName}'", "Not Found");
            }
        }

        public void SetEnableMapAll(bool enable)
        {
            foreach (var map in _mapTable.Values)
            {
                if (enable) map.Enable();
                else map.Disable();
            }
        }

        public InputAction GetAction(string mapName, string actionName)
        {
            string key = $"{mapName}/{actionName}";

            if (_actionTable.TryGetValue(key, out var action))
            {
                return action;
            }

            DebugUtil.LogWarning($"[InputManager] Action '{key}'", "Not Found");
            return null;
        }
    }
}