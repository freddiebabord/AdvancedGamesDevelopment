using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpokkticleEditorWindow : EditorWindow {
    
    
    // Add menu named "My Window" to the Window menu
    [MenuItem ("Window/Spookticle Manager")]
    static void Init () {
        // Get existing open window or if none, make a new one:
        SpokkticleEditorWindow window = (SpokkticleEditorWindow)EditorWindow.GetWindow (typeof (SpokkticleEditorWindow));
		window.titleContent = new GUIContent("Spookticle Manager");
        window.Show();
    }

	public void OnEnable()
    {
        Refresh();
    }
	
    public void Refresh()
    {
        EditorApplication.update += Update;
    }

	private bool doQuickStart = false;
    public void Update()
    {
        if (doQuickStart)
        {
            if (EditorApplication.isPlaying)
            {
                Debug.Log("Quickstart!");
                EditorApplication.LoadLevelInPlayMode(@"Assets/Resources/Scenes/Menu.unity");
                doQuickStart = false;
            }
        }
    }
    
    void OnGUI () {
		GUILayout.Label("");
        GUILayout.BeginVertical();
		if (GUILayout.Button("Play", GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3)))
		{
			EditorApplication.SaveCurrentSceneIfUserWantsTo();
			doQuickStart = true;
			EditorApplication.isPlaying = true;
		}
        GUILayout.FlexibleSpace();
		if(GUILayout.Button("Jump To Scenes Folder"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Scenes/Menu.unity");
        if (GUILayout.Button("Jump To Prefabs Folder"))
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Prefabs/UI");
        if (GUILayout.Button("Jump To Art Folder"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Art/Assets");
		if(GUILayout.Button("Jump To Post Processing"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Standard Assets/PostProcessing/Default.asset");
        if (GUILayout.Button("Jump To Scripts"))
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Scripts/Managers");
        GUILayout.EndVertical();
    }
}
