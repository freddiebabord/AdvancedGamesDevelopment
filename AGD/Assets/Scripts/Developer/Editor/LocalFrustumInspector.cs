using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalFrustum))]
public class LocalFrustumInspector : Editor
{

    public override void OnInspectorGUI()
    {
        LocalFrustum myTarget = (LocalFrustum)target;

        myTarget.ShowView = EditorGUILayout.Foldout(myTarget.ShowView, "Frustum Views");
        if (myTarget.ShowView)
        {
            EditorGUI.indentLevel++;
            myTarget.drawFrustum = (DrawFrustum)EditorGUILayout.EnumPopup("Draw Frustum", myTarget.drawFrustum);
            EditorGUILayout.Space();
            myTarget.IsPeaceful = EditorGUILayout.Foldout(myTarget.IsPeaceful, "Peaceful");
            if (myTarget.IsPeaceful)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Near", GUILayout.Width(100));
                EditorGUIUtility.labelWidth = 75;
                float nearWidth = EditorGUILayout.FloatField("Width", myTarget.peaceful.near.x, GUILayout.MaxWidth(150));
                float nearHeight = EditorGUILayout.FloatField("Height", myTarget.peaceful.near.y, GUILayout.MaxWidth(150));
                myTarget.peaceful.near = new Vector2(nearWidth, nearHeight);
                GUILayout.Space(250);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Far", GUILayout.Width(100));
                EditorGUIUtility.labelWidth = 75;
                float farWidth = EditorGUILayout.FloatField("Width", myTarget.peaceful.far.x, GUILayout.MaxWidth(150));
                float farHeight = EditorGUILayout.FloatField("Height", myTarget.peaceful.far.y, GUILayout.MaxWidth(150));
                myTarget.peaceful.far = new Vector2(farWidth, farHeight);
                GUILayout.Space(250);
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth = 150;
                EditorGUILayout.Space();
                myTarget.peaceful.distance = EditorGUILayout.FloatField("Distance", myTarget.peaceful.distance);
                EditorGUI.indentLevel--;
            }
            myTarget.IsAggravated = EditorGUILayout.Foldout(myTarget.IsAggravated, "Aggravated");
            if (myTarget.IsAggravated)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Near", GUILayout.Width(100));
                EditorGUIUtility.labelWidth = 75;
                float nearWidth = EditorGUILayout.FloatField("Width", myTarget.aggro.near.x, GUILayout.MaxWidth(150));
                float nearHeight = EditorGUILayout.FloatField("Height", myTarget.aggro.near.y, GUILayout.MaxWidth(150));
                myTarget.aggro.near = new Vector2(nearWidth, nearHeight);
                GUILayout.Space(250);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Far", GUILayout.Width(100));
                EditorGUIUtility.labelWidth = 75;
                float farWidth = EditorGUILayout.FloatField("Width", myTarget.aggro.far.x, GUILayout.MaxWidth(150));
                float farHeight = EditorGUILayout.FloatField("Height", myTarget.aggro.far.y, GUILayout.MaxWidth(150));
                myTarget.aggro.far = new Vector2(farWidth, farHeight);
                GUILayout.Space(250);
                EditorGUILayout.EndHorizontal();
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.Space();
                myTarget.aggro.distance = EditorGUILayout.FloatField("Distance", myTarget.aggro.distance);
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }
        myTarget.attentionSpan = EditorGUILayout.FloatField("Attention Span Timer", myTarget.attentionSpan);
    }
    void OnSceneGUI()
    {
        SceneView.RepaintAll();
    }
}
