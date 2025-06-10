using System;
using UnityEditor;
using UnityEngine;

namespace Pixelsmao.UnityCommonSolution.UnityEditorExtensions
{
    public abstract class InspectorEditor<T> :EditorDrawer where T : MonoBehaviour
    {
        protected T self { get; private set; }

        protected virtual void OnEnable()
        {
            self = target as T;
            this.FixEditorBug();
        }

        public override void OnInspectorGUI()
        {
            DrawScriptProperty();
        }

        protected void ApplyModifiedProperties() => InspectorEditorExtensions.ApplyModifiedProperties(this);

        #region Draw Properties

        /// <summary>
        /// 绘制脚本属性字段
        /// </summary>
        protected void DrawScriptProperty() => InspectorEditorExtensions.DrawScriptProperty(this);

        /// <summary>
        /// 绘制脚本编辑器属性字段
        /// </summary>
        protected void DrawEditorScriptProperty() => InspectorEditorExtensions.DrawEditorScriptProperty(this);

        /// <summary>
        /// 在检查器上绘制一个属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawPropertyField(string propertyName, params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawPropertyField(this, propertyName, options);

        /// <summary>
        /// 在检查器上绘制一个属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawPropertyField(string propertyName, bool includeChildren,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawPropertyField(this, propertyName, includeChildren, options);

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="toolTip">提示内容</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawPropertyField(string propertyName, string toolTip, params GUILayoutOption[] options)
            => InspectorEditorExtensions.DrawPropertyField(this, propertyName, toolTip, options);

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="toolTip">提示内容</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawPropertyField(string propertyName, string toolTip,
            bool includeChildren,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawPropertyField(this, propertyName, toolTip, includeChildren, options);

        /// <summary>
        /// 在检查器上绘制一个只读属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawReadOnlyProperty(string propertyName, params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawReadOnlyProperty(this, propertyName, options);

        /// <summary>
        /// 在检查器上绘制一个只读属性字段
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        /// <param name="includeChildren">包括子字段</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawReadOnlyProperty(string propertyName, bool includeChildren,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawReadOnlyProperty(this, propertyName, includeChildren, options);

        /// <summary>
        /// 在检查器上绘制一个带有工具提示的只读属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawReadOnlyProperty(string propertyName, string tooltip, params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawReadOnlyProperty(this, propertyName, tooltip, options);

        /// <summary>
        /// 在检查器上绘制缩进属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawIndentPropertyField(string propertyName, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawIndentPropertyField(this, propertyName, indentLevel, options);

        /// <summary>
        /// 在检查器上绘制一个带工具提示的缩进属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawIndentPropertyField(string propertyName, string tooltip, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawIndentPropertyField(this, propertyName, tooltip, indentLevel, options);

        /// <summary>
        /// 在检查器上绘制一个缩进的只读属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawIndentReadOnlyPropertyField(string propertyName, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawIndentReadOnlyPropertyField(this, propertyName, indentLevel, options);

        /// <summary>
        /// 在检查器上绘制一个带工具提示的缩进只读属性字段
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="tooltip">提示内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void DrawIndentReadOnlyPropertyField(string propertyName, string tooltip, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.DrawIndentReadOnlyPropertyField(this, propertyName, tooltip, indentLevel,
                options);

        #endregion

        #region Draw GUI

        /// <summary>
        /// 绘制水平线
        /// </summary>
        /// <param name="color">线颜色</param>
        /// <param name="lineHeight">线粗细</param>
        protected void DrawHorizontalLine(Color color, float lineHeight = 1.25f) =>
            InspectorEditorExtensions.DrawHorizontalLine(this, color, lineHeight);


        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="label">文本内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void IndentLabel(string label, int indentLevel = 1, params GUILayoutOption[] options) =>
            InspectorEditorExtensions.IndentLabel(this, label, indentLevel, options);

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="label">文本内容</param>
        /// <param name="style">文本GUI样式</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void IndentLabel(string label, GUIStyle style, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.IndentLabel(this, label, style, indentLevel, options);

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="label">文本内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void IndentLabel(GUIContent label, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.IndentLabel(this, label, indentLevel, options);

        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="label">文本1内容</param>
        /// <param name="label2">文本2内容</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void IndentLabel(GUIContent label, GUIContent label2, int indentLevel = 1,
            params GUILayoutOption[] options) =>
            InspectorEditorExtensions.IndentLabel(this, label, label2, indentLevel, options);


        /// <summary>
        /// 绘制缩进文本
        /// </summary>
        /// <param name="label">文本1内容</param>
        /// <param name="label2">文本2内容</param>
        /// <param name="style">文本样式</param>
        /// <param name="indentLevel">缩进级别</param>
        /// <param name="options">GUI布局选项</param>
        protected void IndentLabel(GUIContent label, GUIContent label2, GUIStyle style,
            int indentLevel = 1, params GUILayoutOption[] options) =>
            InspectorEditorExtensions.IndentLabel(this, label, label2, style, indentLevel, options);

        #endregion
    }
}