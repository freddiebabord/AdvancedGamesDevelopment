using UnityEngine;
using UnityEditor;
using System.Collections;

public class SpokkticleEditorWindow : EditorWindow {

	string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;
    
    // Add menu named "My Window" to the Window menu
    [MenuItem ("Window/Spookticle Manager")]
    static void Init () {
        // Get existing open window or if none, make a new one:
        SpokkticleEditorWindow window = (SpokkticleEditorWindow)EditorWindow.GetWindow (typeof (SpokkticleEditorWindow));
		window.title = "Spookticle Manager";
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
		if(GUILayout.Button("Jump To Scenes Folder"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Scenes/Menu.unity");
		if(GUILayout.Button("Jump To Art Folder"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Art/Assets");
		if(GUILayout.Button("Jump To Post Processing"))
			Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(@"Assets/Resources/Standard Assets/PostProcessing/Default.asset");
		GUILayout.EndVertical();
    }
}
