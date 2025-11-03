using System.Collections.Generic;

namespace Lumos.DevKit
{
    public class UIGlobalManager : UIBaseManager, IPreInitialize, IGlobal
    {
        #region >--------------------------------------------------- PROPERTIES

        
        public int PreInitOrder => (int)PreInitializeOrder.UI;
        public bool PreInitialized { get; private set; }


        #endregion
        #region >--------------------------------------------------- PROPERTIES


        private Dictionary<int, UIBase> _globalUIPrefabs = new();
        
        
        #endregion
        #region >--------------------------------------------------- INIT


        public void PreInit()
        {
            var uiGlobalPrefabs = Global.Get<BaseResourceManager>().LoadAll<UIBase>(Path.UI);

            for (int i = 0; i < uiGlobalPrefabs.Length; i++)
            {
                var key = uiGlobalPrefabs[i].ID;
                var value = uiGlobalPrefabs[i];

                _globalUIPrefabs[key] = value;
            }
            
            Global.Register(this);
            
            PreInitialized = true;
        }
  

        #endregion
        #region >--------------------------------------------------- GET & SET


        public override T Get<T>(int id)
        {
            var returnUi = base.Get<T>(id);
            
            if(returnUi != null) return returnUi;
            
            return TryCreateGlobalUI<T>(id);
        }
        
        private T TryCreateGlobalUI<T>(int id) where T : UIBase
        {
            if (!_globalUIPrefabs.TryGetValue(id, out var prefab)) return null;

            var createdUI = Instantiate(prefab, transform);
            
            Register(createdUI);
            
            return createdUI as T;
        }


        #endregion
    }
}