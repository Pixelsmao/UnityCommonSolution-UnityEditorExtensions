using UnityEditor;
using UnityEngine;

namespace MFramework.EditorExtensions
{
    public static class EditorGUIIcons
    {
        public static GUIContent PackageInstalled { get; }
        public static GUIContent UnityLogo { get; }
        public static Texture Github { get; }
        public static Texture GithubLogo { get; }
        public static Texture Gitee { get; }
        public static Texture Home { get; }

        static EditorGUIIcons()
        {
            PackageInstalled = EditorGUIUtility.IconContent("package_installed");
            UnityLogo = EditorGUIUtility.IconContent("UnityLogo");
            Github = Resources.Load<Texture>("Icons/icons-github-128");
            GithubLogo = Resources.Load<Texture>("Icons/icons-github-1156");
            Gitee = Resources.Load<Texture>("Icons/icons-gitee-64");
            Home = Resources.Load<Texture>("Icons/icons-home-96");
        }
    }
}