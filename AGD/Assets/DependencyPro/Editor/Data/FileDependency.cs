using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DependencyPro
{
    [Serializable]
    internal sealed class FileDependency : DependencyBase
    {
        public class Pair
        {
            public string NicifiedPath;
            public string Path;
        }
        public Pair[] ScenePaths;

        public FileDependency(Object target)
        {
            Target = new SearchTarget(target);
            Update();
//        SceneObjectDependencies = FindDependencies.ScenesThatContain(Target as GameObject).Select(p => AssetDatabase.LoadAssetAtPath<Object>(p)).ToArray();
            //        SceneDependencies = FindDependencies.ScenesThatContain(Target as GameObject).Select(p => AssetDatabase.LoadAssetAtPath<Object>(p)).ToArray();

            TabContent = new GUIContent
            {
                text = target.name,
                image = AssetDatabase.GetCachedIcon(AssetDatabase.GetAssetPath(Target.Target))
            };
        }


        public override void Update()
        {
            var files = FindDependencies.FilesThatReference(Target);
            Dependencies = Group(files.Where(f => !(f.Target is SceneAsset)))
                .OrderBy(t => t.LabelContent.text, StringComparer.Ordinal)
                .ToArray();
//            ScenePaths = FindDependencies.ScenesThatContain(Target).ToArray();
        }

        public override DependencyBase Nest(Object o)
        {
            return new FileDependency(o) {Parent = this};
        }
    }
}