using System.IO;
using UnityEditor;
using UnityEngine;

namespace LumosLib
{
    [InitializeOnLoad]
    public static class EditorInitializer
    {
        static EditorInitializer()
        {
            EditorApplication.delayCall += OnEditorFullyLoaded;
        }

        private static void OnEditorFullyLoaded()
        {
            if (Resources.Load<PreInitializeConfigSO>(Constant.PreInitializerConfig) == null)
            {
                string resourcesDir = Path.Combine(Application.dataPath, "Resources");
                if (!Directory.Exists(resourcesDir))
                {
                    Directory.CreateDirectory(resourcesDir);
                }
                
                var config = ScriptableObject.CreateInstance<PreInitializeConfigSO>();
                
                string assetPath = "Assets/Resources/" + Constant.PreInitializerConfig + ".asset";
                AssetDatabase.CreateAsset(config, assetPath);
                AssetDatabase.SaveAssets();
            }
        }
    }
}