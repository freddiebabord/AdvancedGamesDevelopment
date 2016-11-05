using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DependencyPro.Styles;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor.SceneManagement;

namespace DependencyPro
{
    internal class DependencyWindow : EditorWindow
    {
        private Vector2 _scrollPos;
        [SerializeField] private DependencyBase _data;
        [SerializeField] private bool _fileMode;
        private bool _expandFiles = true;
        private bool _expandScenes = true;
        private static GUIContent _sceneIcon;

        [Serializable]
        public class Style
        {
            public ContentStylePair LookupBtn = new ContentStylePair();
            public GUIStyle TabBreadcrumb0 = new GUIStyle();
            public GUIStyle TabBreadcrumb1 = new GUIStyle();
            public ContentStylePair TabBreadcrumbDotDotDot = new ContentStylePair();
            public GUIStyle RowPropertyLabel = new GUIStyle();
            public GUIStyle RowMainAssetBtn = new GUIStyle();
            public Vector2 Size = new Vector2(250f, 800f);

            public static Style Instance
            {
                get { return _style ?? (_style = DependencyStyle.Instance.Row); }
            }

            private static Style _style;
            [CanBeNull] public GUIStyle RowLabel;
        }

        //    [MenuItem("Window/Dependency Window")]
        private static void InitFileWindow()
        {
            if (Selection.activeObject is SceneAsset)
            {
                EditorUtility.DisplayDialog("Error",
                    "No usages of scene can be found.\n\n" + "Didn't you want to search for asset dependencies inside this scene instead?\n\n" +
                    "In order to do it, open this scene and right-click on asset in Inspector and click\n" + "- Usages in Scene", "OK");
            }
            else
            {
                var window = CreateInstance<DependencyWindow>();
                window.Init(new FileDependency(Selection.activeObject));
                var p = window.position;
                p.size = Style.Instance.Size;
                window.position = p;
                window.Show();
            }
        }


        private void Init(DependencyBase d)
        {
            _data = d;
            _labelMaxWidth = CalculateContentMaxWidth(EditorStyles.label, _data.Dependencies.SelectMany(dd => dd.Properties.Select(p => p.Content)));
            _rowPropWidth = CalculateContentMaxWidth(EditorStyles.label, _data.Target.Nested.Union(new[] {_data.Target.Root}).Where(o => o).Select(o => new GUIContent((o is ScriptableObject || o is MonoScript) ? o.ToString() : o.name)));

            _fileMode = d is FileDependency;
            var sceneDependency = d as InSceneDependency;
            titleContent = new GUIContent(sceneDependency != null ? "Scene Objects" : "File Usages");
        }

        [MenuItem("Assets/- File Usages", false, 30)]
        private static void ContextMenu()
        {
            InitFileWindow();
        }


        [MenuItem("GameObject/-Usages in Scene", false, -1)]
        private static void FindReferencesToAsset(MenuCommand data)
        {
            var selected = Selection.activeObject;
            if (!selected) return;

            var scenePath = SceneManager.GetActiveScene().path;

            InitSceneWindow(selected, scenePath);
        }

        [MenuItem("CONTEXT/Component/- Component Usages (Scene)", false, 159)]
        private static void FindReferencesToComponent(MenuCommand data)
        {
            Object selected = data.context;
            if (!selected) return;

            var scenePath = SceneManager.GetActiveScene().path;

            InitSceneWindow(selected, scenePath);
        }

        private static void InitSceneWindow(Object target, string scenePath)
        {
            DependencyWindow window = CreateInstance<DependencyWindow>();
            window.Init(new InSceneDependency(target, scenePath));
            window.Show();
        }

        private const string FileDependencies = "Used in following files:";
        private const string SceneDependencies = "Used in current scene:";

        private void BreadCrumbs()
        {
            var parents = _data.Parents();
            parents.Reverse();
            var count = parents.Count;
            //        var selectedToolbar = GUILayout.Toolbar(last, parents.Select(p => p.TabName).ToArray());
            var i = 0;
            EditorGUILayout.Space();
            using (new EditorGUILayout.HorizontalScope())
            {
                foreach (var parent in parents)
                {

                    if (i == count - 1)
                    {
                        GUILayout.Toggle(true, parent.TabContent, i == 0 ? Style.Instance.TabBreadcrumb0 : Style.Instance.TabBreadcrumb1);
                    }
                    else if (count < 4)
                    {
                        if (GUILayout.Button(parent.TabContent, i == 0 ? Style.Instance.TabBreadcrumb0 : Style.Instance.TabBreadcrumb1))
                        {
                            Init(parent);
                        }
                    }
                    else
                    {
                        if (i == 0 || i == count - 2)
                        {
                            if (GUILayout.Button(parent.TabContent, i == 0 ? Style.Instance.TabBreadcrumb0 : Style.Instance.TabBreadcrumb1))
                                Init(parent);

                        }
                        else if (i == count - 3)
                        {
                            if (GUILayout.Button(Style.Instance.TabBreadcrumbDotDotDot.Content, Style.Instance.TabBreadcrumbDotDotDot.Style))
                                Init(parent);
                        }
                    }

                    ++i;
                }
            }
        }

        private void Update()
        {
            if (_close)
                Close();
        }

        private void DelayedClose()
        {
            _close = true;
        }

