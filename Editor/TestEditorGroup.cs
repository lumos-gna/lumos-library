using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace LumosLib.Core
{
    public class TestEditorGroup : Editor
    {
        public string Title { get; private set; }

        private int _basicFontSize = 11;


        public void Init(string title)
        {
            Title = title;
        }


        #region >--------------------------------------------------- DRAW : BUTTON
        
        
        public void DrawButton(string label, UnityAction onClick, float width = -1, float height = -1)
        {
            List<GUILayoutOption> options = new();

            if (width > 0) options.Add(GUILayout.Width(width));
            if (height > 0) options.Add(GUILayout.Height(height));

            if (GUILayout.Button(label, options.ToArray()))
            {
                onClick?.Invoke();
            }
        }
        
        
        #endregion
        #region >--------------------------------------------------- DRAW : FIELD

        
        public void DrawSpaceLine()
        {
            EditorGUILayout.Space(_basicFontSize); 
        }
        
        public void DrawField(string label, ref int value)
        {
            value = EditorGUILayout.IntField(label, value); 
        }
        
        public void DrawField(string label, ref float value)
        {
            value = EditorGUILayout.FloatField(label, value); 
        }
        
        public void DrawField(string label, ref Vector2 value)
        {
            value = EditorGUILayout.Vector2Field(label, value); 
        }
        
        public void DrawField(string label, ref Vector3 value)
        {
            value = EditorGUILayout.Vector3Field(label, value); 
        }
        
        public void DrawField(string label, ref Vector4 value)
        {
            value = EditorGUILayout.Vector4Field(label, value);
        }
        
        public void DrawField(string label, ref string value)
        {
            value = EditorGUILayout.TextField(label, value); 
        }
        
        public void DrawField<T>(string label, ref T value) where T : Object
        {
            value = (T)EditorGUILayout.ObjectField(label, value, typeof(T), false);
        }
        
        public void DrawField(string label, ref Bounds value)
        {
            value = EditorGUILayout.BoundsField(label, value);
        }
        
        public void DrawField(string label, ref BoundsInt value)
        {
            value = EditorGUILayout.BoundsIntField(label, value);
        }
        
        public void DrawField(string label, ref Color value)
        {
            value = EditorGUILayout.ColorField(label, value);
        }
        
        public void DrawField(string label, ref Rect value)
        {
            value = EditorGUILayout.RectField(label, value);
        }
        
        public void DrawField(string label, ref RectInt value)
        {
            value = EditorGUILayout.RectIntField(label, value);
        }
        
        public void DrawField(string label, ref Enum value)
        {
            value = EditorGUILayout.EnumFlagsField(label, value);
        }
        
        public void DrawField(string label, ref bool value, UnityAction<bool> onClick)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(label, GUILayout.Width(EditorStyles.label.CalcSize(new GUIContent(label)).x + 10));
            
            bool newValue = GUILayout.Toggle(value, GUIContent.none);

            if (newValue != value)
            {
                value = newValue;
                onClick?.Invoke(value);
            }

            GUILayout.EndHorizontal();
        }


        #endregion
    }
}