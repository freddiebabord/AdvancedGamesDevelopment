using System;
using UnityEngine;

namespace DependencyPro.Styles
{
    [Serializable]
    internal class ContentStylePair
    {
        public GUIStyle Style = new GUIStyle();
        public GUIContent Content = new GUIContent();
    }
}