        private void OnGUI()
        {
            if (_data == null)
            {
                DelayedClose();
                return;
            }
            EditorGUILayout.BeginVertical();
            {

                _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
                {
                    BreadCrumbs();
                    EditorGUILayout.Space();
                    ShowDependencies(_data.Dependencies);
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }

        private void ShowDependencies(ResultRow[] dependencies)
        {
            _expandFiles = EditorGUILayout.Foldout(_expandFiles, _fileMode ? FileDependencies : SceneDependencies);
            if (!_fileMode)
            {
                if (!_data.Target.Scene.isLoaded)
                    DelayedClose();
            }
            if (_expandFiles)
            {
                if (dependencies.Any())
                {
                    foreach (var dependency in dependencies)
                        DrawRow(dependency);
                }
                else
                {
                    EditorGUILayout.LabelField("No file dependencies found.");
                }
            }
            EditorGUILayout.Space();

            var fileDep = _data as FileDependency;
            if (fileDep == null)
                return;

            if (fileDep.ScenePaths == null)
            {
                if (GUILayout.Button("Search Scenes"))
                    fileDep.ScenePaths = FindDependencies.ScenesThatContain(_data.Target.Target).Select(p => new FileDependency.Pair {Path = p, NicifiedPath = p.Replace("Assets/", string.Empty)}).ToArray();
                return;
            }
            _expandScenes = EditorGUILayout.Foldout(_expandScenes, "Scenes:");

            if (!_expandScenes) return;

            if (!fileDep.ScenePaths.Any())
            {
                EditorGUILayout.LabelField("No scene dependencies found.");
                return;
            }

            for (int i = 0; i < fileDep.ScenePaths.Length; i++)
            {
                var p = fileDep.ScenePaths[i];
                using (new EditorGUILayout.HorizontalScope())
                {
                    SceneIcon.text = p.NicifiedPath;

                    if (GUILayout.Button(SceneIcon, EditorStyles.label, GUILayout.Height(16f)))
                        Selection.activeObject = AssetDatabase.LoadAssetAtPath<SceneAsset>(p.Path);

                    if (!GUILayout.Button("Open scene & search", GUILayout.Width(200f)))
                        continue;

                    if (SceneManager.GetSceneByPath(p.Path).isLoaded)
                    {
                        InitSceneWindow(_data.Target.Target, p.Path);
                    }
                    else
                    {
                        EditorSceneManager.OpenScene(p.Path);
                        EditorSceneExtensions.FireOnSceneOpenAndForget(() => InitSceneWindow(_data.Target.Target, p.Path));
                    }
                }
            }
        }

        private struct PrevClick
        {
            public Object Target;
            public float TimeClicked;

            public PrevClick(Object target)
            {
                Target = target;
                TimeClicked = Time.realtimeSinceStartup;
            }

            private const float DoubleClickTime = 0.5f;

            public bool IsDoubleClick(Object o)
            {
                return Target == o && Time.realtimeSinceStartup - TimeClicked < DoubleClickTime;
            }
        }

        private PrevClick _click;
        private bool _close;
        private float _rowPropWidth;
        private float _labelMaxWidth;

        private void DrawRow(ResultRow dependency)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button(dependency.LabelContent, Style.Instance.RowMainAssetBtn))
                    {
                        if (_click.IsDoubleClick(dependency.Main))
                            Selection.activeObject = dependency.Main;
                        else
                            EditorGUIUtility.PingObject(dependency.Main);

                        _click = new PrevClick(dependency.Main);
                    }
                    if (GUILayout.Button(Style.Instance.LookupBtn.Content, Style.Instance.LookupBtn.Style))
                    {
                        Init(_data.Nest(dependency.Main));
                    }
                }
                dependency.SerializedObject.Update();
                EditorGUI.BeginChangeCheck();
                if (dependency.Target)
                {
                    foreach (var prop in dependency.Properties)
                    {
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            var locked = prop.Property.objectReferenceValue is MonoScript;
                            var f = GUI.enabled;

                            if (locked) GUI.enabled = false;

                            EditorGUILayout.LabelField(prop.Content, Style.Instance.RowLabel, GUILayout.MaxWidth(_labelMaxWidth));
                            EditorGUILayout.PropertyField(prop.Property, GUIContent.none, true, GUILayout.MinWidth(_rowPropWidth));

                            if (locked) GUI.enabled = f;
                        }
                    }
                }

                if (EditorGUI.EndChangeCheck())
                    dependency.SerializedObject.ApplyModifiedProperties();
            }
        }


        private static float CalculateContentMaxWidth(GUIStyle rowStyle, IEnumerable<GUIContent> guiContents)
        {
            var maxWidth = 0f;
            foreach (var guiContent in guiContents)
            {
                float min, max;
                rowStyle.CalcMinMaxWidth(guiContent, out min, out max);
                maxWidth = Mathf.Max(maxWidth, max);
            }
            return maxWidth;
        }

        private static GUIContent SceneIcon
        {
            get { return _sceneIcon ?? (_sceneIcon = new GUIContent(AssetPreview.GetMiniTypeThumbnail(typeof (SceneAsset)))); }
        }

        internal static class EditorSceneExtensions
        {
            private static Action _delayedAction;

            public static void FireOnSceneOpenAndForget(Action a)
            {
                _delayedAction = a;
                EditorApplication.hierarchyWindowChanged += Callback;
            }

            private static void Callback()
            {
                EditorApplication.hierarchyWindowChanged -= Callback;
                _delayedAction();
                _delayedAction = null;
            }
        }
    }
}