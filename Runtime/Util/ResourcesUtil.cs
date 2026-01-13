using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LumosLib
{
    public static class ResourcesUtil
    {
        public static List<T> Find<T>(Object owner, string folderPath, SearchOption searchOption)
        {
#if UNITY_EDITOR
            
            Undo.RecordObject(owner, "Find Assets");

            var results = new List<T>();
            
            var path = folderPath.Replace("Assets/", "");
            path = path.Replace("Resources/", "");
            path = "Resources/" + path;
            
            string absolutePath = Path.Combine(Application.dataPath, path);
                
            if (!Directory.Exists(absolutePath))
            {
                DebugUtil.LogWarning($"folder path not found : {path}", "Resource");
                return null;
            }

            string[] files = Directory.GetFiles(absolutePath, "*.*", searchOption);
                
            foreach (var file in files)
            {
                if (file.EndsWith(".meta")) continue;

                string relativePath = "Assets" +
                                      file.Replace(Application.dataPath, "")
                                          .Replace("\\", "/");

                Object asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);
                
                if (typeof(Component).IsAssignableFrom(typeof(T)))
                {
                    if (asset == null) continue;

                    if (asset is GameObject go)
                    {
                        var component = go.GetComponent<T>();
                        if (component != null)
                        {
                            results.Add(component);
                        }
                    }
                }
                else
                {
                    if (asset != null)
                    {
                        if (asset is T t)
                        {
                            results.Add(t);
                        }
                    }
                }
            }
            
            if (results.Count > 0)
            {
                EditorUtility.SetDirty(owner);
            }

            DebugUtil.Log($"Find {results.Count} resources.", $"Complete");
            return results;
#endif
            return null;
        }
    }
}
