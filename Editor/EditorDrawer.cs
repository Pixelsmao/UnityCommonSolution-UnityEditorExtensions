using System;
using UnityEditor;
using UnityEngine;

namespace Pixelsmao.UnityCommonSolution.UnityEditorExtensions
{
    public abstract class EditorDrawer : UnityEditor.Editor
    {
        #region LinkButton

        protected void DrawLinkButton(GUIContent content, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(content, options)) Application.OpenURL(url);
        }

        protected void DrawLinkButton(GUIContent content, GUIStyle style, string url,
            params GUILayoutOption[] options)
        {
            if (GUILayout.Button(content, style, options)) Application.OpenURL(url);
        }

        protected void DrawLinkButton(string label, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) Application.OpenURL(url);
        }

        protected void DrawLinkButton(Texture texture, GUIStyle style, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(texture, style, options)) Application.OpenURL(url);
        }

        protected void DrawLinkButton(Texture texture, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(texture, options)) Application.OpenURL(url);
        }

        protected void DrawLinkButton(string label, GUIStyle style, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) Application.OpenURL(url);
        }

        protected void DrawLink(string link, Action onClick = null, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(options);
            {
                EditorGUILayout.LabelField(link, EditorGUIStyles.linkStyle);
                var linkRect = GUILayoutUtility.GetLastRect();
                EditorGUIUtility.AddCursorRect(linkRect, MouseCursor.Link);
                if (Event.current.type == EventType.MouseDown && linkRect.Contains(Event.current.mousePosition))
                {
                    if (onClick != null) onClick.Invoke();
                    else Application.OpenURL(link);
                    Event.current.Use();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}