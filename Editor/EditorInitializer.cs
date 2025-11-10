using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LumosLib
{
    public static class EditorInitializer
    {
        [DidReloadScripts]
        private static void Initialize()
        {
            TryCreateConfig();
            
            AssetDatabase.Refresh();
        }
        
        
        private static void TryCreateConfig()
        {
            string path = Constant.PathPreInitializerConfig;
            
            PreInitializerConfigSO config = AssetDatabase.LoadAssetAtPath<PreInitializerConfigSO>(path);
            
            string folderPath = Path.GetDirectoryName(path);
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<PreInitializerConfigSO>();
                
                AssetDatabase.CreateAsset(config, path);
                AssetDatabase.SaveAssets();
            }
        }
    }
}