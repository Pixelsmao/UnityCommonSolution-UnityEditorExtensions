using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pixelsmao.UnityCommonSolution.UnityEditorExtensions
{
    public static class InspectorEditorExtensions
    {
        public static void FixEditorBug<T>(this InspectorEditor<T> editor) where T : MonoBehaviour
        {
            var remainingBuggedEditors = Object.FindObjectsOfType<InspectorEditor<T>>();
            foreach (var remainingEditor in remainingBuggedEditors)
            {
                if (editor == remainingEditor) continue;
                Object.DestroyImmediate(remainingEditor);
            }
        }

        public static void ApplyModifiedProperties(this Editor editor)
        {
            editor.serializedObject.ApplyModifiedProperties();
            if (GUI.changed) EditorUtility.SetDirty(editor.target);
        }

        /// <summary>
        /// 通过 Type 获取对应的 MonoScript
        /// </summary>
        /// <param name="type">目标 Editor 类型</param>
        /// <returns>MonoScript 或 null</returns>
        private static MonoScript FindMonoScriptByType(System.Type type)
        {
            var monoScripts = MonoImporter.GetAllRuntimeMonoScripts();

            return monoScripts.FirstOrDefault(script => script.GetClass() == type);
        }

        #region Draw Properties

        /// <summary>
        /// 绘制脚本属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        public static void DrawScriptProperty(this Editor editor)
        {
            editor.DrawReadOnlyProperty("m_Script");
        }

        /// <summary>
        /// 绘制脚本编辑器属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        public static void DrawEditorScriptProperty(this Editor editor)
        {
            var editorType = editor.GetType();
            var editorScript = FindMonoScriptByType(editorType);
            var serializedObject = new SerializedObject(editorScript);
            var scriptProperty = serializedObject.FindProperty("m_Script");
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(scriptProperty);
            EditorGUI.EndDisabledGroup();
        }

        /// <summary>
        /// 在检查器上绘制一个属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawPropertyField(this Editor editor, string propertyName, params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(editor.serializedObject.FindProperty(propertyName), options);
        }

        /// <summary>
        /// 在检查器上绘制一个属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawPropertyField(this Editor editor, string propertyName, bool includeChildren,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(editor.serializedObject.FindProperty(propertyName), includeChildren, options);
        }

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="toolTip">提示内容</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawPropertyField(this Editor editor, string propertyName, string toolTip,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(editor.serializedObject.FindProperty(propertyName),
                new GUIContent(propertyName, toolTip), options);
        }

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="toolTip">提示内容</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawPropertyField(this Editor editor, string propertyName, string toolTip,
            bool includeChildren,
            params GUILayoutOption[] options)
        {
            EditorGUILayout.PropertyField(editor.serializedObject.FindProperty(propertyName),
                new GUIContent(propertyName, toolTip), includeChildren, options);
        }

        /// <summary>
        /// 在检查器上绘制一个只读属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawReadOnlyProperty(this Editor editor, string propertyName,
            params GUILayoutOption[] options)
        {
            GUI.enabled = false;
            editor.DrawPropertyField(propertyName, options);
            GUI.enabled = true;
        }

        /// <summary>
        /// 在检查器上绘制一个只读属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">Property Name</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawReadOnlyProperty(this Editor editor, string propertyName, bool includeChildren,
            params GUILayoutOption[] options)
        {
            GUI.enabled = false;
            editor.DrawPropertyField(propertyName, includeChildren, options);
            GUI.enabled = true;
        }

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的只读属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawReadOnlyProperty(this Editor editor, string propertyName, string tooltip,
            params GUILayoutOption[] options)
        {
            GUI.enabled = false;
            editor.DrawPropertyField(propertyName, tooltip, options);
            GUI.enabled = true;
        }

        /// <summary>
        /// 在检查器上绘制缩进属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawIndentPropertyField(this Editor editor, string propertyName, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            editor.DrawPropertyField(propertyName, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        /// <summary>
        /// 在检查器上绘制一个带工具提示的缩进属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawIndentPropertyField(this Editor editor, string propertyName, string tooltip,
            int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            editor.DrawPropertyField(propertyName, tooltip, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        /// <summary>
        /// 在检查器上绘制一个缩进的只读属性字段
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawIndentReadOnlyPropertyField(this Editor editor, string propertyName, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            editor.DrawReadOnlyProperty(propertyName, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        /// <summary>
        /// 在检查器上绘制一个带工具提示的缩进只读属性字段
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void DrawIndentReadOnlyPropertyField(this Editor editor, string propertyName, string tooltip,
            int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            editor.DrawReadOnlyProperty(propertyName, tooltip, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        #endregion

        #region Draw GUI

        /// <summary>
        /// 绘制水平线
        /// </summary>
        /// <param name="editor">editor</param>
        /// <param name="color">线颜色</param>
        /// <param name="lineHeight">线粗细</param>
        public static void DrawHorizontalLine(this Editor editor, Color color, float lineHeight = 1.25f)
        {
            EditorGUILayout.Space(0.5f);
            var lineRect = EditorGUILayout.GetControlRect(false, lineHeight);
            EditorGUI.DrawRect(lineRect, color);
            EditorGUILayout.Space(0.5f);
        }

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="_"></param>
        /// <param name="label">文本内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void IndentLabel(this Editor _, string label, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            EditorGUILayout.LabelField(label, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="_"></param>
        /// <param name="label">文本内容</param>
        /// <param name="style">文本GUI样式</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void IndentLabel(this Editor _, string label, GUIStyle style, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            EditorGUILayout.LabelField(label, style, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="_"></param>
        /// <param name="label">文本内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void IndentLabel(this Editor _, GUIContent label, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            EditorGUILayout.LabelField(label, options);
            EditorGUI.indentLevel -= indentLevel;
        }


        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="_"></param>
        /// <param name="label">文本1内容</param>
        /// <param name="label2">文本2内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void IndentLabel(this Editor _, GUIContent label, GUIContent label2, int indentLevel = 1,
            params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            EditorGUILayout.LabelField(label, label2, options);
            EditorGUI.indentLevel -= indentLevel;
        }


        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="_"></param>
        /// <param name="label">文本1内容</param>
        /// <param name="label2">文本2内容</param>
        /// <param name="style">文本样式</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        public static void IndentLabel(this Editor _, GUIContent label, GUIContent label2, GUIStyle style,
            int indentLevel = 1, params GUILayoutOption[] options)
        {
            EditorGUI.indentLevel += indentLevel;
            EditorGUILayout.LabelField(label, label2, style, options);
            EditorGUI.indentLevel -= indentLevel;
        }

        #endregion

        #region LinkButton

        public static void DrawLinkButton(GUIContent content, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(content, options)) Application.OpenURL(url);
        }

        public static void DrawLinkButton(GUIContent content, GUIStyle style, string url,
            params GUILayoutOption[] options)
        {
            if (GUILayout.Button(content, style, options)) Application.OpenURL(url);
        }

        public static void DrawLinkButton(string label, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) Application.OpenURL(url);
        }

        public static void DrawLinkButton(Texture texture, GUIStyle style, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(texture, style, options)) Application.OpenURL(url);
        }

        public static void DrawLinkButton(Texture texture, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(texture, options)) Application.OpenURL(url);
        }

        public static void DrawLinkButton(string label, GUIStyle style, string url, params GUILayoutOption[] options)
        {
            if (GUILayout.Button(label, options)) Application.OpenURL(url);
        }

        public static void DrawLink(string link, Action onClick = null, params GUILayoutOption[] options)
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