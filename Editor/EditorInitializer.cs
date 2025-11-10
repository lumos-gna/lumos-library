using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace LumosLib
{
    public static class EditorInitializer
    {
        [DidReloadScripts]
        static void Initialize()
        {
            TryCreateConfig();
            CreateGlobalsScript();
        }
        
        
        private static void TryCreateConfig()
        {
            
            string path = $"Assets/Resources/{Constant.PreInitializerConfig}.asset"; 
            
            PreInitializerConfigSO config = AssetDatabase.LoadAssetAtPath<PreInitializerConfigSO>(path);
            
            
            string folderPath = Path.GetDirectoryName(path);
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                AssetDatabase.Refresh();
            }
            
            if (config == null)
            {
                config = ScriptableObject.CreateInstance<PreInitializerConfigSO>();
                
                AssetDatabase.CreateAsset(config, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
        
        private static void CreateGlobalsScript()
        {
            string templatePath = $"{Constant.LumosLib}/Editor/Templates/{Constant.TemplateGlobal}.txt";
            string path = $"Assets/Scripts/{Constant.Global}.cs";
            
            
            string[] guids = AssetDatabase.FindAssets("Globals t:MonoScript", new[] { "Assets" });

            if (guids.Length > 0)
            {
                return;
            }

            
            string template = File.ReadAllText(templatePath);
            
            File.WriteAllText(path, template);

            AssetDatabase.Refresh();
            
        }
    }
}