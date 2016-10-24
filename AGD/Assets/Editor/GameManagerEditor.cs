using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using Rotorz.ReorderableList;
using Rotorz.ReorderableList.Internal;

// Draw outer reorderable list control within custom inspector.
[CustomEditor(typeof(GameManager))]
public class SomeBehaviourEditor : Editor {

    private SerializedProperty _itemsProperty;
    GameManager gm;
    private void OnEnable() {
        _itemsProperty = serializedObject.FindProperty("waves");
        gm = (GameManager)target;
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        serializedObject.Update();
		GUILayout.Space(10);
		GUILayout.BeginHorizontal();
        GUILayout.Box("Waves", GUILayout.ExpandWidth(true));
		GUILayout.EndHorizontal();
		ReorderableListGUI.ListField(_itemsProperty);
        serializedObject.ApplyModifiedProperties();
        
        
    }

}

// Draw inner reorderable list control using property drawer.
[CustomPropertyDrawer(typeof(WaveDefinition))]
public class OuterItemPropertyDrawer : PropertyDrawer {

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        var itemsProperty = property.FindPropertyRelative("enemyDef");
        return ReorderableListGUI.CalculateListFieldHeight(itemsProperty);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        position = EditorGUI.PrefixLabel(position, label);

        var itemsProperty = property.FindPropertyRelative("enemyDef");
        ReorderableListGUI.ListFieldAbsolute(position, itemsProperty);
    }

}

// Draw inner-most items using property drawer.
[CustomPropertyDrawer(typeof(EnemyDefinition))]
public class NestedItemPropertyDrawer : PropertyDrawer {

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Rect leftRect = position;
		leftRect.width /= 2;
		Rect rightRect = position;
		rightRect.xMin += leftRect.width + 5;
		rightRect.xMax = position.xMax;
        var someValueProperty = property.FindPropertyRelative("enemySpawner");
		EditorGUI.PropertyField(leftRect, someValueProperty, label);

		var quantityProperty = property.FindPropertyRelative("quantity");
		quantityProperty.intValue = EditorGUI.IntField(rightRect, quantityProperty.intValue);
    }

}