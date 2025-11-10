using UnityEditor;
using UnityEngine;

namespace LumosLib
{
    public class EditorHierarchyMenu
    {
        [MenuItem("GameObject/[ ✨Lumos Lib ]/Test", false, 0)]
        private static void CreateCustomScriptObject(MenuCommand menuCommand)
        {
            CreateResource(menuCommand, "AudioPlayer");
        }

        private static void CreateResource(MenuCommand menuCommand, string path)
        {
            var resource = Resources.Load<GameObject>(path);
            if (resource == null) return;
            
            GameObject parent = menuCommand.context as GameObject;
            GameObject createObject = (GameObject)PrefabUtility.InstantiatePrefab(resource);

            if (parent != null)
            {
                GameObjectUtility.SetParentAndAlign(createObject, parent);
            }

            Undo.RegisterCreatedObjectUndo(createObject, "Create " + createObject.name);
            
            Selection.activeObject = createObject;
        }
    }
}