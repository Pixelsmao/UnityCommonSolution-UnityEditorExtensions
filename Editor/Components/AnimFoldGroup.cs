using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Pixelsmao.UnityCommonSolution.UnityEditorExtensions
{
    public sealed class AnimFoldGroup : IDisposable
    {
        #region Styles

        private static GUIStyle _collapsableSectionStyle;
        private static GUIStyle _buttonFoldoutStyle;
        private static GUIStyle _helpBoxNoPaddingStyle;
        private static bool _isStylesInitialized;

        private static void EnsureStylesInitialized()
        {
            if (_isStylesInitialized) return;

            _collapsableSectionStyle = new GUIStyle(GUI.skin.box)
            {
                padding = { top = 0, bottom = 0 }
            };

            _buttonFoldoutStyle = new GUIStyle(EditorStyles.foldout)
            {
                margin = new RectOffset(),
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };

            _helpBoxNoPaddingStyle = new GUIStyle(EditorStyles.helpBox)
            {
                padding = new RectOffset(),
                overflow = new RectOffset(),
                margin = new RectOffset(8, 0, 0, 0),
                stretchWidth = false,
                stretchHeight = false
            };

            _isStylesInitialized = true;
        }

        #endregion

        #region Core

        public Color backgroundColor;
        private const string SettingsPrefix = "EditorExtensions-";
        private const float CollapseSpeed = 2f;
        private readonly AnimBool _animBool;
        private readonly Action _drawAction;
        private readonly List<AnimFoldGroup> _siblingGroups;
        private readonly string _editorPrefsKey;
        private readonly Editor _editor;

        public GUIContent Label { get; }
        public Color BackgroundColor { get; }

        public bool IsExpanded
        {
            get => _animBool.target;
            set
            {
                if (_animBool.target == value) return;
                _animBool.target = value;
                if (value) CollapseSiblings();
                SaveState();
            }
        }

        public float Faded => _animBool.faded;

        #endregion

        public AnimFoldGroup(
            GUIContent label,
            Action drawAction,
            Editor editor,
            Color backgroundColor,
            bool isDefaultExpanded = false,
            List<AnimFoldGroup> siblings = null)
        {
            EnsureStylesInitialized();

            Label = new GUIContent(" " + label.text, label.tooltip);
            _drawAction = drawAction;
            BackgroundColor = backgroundColor;
            _siblingGroups = siblings ?? new List<AnimFoldGroup>();
            _editorPrefsKey = $"{SettingsPrefix}Expand-{label.text}";

            var savedState = EditorPrefs.GetBool(_editorPrefsKey, isDefaultExpanded);
            _animBool = new AnimBool(savedState) { speed = CollapseSpeed };
            _animBool.valueChanged.AddListener(editor.Repaint);
        }

        #region State Management

        private void CollapseSiblings()
        {
            foreach (var sibling in _siblingGroups)
            {
                if (sibling != this && sibling.IsExpanded)
                {
                    sibling.IsExpanded = false;
                }
            }
        }

        private void SaveState() => EditorPrefs.SetBool(_editorPrefsKey, IsExpanded);

        #endregion

        #region GUI Rendering

        public void Draw(int indentLevel = 0)
        {
            var originalColor = GUI.backgroundColor;
            try
            {
                ApplyBackgroundColor();

                GUILayout.BeginVertical(_helpBoxNoPaddingStyle);
                DrawFoldoutHeader(indentLevel);

                if (EditorGUILayout.BeginFadeGroup(Faded))
                {
                    _drawAction?.Invoke();
                }

                EditorGUILayout.EndFadeGroup();

                GUILayout.EndVertical();
            }
            finally
            {
                GUI.backgroundColor = originalColor;
            }
        }

        private void ApplyBackgroundColor()
        {
            var glowIntensity = Mathf.Lerp(EditorGUIUtility.isProSkin ? 0.5f : 0.75f,
                EditorGUIUtility.isProSkin ? 0.85f : 1f,
                Faded);
            GUI.backgroundColor = BackgroundColor * new Color(glowIntensity, glowIntensity, glowIntensity);
        }

        private void DrawFoldoutHeader(int indentLevel)
        {
            GUILayout.Box(GUIContent.none, EditorStyles.miniButton);

            if (Event.current.type == EventType.Repaint)
            {
                var buttonRect = GUILayoutUtility.GetLastRect();
                buttonRect.xMin += indentLevel * EditorGUIUtility.fieldWidth / 3f;

                using (new EditorGUI.IndentLevelScope(1))
                {
                    EditorGUIUtility.SetIconSize(new Vector2(16f, 16f));
                    IsExpanded = EditorGUI.Foldout(buttonRect, IsExpanded, Label, true, _buttonFoldoutStyle);
                    EditorGUIUtility.SetIconSize(Vector2.zero);
                }
            }
        }

        #endregion

        #region Static API

        public static void DrawSection(string label, ref bool isExpanded, Action contentDrawer)
        {
            if (!BeginSection(label, ref isExpanded)) return;
            contentDrawer?.Invoke();
            EndSection();
        }

        private static bool BeginSection(string label, ref bool isExpanded)
        {
            GUI.color = isExpanded ? Color.white : new Color(0.8f, 0.8f, 0.8f, 0.5f);
            GUILayout.BeginVertical(_collapsableSectionStyle);

            if (GUILayout.Button(label, EditorStyles.toolbarButton))
            {
                isExpanded = !isExpanded;
            }

            GUI.color = Color.white;
            return isExpanded;
        }

        private static void EndSection() => GUILayout.EndVertical();

        #endregion

        public void Dispose() => _animBool.valueChanged.RemoveAllListeners();
    }
}