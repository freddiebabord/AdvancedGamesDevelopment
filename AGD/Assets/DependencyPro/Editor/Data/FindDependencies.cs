using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DependencyPro
{
    internal static class FindDependencies
    {
        private static void InformUser(IEnumerable<Object> dependencies)
        {
            var arr = dependencies as Object[] ?? dependencies.ToArray();
            if (arr.Any())
            {
                Selection.objects = arr;
                foreach (var dependency in arr)
                    EditorGUIUtility.PingObject(dependency);
                Debug.Log(arr
                        .Select(o => o.name)
                        .Aggregate((a, b) => a + ", " + b));
            }
            else
                EditorUtility.DisplayDialog("Backlinks Search",
                    string.Format("No backlinks were found for '{0}'",
                        Selection.objects
                            .Select(o => o.name)
                            .Aggregate((a, b) => a + ", " + b)),
                    "OK");
        }

        #region Project
        
        //        [MenuItem("Assets/Backlinks/In Files", false, 49)]
        private static HashSet<string> Dependencies(Object activeObject)
        {
            var targetPath = AssetDatabase.GetAssetPath(activeObject);
            var objectGuids = AssetDatabase.FindAssets("t:Object");
            var results = new HashSet<string>(StringComparer.Ordinal);
            var total = objectGuids.LongLength;

            for (int i = 0; i < total; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(objectGuids[i]);
                if (assetPath.Contains(".unity") || AssetExtensionsWithoutReference.Any(t => assetPath.Contains(t)))
                    continue;

                var h = new HashSet<string>(AssetDatabase.GetDependencies(assetPath), StringComparer.Ordinal);

                if (h.Contains(targetPath))
                    results.Add(assetPath);

                if (EditorUtility.DisplayCancelableProgressBar("Searching for file usages..", assetPath, (float)i / total))
                    break;
            }
            EditorUtility.ClearProgressBar();
            results.Remove(targetPath);
            return results;
        }

        public static IEnumerable<ResultRow> FilesThatReference(SearchTarget target)
        {
            return Dependencies(target.Target)
                .SelectMany(p => AssetDatabase.LoadAllAssetsAtPath(p))
                .Where(t => t && !(t is DefaultAsset) && !(t is Transform))
                .Select(asset => CheckObject(target, asset, false))
                .Where(row => row != null);
        }

        public static HashSet<string> ScenesThatContain(Object activeObject)
        {
            var targetPath = AssetDatabase.GetAssetPath(activeObject);

            var results = new HashSet<string>(StringComparer.Ordinal);
            var sceneGuids = AssetDatabase.FindAssets("t:Scene");

            var total = sceneGuids.LongLength;
            for (int i = 0; i < total; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                var h = new HashSet<string>(AssetDatabase.GetDependencies(assetPath), StringComparer.Ordinal);

                if (h.Contains(targetPath))
                    results.Add(assetPath);
                if (EditorUtility.DisplayCancelableProgressBar("Searching for file usages in scenes..", assetPath, (float)i / total))
                    break;
            }
            EditorUtility.ClearProgressBar();
            results.Remove(targetPath);
            return results;
        }

        #endregion

        #region Scene

        public static IEnumerable<T> AsEnumerable<T>(T o)
        {
            yield return o;
        } 
        public static IEnumerable<ResultRow> InScenePro(SearchTarget target)
        {
            var referencedBy = new List<ResultRow>();

            for (int ii = 0; ii < SceneManager.sceneCount; ii++)
            {
                var currentScene = SceneManager.GetSceneAt(ii);
                if (!currentScene.IsValid() || !currentScene.isLoaded)
                    continue;
                var allObjects = currentScene
                    .GetRootGameObjects()
                    .SelectMany(g => g.GetComponentsInChildren<Component>(true).Where(c => c && !(c is Transform)).OfType<Object>()
                .Union(AsEnumerable(g as Object))).ToArray();
                var total = allObjects.Length;
                for (int i = 0; i < total; i++)
                {
                    var comp = allObjects[i];
                    var res = CheckObject(target, comp);
                    if (res == null)
                        continue;
                    referencedBy.Add(res);

                    if (EditorUtility.DisplayCancelableProgressBar("Searching for file usages in current scene..", target.Target.name, (float)i / total))
                        break;
                }
                EditorUtility.ClearProgressBar();

            }

            return referencedBy;
        }

        private static ResultRow CheckObject(SearchTarget target, Object c, bool scene = true)
        {
            if (target.Check(c)) return null;
            if (target.Root == c) return null;
            var so = new SerializedObject(c);
            var sp = so.GetIterator();
            ResultRow row = null;
            while (sp.Next(true))
            {
                if (sp.propertyType != SerializedPropertyType.ObjectReference || !target.Check(sp.objectReferenceValue)) continue;
                if (row == null)
                {
                    row = new ResultRow
                    {
                        Root = c,
                        Target = c,
                        SerializedObject = so
                    };

                    if (scene)
                    {
                        var component = c as Component;
                        if (component)
                        {
                            row.Main = component.gameObject;
                        }
                        else
                        {
                            var o = c as GameObject;
                            if (o != null)
                            {
                                row.Main = o;
                            }
                        }
                        var go = row.Main as GameObject;
                        Assert.NotNull(go);
                        row.LabelContent.text = AnimationUtility.CalculateTransformPath(go.transform, null);
                        row.LabelContent.image = AssetPreview.GetMiniThumbnail(go);
                        ;
                    }
                    else
                    {
                        var path = AssetDatabase.GetAssetPath(c);
                        row.Main = AssetDatabase.LoadMainAssetAtPath(path);
                        row.LabelContent.text = path.Replace(AssetsRootPath, string.Empty);
                        row.LabelContent.image = AssetDatabase.GetCachedIcon(path);
                        ;
                    }
                }

                Texture2D miniTypeThumbnail = row.Main == c ? null : AssetPreview.GetMiniThumbnail(c);

                row.Properties.Add(new ResultRow.PropertyData
                {
                    Property = sp.Copy(),
                    Content = new GUIContent
                    {
                        image = miniTypeThumbnail,
                        text = Nicify(sp, sp.serializedObject.targetObject),
                        tooltip= string.Format("{0}.{1}", sp.serializedObject.targetObject.GetType().Name, sp.propertyPath)
                    }
                });
            }

            if (row == null)
                so.Dispose();
            return row;
        }

        private static string Nicify(SerializedProperty sp, Object o)
        {
            //            return sp.propertyPath;

            string nice;
            if (o is AnimatorState)
                return o.name;

            if (o is Material)
                nice = sp.displayName;
            else
            {
                nice = sp.propertyPath.Replace(".Array.data", string.Empty);
                if (nice.IndexOf(".m_PersistentCalls.m_Calls", StringComparison.Ordinal) > 0)
                {
                    nice = nice.Replace(".m_PersistentCalls.m_Calls", string.Empty)
                        .Replace(".m_Target", string.Empty);
                }
                nice = nice.Split('.').Select(t => ObjectNames.NicifyVariableName(t).Replace(" ", string.Empty)).Aggregate((a, b) => a + "." + b);
            }
            nice = string.Format("{0}.{1}", o.GetType().Name, nice);
            return nice;
        }

        private static readonly string[] AssetExtensionsWithoutReference = {".fbx", ".png", ".jpg", ".bmp", ".tga", ".jpeg", ".psd", ".guiskin", ".txt", ".anim"};
        private const string AssetsRootPath = "Assets/";

        public static IEnumerable<Object> InScene(Object to)
        {
            var referencedBy = new List<Object>();

            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>()
                .Select(g => g.transform);

            foreach (var go in allObjects)
            {
                var prefabType = PrefabUtility.GetPrefabType(go);
                switch (prefabType)
                {
                    case PrefabType.ModelPrefab:
                    case PrefabType.Prefab:
                        continue;
                    case PrefabType.None:
                    case PrefabType.ModelPrefabInstance:
                    case PrefabType.MissingPrefabInstance:
                    case PrefabType.DisconnectedPrefabInstance:
                    case PrefabType.DisconnectedModelPrefabInstance:
                    case PrefabType.PrefabInstance:
                        if (PrefabUtility.GetPrefabParent(go) == to)
                            referencedBy.Add(go);
                        break;
                }

                var components = go.GetComponents<Component>().Where(c => !(c is Transform));
                foreach (var c in components)
                {
                    var so = new SerializedObject(c);
                    var sp = so.GetIterator();

                    while (sp.Next(true))
                    {
                        if (sp.propertyType == SerializedPropertyType.ObjectReference && sp.objectReferenceValue == to)
                        {
                            //                            Debug.LogFormat("{0}.{1}", sp.serializedObject.targetObject, sp.propertyPath);
                            referencedBy.Add(c.gameObject);
                        }
                    }
                }
            }

            return referencedBy;
        }

        #endregion
    }
}