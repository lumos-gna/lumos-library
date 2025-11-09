using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace LumosLib
{
    public class DataManager : MonoBehaviour, IPreInitialize, IDataManager
    {
        public int PreInitOrder => (int)PreInitializeOrder.Data;
        public bool PreInitialized { get; private set; }
        
        private Dictionary<Type, Dictionary<int, IData>> _loadDatas = new();


        private void Awake()
        {
            StartCoroutine(LoadDataAsync());
        }

        private IEnumerator LoadDataAsync()
        {
            var jsonLoader = new GoogleSheetLoader();

            yield return jsonLoader.LoadJsonAsync();

            if (jsonLoader.Json == "")
            {
                yield break;
            }
            
            var allSheets = JsonConvert.DeserializeObject<Dictionary<string, object[]>>(jsonLoader.Json);

            var targetAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => 
                    a.GetName().Name == "Assembly-CSharp" || 
                    a.GetName().Name.StartsWith(GetType().Assembly.GetName().Name))
                .ToArray();
            
            var dataTypes = targetAssemblies
                .SelectMany(a =>
                {
                    try { return a.GetTypes(); }
                    catch (ReflectionTypeLoadException e) { return e.Types.Where(t => t != null); }
                })
                .Where(t => typeof(IData).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

            
            foreach (var type in dataTypes)
            {
                if (allSheets.TryGetValue(type.Name, out var sheetJson))
                {
                    string sheetJsonStr = JsonConvert.SerializeObject(sheetJson);
                    var listType = typeof(List<>).MakeGenericType(type);
                    var list = (IList)JsonConvert.DeserializeObject(sheetJsonStr, listType);

                    var dict = new Dictionary<int, IData>();
                    
                    foreach (var item in list)
                    {
                        var data = (IData)item;
                        dict[data.ID] = data;
                    }
                    
                    _loadDatas[type] = dict;
                }
                else
                {
                    DebugUtil.LogWarning($" haven't sheet '{type.Name}'" , " LOAD FAIL ");
                }
            }
            
            Global.Register<IDataManager>(this);
            
            PreInitialized = true;
        }
        
        
        public List<T> GetDataAll<T>() where T : IData
        {
            if (_loadDatas.TryGetValue(typeof(T), out var dict))
            {
                return dict.Values.Cast<T>().ToList();
            }

            DebugUtil.LogError($" haven't data '{typeof(T).Name}' ", " GET FAIL ");
            return new List<T>();
        }
    }
}