using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace LumosLib
{
    public abstract class BaseTestEditorWindow : EditorWindow
    {
        #region >--------------------------------------------------- PROPERTIE

        protected int TitleFontSize { get; set; } = 20;
        protected int GroupTitleFontSize { get; set; } = 12;
        protected Color TitleColor { get; set; } = Color.yellow;
        protected Color GroupTitleColor { get; set; } = Color.cyan;
        protected Color UntoggledGroupTitleColor { get; set; } = Color.gray;
        
        
        #endregion
        #region >--------------------------------------------------- FIELD

        
        private static string _title;
        private Vector2 _scrollPos;
        
        
        #endregion
        #region >--------------------------------------------------- CORE
        
        
        protected static void OnOpen<T>(string title) where T : BaseTestEditorWindow
        {
            var window = GetWindow<T>();
            window.titleContent = new GUIContent(title);
            window.Show();
            _title = title;
        }

        protected virtual void OnGUI()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(
                _scrollPos,
                false,
                true,
                GUIStyle.none,
                GUI.skin.verticalScrollbar,
                GUIStyle.none
                );
            
            DrawTitle(_title);
        }

        protected TestEditorGroup CreateGroup(string title)
        {
            var group = CreateInstance<TestEditorGroup>();
            group.Init(title);
            
            return group;
        }

        protected void FinishDraw()
        {
            EditorGUILayout.Space(TitleFontSize);
            EditorGUILayout.EndScrollView();
        }
        
        
        #endregion
        #region >--------------------------------------------------- DRAW : TITLE
        
        
        private void DrawTitle(string title)
        {
            EditorGUILayout.Space(TitleFontSize);
            
            EditorGUILayout.LabelField(title, new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = TitleFontSize,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = TitleColor },
                focused = { textColor = TitleColor },
                hover =  { textColor = TitleColor },
                active = { textColor = TitleColor },
            });
            
            EditorGUILayout.Space(TitleFontSize);
            EditorGUILayout.Space(GroupTitleFontSize);
        }
        
        
        #endregion
        #region >--------------------------------------------------- DRAW : GROP
      
        
        protected void DrawGroup(TestEditorGroup group, UnityAction<TestEditorGroup> contents)
        {
            EditorGUILayout.LabelField(group.Title, new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = GroupTitleFontSize,
                normal = { textColor = GroupTitleColor },
                focused = { textColor = GroupTitleColor },
                hover =  { textColor = GroupTitleColor },
                active = { textColor = GroupTitleColor },
            });
            EditorGUILayout.BeginVertical("box");
            contents?.Invoke(group);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(GroupTitleFontSize);
        }

        protected void DrawToggleGroup(TestEditorGroup group, ref bool isToggled, UnityAction<TestEditorGroup> contents)
        {
            isToggled = EditorGUILayout.Foldout(isToggled, group.Title, true, new GUIStyle(EditorStyles.foldout)
            {
                fontSize = GroupTitleFontSize,
                fontStyle = FontStyle.Bold,
                normal = { textColor = UntoggledGroupTitleColor },
                focused = { textColor = UntoggledGroupTitleColor },
                active = { textColor = UntoggledGroupTitleColor },
                hover = { textColor = UntoggledGroupTitleColor },
                onNormal = { textColor = GroupTitleColor },
                onFocused =  { textColor = GroupTitleColor },
                onHover = { textColor = GroupTitleColor },
                onActive = { textColor = GroupTitleColor },
            });

            if (isToggled)
            {
                EditorGUILayout.BeginVertical("box");
                contents?.Invoke(group); 
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(GroupTitleFontSize);
            }
        }
        
        #endregion
        
    }
}
